using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField]
    GameObject goodApple, goldApple, badApple;
    [SerializeField]
    int boardSizeX, boardSizeY, dropHeight, groundHeight, numPooledApples, gameTimePerRoundInSeconds, warningCountdown;
    int timeLeft, score;
    [SerializeField]
    float timeBetweenDrops;
    bool isGameRunning;
    List<GameObject> applePool, goldPool, badPool;

    //UI shit
    [SerializeField]
    TMP_Text scoreText, warningText, warningCountdownText;
    [SerializeField]
    Image timerUI;
    AudioSource source;
    [SerializeField]
    AudioClip fuckTikTok, timesUpRinger;
    

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }

        //object pool of apples
        applePool = new List<GameObject>();
        badPool = new List<GameObject>();
        goldPool = new List<GameObject>();
        GameObject tmp;
        
        for(int i = 0; i < numPooledApples; i++){
            tmp = Instantiate(goodApple);
            applePool.Add(tmp);
            tmp.SetActive(false);

            tmp = Instantiate(badApple);
            badPool.Add(tmp);
            tmp.SetActive(false);

            tmp = Instantiate(goldApple);
            goldPool.Add(tmp);
            tmp.SetActive(false);
        }

        source = GetComponent<AudioSource>();

    }


    void StartGame(){
        isGameRunning = true;
        score = 0;
        timeLeft = gameTimePerRoundInSeconds;
        StartCoroutine("Warning");
    }

    void StopGame(){
        isGameRunning = false;
        StopCoroutine("AppleSpawnTimer");
        StopCoroutine("Timer");
        StopAllCoroutines();

        timerUI.fillAmount = 0.0f;
        foreach(GameObject g in applePool){
            g.SetActive(false);
        }
        foreach(GameObject g in goldPool){
            g.SetActive(false);
        }
        foreach(GameObject g in badPool){
            g.SetActive(false);
        }

        scoreText.text = "You harvested " + score.ToString() + " apples!";
        StartCoroutine("Cooldown");
    }

    public void AdjustScore(int i){
        score += i;
        if(score < 0){
            score = 0;
        }
        scoreText.text = score.ToString();
    }

    void Start(){
        scoreText.text = "";
        timerUI.fillAmount = 0.0f;

        StartGame();
    }


    GameObject GetPooledApple(){
        for (int i = 0; i < numPooledApples; i++){
            if(!applePool[i].activeInHierarchy){
                return applePool[i];
            }
        }
        return null;
    }

    GameObject GetPooledGold(){
        for (int i = 0; i < numPooledApples; i++){
            if(!goldPool[i].activeInHierarchy){
                return goldPool[i];
            }
        }
        return null;
    }

    GameObject GetPooledBad(){
        for (int i = 0; i < numPooledApples; i++){
            if(!badPool[i].activeInHierarchy){
                return badPool[i];
            }
        }
        return null;
    }


    void SpawnApple(){
        int random = Mathf.RoundToInt(Random.Range(1, 20));
        //Debug.Log(random.ToString());
        GameObject apple = null;

        if(random >= 15){
            //spawn gold apple
            apple = GetPooledGold();
        }

        if(random <= 5){
            //spawn bad apple
            apple = GetPooledBad();
        }

        if(random > 5 && random < 15) {
            //spawn good apple
            apple = GetPooledApple();
        }

        apple.SetActive(true);
        apple.transform.position = new Vector3(
            Mathf.RoundToInt(Random.Range(-boardSizeX/2, boardSizeX/2)),
            dropHeight,
            Mathf.RoundToInt(Random.Range(-boardSizeY/2, boardSizeY/2))
        );

    }

    IEnumerator AppleSpawnTimer(){
        while(isGameRunning){
            SpawnApple();
            yield return new WaitForSecondsRealtime(timeBetweenDrops);    
        }
    }

    IEnumerator Timer(){
        while(isGameRunning){
            float percent = (float)timeLeft/(float)gameTimePerRoundInSeconds;
            timerUI.fillAmount = percent;

            yield return new WaitForSecondsRealtime(1);

            if(timeLeft < 1){
                StopGame();
            }

            if(timeLeft < 6 && timeLeft > 0){
                source.PlayOneShot(fuckTikTok);
            }

            if(timeLeft <= 0){
                source.PlayOneShot(timesUpRinger);
            }

            timeLeft--;
        }
    }

    //lots of magic numbers below
    //idgaf, it's gamejam code
    IEnumerator Warning(){
        scoreText.text = "";
        warningCountdown = 3;
        warningText.text = "Get Ready!";
        warningCountdownText.text = warningCountdown.ToString();
        while(warningCountdown > 0){
            warningCountdownText.text = warningCountdown.ToString();
            yield return new WaitForSecondsRealtime(1.5f);
            warningCountdown--;
        }
        warningText.text = "";
        warningCountdownText.text = "";

        isGameRunning = true;
        scoreText.text = "0";

        StartCoroutine("AppleSpawnTimer");
        StartCoroutine("Timer");
    }

    IEnumerator Cooldown(){
        yield return new WaitForSecondsRealtime(10f);
        StartGame();
    }

    public int GetDropHeight(){
        return dropHeight;
    }

    public int GetGroundHeight(){
        return groundHeight;
    }

}
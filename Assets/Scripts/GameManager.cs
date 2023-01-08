using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField]
    GameObject goodApple, goldApple, badApple;
    [SerializeField]
    int boardSizeX, boardSizeY, dropHeight, groundHeight, numPooledApples, timeBetweenDrops;
    bool isGameRunning;
    List<GameObject> applePool, goldPool, badPool;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
        isGameRunning = true;
    }


    void Start(){
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

        StartCoroutine("AppleSpawnTimer");
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
        Debug.Log(random.ToString());
        GameObject apple = null;

        if(random >= 19){
            //spawn gold apple
            apple = GetPooledGold();
        }

        if(random <= 4){
            //spawn bad apple
            apple = GetPooledBad();
        }

        if(random > 4 && random < 19) {
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

    public int GetDropHeight(){
        return dropHeight;
    }

    public int GetGroundHeight(){
        return groundHeight;
    }

}
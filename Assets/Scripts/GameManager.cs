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
    List<GameObject> applePool;

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
        GameObject tmp;
        for(int i = 0; i < numPooledApples; i++){
            tmp = Instantiate(goodApple);
            applePool.Add(tmp);
            tmp.SetActive(false);
        }

        StartCoroutine("AppleSpawnTimer");
    }

    GameObject GetPooledObject(){
        for (int i = 0; i < numPooledApples; i++){
            if(!applePool[i].activeInHierarchy){
                return applePool[i];
            }
        }
        return null;
    }



    void SpawnApple(){
        GameObject apple = GetPooledObject();
        apple.SetActive(true);
        apple.transform.position = new Vector3(Random.Range(-boardSizeX/2, boardSizeX/2), dropHeight, Random.Range(-boardSizeY/2, boardSizeY/2));        
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
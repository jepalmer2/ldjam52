using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject workerAnt, soldierAnt;

    void Start(){
        Instantiate(workerAnt);
    }

}
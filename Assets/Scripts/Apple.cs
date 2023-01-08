using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    int pointValue;
    [SerializeField]
    float speed;
    float groundHeight;

    void Awake(){
        groundHeight = GameManager.Instance.GetGroundHeight();
    }

    void FixedUpdate(){
        //fall
        //float dropHeight = GameManager.Instance.GetDropHeight();
        //float percentJourney = (dropHeight - transform.position.y) / dropHeight;
        transform.Translate(new Vector3(
            0,
            -speed,
            //Mathf.Lerp(dropHeight, groundHeight, percentJourney * Time.fixedDeltaTime),
            0
        ));


        if(transform.position.y <= groundHeight){
            gameObject.SetActive(false);
        }
    }

}

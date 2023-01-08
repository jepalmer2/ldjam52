using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    int pointValue;
    [SerializeField]
    float maxSpeed;
    float speed;

    float groundHeight;

    void Awake(){
        groundHeight = GameManager.Instance.GetGroundHeight();
        speed = Random.Range(0.25f, maxSpeed) / 10;
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
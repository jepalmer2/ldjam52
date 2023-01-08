using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWand : MonoBehaviour
{
    [SerializeField]
    GameObject wand;
    Vector3 wandPos;
    Vector3 targetPos;
    [SerializeField]
    float moveSpeed;

   void Update(){
        wandPos = wand.transform.position;
        targetPos = Vector3.Lerp(transform.position, wandPos, moveSpeed);
   }

   void FixedUpdate(){
    transform.position = new Vector3(targetPos.x, GameManager.Instance.GetGroundHeight()+1, targetPos.z);
   }

}

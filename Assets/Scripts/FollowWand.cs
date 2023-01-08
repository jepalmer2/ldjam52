using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWand : MonoBehaviour
{
    [SerializeField]
    GameObject wand;
    Vector3 wandPos;
    Vector3 targetPos;
    float moveSpeed = 1f;

   void Update(){
        wandPos = wand.transform.position;
        targetPos = Vector3.Lerp(transform.position, wandPos, moveSpeed);
   }

   void FixedUpdate(){
    transform.position = new Vector3(targetPos.x, 1.5f, targetPos.z);
   }

}

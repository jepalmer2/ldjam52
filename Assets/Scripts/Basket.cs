using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Basket : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip clip;

    void Awake(){
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Apple"){
            int tmp = other.gameObject.GetComponent<Apple>().GetValueAndCollect();
            GameManager.Instance.AdjustScore(tmp);
            source.PlayOneShot(clip);
        }
    }
}

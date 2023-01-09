using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Basket : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip[] negativeReinforcement = new AudioClip[3];
    [SerializeField]
    AudioClip[] positiveReinforcement = new AudioClip[3];

    void Awake(){
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Apple"){
            int tmp = other.gameObject.GetComponent<Apple>().GetValueAndCollect();
            GameManager.Instance.AdjustScore(tmp);
            source.PlayOneShot(clip);
            if(tmp < 0){
                source.PlayOneShot(negativeReinforcement[Mathf.RoundToInt(Random.Range(0, negativeReinforcement.Length-1))]);
            }
            if(tmp > 1){
                source.PlayOneShot(positiveReinforcement[Mathf.RoundToInt(Random.Range(0, positiveReinforcement.Length-1))]);
            }
        }
    }
}

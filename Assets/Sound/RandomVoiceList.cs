using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVoiceList : MonoBehaviour
{
    public AudioSource trigSource;
    public AudioClip[] soundList;
    public AudioClip sound;
    public int count=0;

    void OnTriggerEnter()
    {
        int randNum = Random.Range(0, soundList.Length);
        trigSource.PlayOneShot(soundList[randNum]);
    }

}

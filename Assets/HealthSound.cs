using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSound : MonoBehaviour
{
    private AudioSource mAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        mAudioSrc = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        mAudioSrc.Play();
    }
}
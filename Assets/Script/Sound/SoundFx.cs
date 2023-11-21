using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFx : MonoBehaviour
{
    AudioSource audio;
    public float volumeSfx;
    public bool isHit;
    [SerializeField] private AudioClip hit;

    private void Awake() 
    {
        audio = GetComponentInChildren<AudioSource>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        audio.volume = volumeSfx;
    }

    public void audioSfx(AudioClip audioClip)
    {
        if(!isHit)
            audio.PlayOneShot(audioClip);
    }

    public void hitSfx()
    {
        if(isHit)
        {
            isHit = false;
            audio.PlayOneShot(hit);
        }
    }
}

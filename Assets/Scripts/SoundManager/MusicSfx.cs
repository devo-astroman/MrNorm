using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSfx : MonoBehaviour
{

    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip musicClip;

    void Start(){
        PlayMusic();
    }

    public void PlayMusic(){
        Debug.Log("Play music");
        aSource.clip = musicClip;
        aSource.Play();
    }
}


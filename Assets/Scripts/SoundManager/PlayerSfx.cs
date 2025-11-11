using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfx : MonoBehaviour
{

    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip checkpointClip;
    [SerializeField] private AudioClip congratsClip;

    void Start(){}

    void Update(){}

    public void PlayCoinSfx(){
        Debug.Log("Play coin");
        aSource.clip = coinClip;
        aSource.Play();
    }

    public void PlayHurtSfx(){
        Debug.Log("Play hurt");
        aSource.clip = hurtClip;
        aSource.Play();
    }

    public void PlayCheckpointSfx(){
        Debug.Log("Play checkpoint");
        aSource.clip = checkpointClip;
        aSource.Play();
    }

    public void PlayCongratsSfx(){
        Debug.Log("Play congrats");
        aSource.clip = congratsClip;
        aSource.Play();
    }


}


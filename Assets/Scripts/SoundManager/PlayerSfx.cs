using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfx : MonoBehaviour
{

    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip hurtClip;

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


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /* [Header("Hero")]
    [SerializeField] private HeroDamageHandler heroDamageHandler;
    [SerializeField] private HeroJetpackMovement heroJetpackMovement; */

    [Header("Player")]
    [SerializeField] private PlayerSfx playerSfx;


    void Start(){
    }


    void Update()
    {        
    }

    public void PlayCoinSfx(){
        Debug.Log("Play coin");
        playerSfx.PlayCoinSfx();
    }

    public void PlayHurtSfx(){
        Debug.Log("Play hurt");
        playerSfx.PlayHurtSfx();
    }

    public void HandleHeroHurt(){
        PlayHurtSfx();
    }

    public void HandleHeroDied(){
        //playdead
    }

    public void HandleCoinTake(){
        PlayCoinSfx();
    }

    public void HandleTouchCheckpoint(){
        PlayCheckpointSfx();
    }
    
    public void PlayCheckpointSfx(){
        playerSfx.PlayCheckpointSfx();
    }

//
    public void HandleShowCongrats(){
        PlayCongratsSfx();
    }
    
    public void PlayCongratsSfx(){
        playerSfx.PlayCongratsSfx();
    }


}


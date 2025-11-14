using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private PlayerSfx playerSfx;    


    void Start(){
    }


    void Update()
    {        
    }

    public void PlayCoinSfx(){
        playerSfx.PlayCoinSfx();
    }

    public void PlayHurtSfx(){
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


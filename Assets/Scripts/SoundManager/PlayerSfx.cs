using UnityEngine;

public class PlayerSfx : MonoBehaviour
{

    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip checkpointClip;
    [SerializeField] private AudioClip congratsClip;

    public void PlayCoinSfx(){
        aSource.clip = coinClip;
        aSource.Play();
    }

    public void PlayHurtSfx(){
        aSource.clip = hurtClip;
        aSource.Play();
    }

    public void PlayCheckpointSfx(){
        aSource.clip = checkpointClip;
        aSource.Play();
    }

    public void PlayCongratsSfx(){
        aSource.clip = congratsClip;
        aSource.Play();
    }


}


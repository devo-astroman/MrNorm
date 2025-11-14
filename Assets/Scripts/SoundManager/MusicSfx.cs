using UnityEngine;

public class MusicSfx : MonoBehaviour
{

    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip musicClip;

    void Start(){
        PlayMusic();
    }

    public void PlayMusic(){
        aSource.clip = musicClip;
        aSource.Play();
    }
}


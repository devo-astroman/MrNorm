using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraManager : MonoBehaviour
{

    [Header("Cameras")]
    [SerializeField]  private CinemachineVirtualCamera cameraHero;

    [Header("Targets")]
    [SerializeField]  private Transform fallingJetpack;


    public void FollowFallingJetpack()
    {
        cameraHero.Follow = fallingJetpack;
    }

    public void FollowHero(Transform hero)
    {   Debug.Log("-----!!!!---FollowHero");
        cameraHero.Follow = hero;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

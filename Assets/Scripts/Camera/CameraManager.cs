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
    {   
        cameraHero.Follow = hero;
    }


}

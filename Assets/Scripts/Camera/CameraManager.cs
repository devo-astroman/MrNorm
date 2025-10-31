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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("References")]
    [SerializeField]  private CameraManager cameraManager;
    [SerializeField]  private Hero hero;


    // Start is called before the first frame update
    void Start()
    {
        hero.OnFinishDeadAnimation += HandleFinishDeadAnimation;
    }

    private void HandleFinishDeadAnimation()
    {
        Debug.Log("HandleFinishDeadAnimation");
        cameraManager.FollowFallingJetpack();
    }

    // Update is called once per frame
    void Update()
    {

    }

}

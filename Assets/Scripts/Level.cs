using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("References")]
    [SerializeField]  private CameraManager cameraManager;
    [SerializeField]  private Hero hero;
    [SerializeField]  private CheckpointManager checkpointManager;
    [SerializeField]  private GameObject heroPrefab;


    private SetTimeoutUtility timeout;

    // Start is called before the first frame update
    void Start()
    {
        timeout = new SetTimeoutUtility(this);
        SetupHero();
    }

    private void SetupHero(){
        hero.OnFinishDeadAnimation += HandleFinishDeadAnimation;
    }

    private void HandleFinishDeadAnimation()
    {
        Debug.Log("HandleFinishDeadAnimation");
        cameraManager.FollowFallingJetpack();
        RespanwnHero();
    }

    private void RespanwnHero()
    {
        timeout.SetTimeout(() => {
            Vector3 position = checkpointManager.GetLastCheckpointPosition();
            Destroy(hero.gameObject);
            GameObject newHero = Instantiate(heroPrefab, position, Quaternion.identity);

            hero = newHero.GetComponent<Hero>();
            SetupHero();
            cameraManager.FollowHero(hero.GetHeroCollider().transform);
            
        }, 2f);

    }


}

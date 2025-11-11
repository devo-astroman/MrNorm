using System;
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
    [SerializeField]  private SoundManager soundManager;
    [SerializeField]  private TakeableItemManager takeableItemManager;
    [SerializeField]  private FinalPresenter finalPresenter;
    

    [Header("Notifiers")]
    public Action OnHeroHurt;
    public Action OnHeroDead;

    private SetTimeoutUtility timeout;

    // Start is called before the first frame update
    void Start()
    {
        timeout = new SetTimeoutUtility(this);
        SetupManagers();
        SetupHero();
    }

    private void SetupManagers(){
        takeableItemManager.OnCoinTake += HandleCoinTake;
        checkpointManager.OnTouchCheckpoint += HandleTouchCheckpoint;
        finalPresenter.OnShowCongrats += HandleShowCongrats;
    }

    private void HandleCoinTake(){
        soundManager.HandleCoinTake();
    }

    private void HandleTouchCheckpoint(){
        soundManager.HandleTouchCheckpoint();
    }

    private void HandleShowCongrats(){
        soundManager.HandleShowCongrats();
    }


    

    private void SetupHero(){
        hero.OnFinishDeadAnimation += HandleFinishDeadAnimation;
        hero.OnHeroHurt += HandleHeroHurt;
        hero.OnHeroDead += OnHeroDead;
    }

    private void HandleHeroHurt(){
        soundManager.HandleHeroHurt();
    }
    
    private void HandleHeroDied(){
        soundManager.HandleHeroDied();
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

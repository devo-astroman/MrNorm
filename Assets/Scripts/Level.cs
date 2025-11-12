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
    [SerializeField] private FinalPresenter finalPresenter;    
    [SerializeField]  private Hud hud;
    
/*
    [Header("Notifiers")]
     public Action OnHeroHurt;
    public Action OnHeroDead; */

    private SetTimeoutUtility timeout;
    private SetTimeoutUtility timeout2;

    // Start is called before the first frame update
    void Start()
    {
        timeout = new SetTimeoutUtility(this);
        timeout2 = new SetTimeoutUtility(this);
        SetupManagers();
        SetupHero();
        SetupHud();
    }

    private void SetupManagers(){
        takeableItemManager.OnCoinTake += HandleCoinTake;
        checkpointManager.OnTouchCheckpoint += HandleTouchCheckpoint;
        finalPresenter.OnShowCongrats += HandleShowCongrats;
    }

    private void HandleCoinTake(){
        soundManager.HandleCoinTake();
        var nCoins = takeableItemManager.GetNCoinsTaken();
        hud.UpdateCoins(nCoins);
    }

    private void HandleTouchCheckpoint(){
        soundManager.HandleTouchCheckpoint();
    }

    private void HandleShowCongrats(){
        soundManager.HandleShowCongrats();

        //restart the level in some time
        RestartGame();
    }


    

    private void SetupHero(){
        hero.OnFinishDeadAnimation += HandleFinishDeadAnimation;
        hero.OnHeroHurt += HandleHeroHurt;
        //hero.OnHeroDead += OnHeroDead;
    }

    private void HandleHeroHurt(){
        soundManager.HandleHeroHurt();
        var health = hero.GetHeroHealth();
        hud.UpdateHearts(health);
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

    private void SetupHud()
    {
        Debug.Log("SetupHud");
        hud.InitHud();
        var nCoins = takeableItemManager.GetNCoinsTaken();
        hud.UpdateCoins(nCoins);
        var health = hero.GetHeroHealth();
        Debug.Log(" health " + health);
        hud.UpdateHearts(health);
    }

    private void RespanwnHero()
    {
        timeout.SetTimeout(() =>
        {
            Vector3 position = checkpointManager.GetLastCheckpointPosition();
            Destroy(hero.gameObject);
            GameObject newHero = Instantiate(heroPrefab, position, Quaternion.identity);

            hero = newHero.GetComponent<Hero>();
            SetupHero();
            cameraManager.FollowHero(hero.GetHeroCollider().transform);

            //this should come from a centralized place
            hud.UpdateHearts(3);

        }, 2f);
    }

    private void RestartGame()
    {
        timeout.SetTimeout(() =>
        {
            //restart checkpoints
            checkpointManager.ResetCheckpoints();            

            //restart coins
            takeableItemManager.ResetTakenCoins();

            //locate player at start point
            Vector3 position = checkpointManager.GetLastCheckpointPosition();

            Destroy(hero.gameObject);
            GameObject newHero = Instantiate(heroPrefab, position, Quaternion.identity);

            hero = newHero.GetComponent<Hero>();
            SetupHero();
            cameraManager.FollowHero(hero.GetHeroCollider().transform);

            //reset Final presenter
            finalPresenter.ResetFinalMessage();

            //reset hud
            var nCoins = takeableItemManager.GetNCoinsTaken();
            hud.UpdateCoins(nCoins);
             //this should come from a centralized place
            hud.UpdateHearts(3);

        }, 6f);

    }


}

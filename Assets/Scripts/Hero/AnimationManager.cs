using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private Animator heroAnimator;    
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private HeroDamageHandler heroDamageHandler;


    [Header("Jetpack")]
    [SerializeField] private HeroJetpackMovement heroJetpackMovement;
    [SerializeField] private Animator jetpackAnimator;
    [SerializeField] private SpriteRenderer jetpackSpriteRenderer;

    [SerializeField] private Transform jetpackLeftPosition;
    [SerializeField] private Transform jetpackRightPosition;
    private bool isActiveLeft = true;

    [SerializeField] private GameObject jetpackOnDead;


    [Header("Settings")]
    [SerializeField] private float movementThreshold = 0.05f; // prevents tiny movement noise


    [Header("Notifiers")]
    public Action OnPlayDead;
    public Action OnFinishDeadAnimation;


    private bool deadAnimationHasEnded = false;

    private void PlayIdle()
    {
        heroAnimator.SetBool("isRunning", false);
        heroAnimator.SetBool("isFlying", false);
        heroAnimator.SetBool("isFalling", false);
    }

    private void PlayRun()
    {
        heroAnimator.SetBool("isRunning", true);
        heroAnimator.SetBool("isFlying", false);
        heroAnimator.SetBool("isFalling", false);
    }

    private void PlayFly()
    {
        heroAnimator.SetBool("isFlying", true);
        heroAnimator.SetBool("isFalling", false);
    }

    private void PlayFall()
    {
        heroAnimator.SetBool("isFlying", false);
        heroAnimator.SetBool("isFalling", true);
        heroAnimator.SetBool("isRunning", false);
    }

    private void PlayJetpackOn()
    {     
        jetpackAnimator.SetBool("isOn",true);
    }

    private void PlayJetpackOff()
    {
        jetpackAnimator.SetBool("isOn",false);
    }

    private void PlayReceiveHit()
    {
        heroAnimator.SetTrigger("receiveDamage");
    }

    private void PlayDead()
    {
        heroAnimator.SetTrigger("isDead");

        OnPlayDead?.Invoke();
    }

    private void PlayFallJetpack(){
        //showing the jetpack on dead
        if(isActiveLeft){
            jetpackOnDead.transform.position = jetpackLeftPosition.position;
            jetpackOnDead.SetActive(true);
            jetpackSpriteRenderer.enabled = false;
        }else{
            jetpackOnDead.transform.position = jetpackRightPosition.position;
            jetpackOnDead.SetActive(true);
            jetpackSpriteRenderer.enabled = false;
        }

    }

    

    private void Flip(float velocityX){

        if(velocityX < 0){
            spriteRenderer.flipX = true;
            jetpackSpriteRenderer.flipX = true;            
            jetpackSpriteRenderer.transform.position = jetpackLeftPosition.position;
            isActiveLeft = true;


        }else if(velocityX > 0){
            spriteRenderer.flipX = false;
            jetpackSpriteRenderer.flipX = false;
            jetpackSpriteRenderer.transform.position = jetpackRightPosition.position;
            isActiveLeft = false;
        }

    }

    private void HandleHealthDown(){
        //fire the hurt animation
        PlayReceiveHit();
    }

    private void HandleDied(){
        //fire the die animation
        PlayDead();
    }

    void Start(){
        heroDamageHandler.OnHealthDown += HandleHealthDown;
        heroDamageHandler.OnDied += HandleDied;
    }


    void Update()
    {
        float velocityX = rb2D.velocity.x;
        float horizontalSpeed = Mathf.Abs(velocityX);

        Flip(velocityX);


        float velocityY = rb2D.velocity.y;
        //float verticalSpeed = Mathf.Abs(velocityY);

        if (velocityY < 0){
            //is falling
            PlayFall();

        }else if (velocityY > 0){
            PlayFly();
        }else{ //(horizontalSpeed == 0)

            if (horizontalSpeed > movementThreshold)
            {
                PlayRun();
            }
            else
            {
                PlayIdle();
            }
        }


        bool isJetpackOn = heroJetpackMovement.GetIsJetpackOn();
        if(isJetpackOn)            
            PlayJetpackOn();
        else
            PlayJetpackOff();

        CheckDeadAnimationHasFinished();
        
    }


    private void CheckDeadAnimationHasFinished(){
        AnimatorStateInfo state = heroAnimator.GetCurrentAnimatorStateInfo(0);

        // Check if THIS specific animation is playing
        if (state.IsName("MN_die"))
        {
            // Check if finished
            if (state.normalizedTime >= 1f && !state.loop && !deadAnimationHasEnded)
            {
                deadAnimationHasEnded = true;
                Debug.Log("MN_die finished!");
                PlayFallJetpack();
                OnFinishDeadAnimation?.Invoke();
            }
        }
        else
        {
            // If animation changed, reset flag
            deadAnimationHasEnded = false;
        }
    }

}

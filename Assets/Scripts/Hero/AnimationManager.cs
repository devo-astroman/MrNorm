using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private Animator heroAnimator;
    
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;


    [Header("Jetpack")]
    [SerializeField] private HeroJetpackMovement heroJetpackMovement;
    [SerializeField] private Animator jetpackAnimator;
    [SerializeField] private SpriteRenderer jetpackSpriteRenderer;

    [SerializeField] private Transform jetpackLeftPosition;
    [SerializeField] private Transform jetpackRightPosition;


    [Header("Settings")]
    [SerializeField] private float movementThreshold = 0.05f; // prevents tiny movement noise

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

    private void Flip(float velocityX){

        if(velocityX < 0){
            spriteRenderer.flipX = true;
            jetpackSpriteRenderer.flipX = true;            
            jetpackSpriteRenderer.transform.position = jetpackLeftPosition.position;


        }else if(velocityX > 0){
            spriteRenderer.flipX = false;
            jetpackSpriteRenderer.flipX = false;
            jetpackSpriteRenderer.transform.position = jetpackRightPosition.position;
        }

    }

    void Update()
    {
        float velocityX = rigidbody2D.velocity.x;
        float horizontalSpeed = Mathf.Abs(velocityX);

        Flip(velocityX);


        float velocityY = rigidbody2D.velocity.y;
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
        
    }


}

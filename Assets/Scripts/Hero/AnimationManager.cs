using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float movementThreshold = 0.05f; // prevents tiny movement noise

    private void PlayIdle()
    {
        heroAnimator.SetBool("isRunning", false);
    }

    private void PlayRun()
    {
        heroAnimator.SetBool("isRunning", true);
    }

    void Update()
    {
        float velocityX = rigidbody2D.velocity.x;
        float horizontalSpeed = Mathf.Abs(velocityX);
        
        if (horizontalSpeed > movementThreshold)
        {
            PlayRun();
        }
        else
        {
            PlayIdle();
        }

        spriteRenderer.flipX = velocityX < 0;

        
    }
}

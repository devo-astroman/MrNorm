using UnityEngine;

public class PCAnimationController : MonoBehaviour
{

    [SerializeField] private Animator animator;


    public void StartFalling(){
        animator.SetBool("isFalling", true);
    }

    public void StopFalling(){
        animator.SetBool("isFalling", false);
    }

    public void StartRunning(){
        animator.SetBool("isRunning", true);
    }

    public void StopRunning(){
        animator.SetBool("isRunning", false);
    }
}

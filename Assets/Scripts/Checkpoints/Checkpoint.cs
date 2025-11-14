using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] int id;

    [Header("References")]
    [SerializeField] CollisionHandler collisionHandler;
    [SerializeField] Animator animator;


    [Header("Notifiers")]
    public Action<int, GameObject> OnChecked;


    // Start is called before the first frame update
    void Start()
    {
        collisionHandler.OnTriggerEnter += HandleTriggerEnter;
    }

    private void HandleTriggerEnter(Collider2D collision, string tag)
    {
        Check();
    }

    public void SetChecked()
    {
        Check();
    }

    private void Check()
    {
        animator.SetBool("checked", true);
        OnChecked?.Invoke(id, gameObject);
        collisionHandler.InactiveCollisions();
    }

    public void Uncheck()
    {
        animator.SetBool("checked", false);
        collisionHandler.ActiveCollisions();
    }
    
    void Destroy()
    {
        collisionHandler.OnTriggerEnter -= HandleTriggerEnter;
    }

}

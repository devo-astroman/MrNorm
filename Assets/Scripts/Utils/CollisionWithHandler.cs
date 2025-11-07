using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionWithHandler: MonoBehaviour
{
    [Header("Target to compare With")]
    [SerializeField] private Collider2D targetCollider2D;


    
    [Header("Notifiers")]
    public UnityEvent FireCollisionEnter;
    public UnityEvent FireTriggerEnter;

    private bool isActive = true;

    public void InactiveCollisions()
    {
        isActive = false;
    }

    public void ActiveCollisions()
    {
        isActive = true;
    }

    public void SetTargetCollider2D(Collider2D coll2D){
        targetCollider2D = coll2D;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter ");
        if (isActive && collision.collider == targetCollider2D)
        {
            FireCollisionEnter?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerenter ");
        if (isActive && collision == targetCollider2D)
        {
            FireTriggerEnter?.Invoke();
        }
    }

    
}

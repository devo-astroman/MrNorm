using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    [Header("Tags to detect")]
    public string[] tags;

    [Header("Player Stats")]
    public Action<Collision2D, string> OnCollisionEnter;
    public Action<Collider2D, string> OnTriggerEnter;

    private bool isActive = true;

    public void InactiveCollisions()
    {
        isActive = false;
    }

    public void ActiveCollisions()
    {
        isActive = true;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && HasTag(collision.collider.tag))
        {
            OnCollisionEnter?.Invoke(collision, collision.collider.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && HasTag(collision.tag))
        {
            OnTriggerEnter?.Invoke(collision, collision.tag);
        }
    }

    private bool HasTag(string tag)
    {
        foreach (var t in tags)
        {
            if (tag == t)
                return true;
        }
        return false;
    }
}

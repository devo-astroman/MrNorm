using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [Header("Tags to detect")]
    public string[] tags;

    [Header("Player Stats")]
    public Action<Collision2D> OnCollisionEnter;
    public Action<Collider2D> OnTriggerEnter;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasTag(collision.collider.tag))
        {
            OnCollisionEnter?.Invoke(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasTag(collision.tag))
        {
            OnTriggerEnter?.Invoke(collision);
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

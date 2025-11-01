using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Check(){
        animator.SetTrigger("check");
        OnChecked?.Invoke(id, gameObject);
        collisionHandler.InactiveCollisions();
    }

}

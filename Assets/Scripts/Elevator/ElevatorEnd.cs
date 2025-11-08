using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnd : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject travelerTarget;
    [SerializeField] private CollisionWithHandler collisionWithHandler;
    

    public void OpenElevator(){
        animator.SetTrigger("open");
    }

    public void CloseElevator(){
        animator.SetTrigger("close");
    }


    public void SetTravelerTarget(GameObject traveler){
        travelerTarget = traveler;
        collisionWithHandler.SetTargetCollider2D(travelerTarget.GetComponent<Collider2D>());
    }
}

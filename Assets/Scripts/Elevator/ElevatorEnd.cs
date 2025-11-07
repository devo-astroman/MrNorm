using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnd : MonoBehaviour
{

    [SerializeField] private Animator animator;

    //[SerializeField] private GameObject travelerPrefab;
    //[SerializeField] private Transform entrance;

    private GameObject activeTraveler = null;

    

    public void OpenElevator(){
        animator.SetTrigger("open");
    }

    public void CloseElevator(){
        animator.SetTrigger("close");
    }


    public void CloneTravelerAtEntrance(){
        
    }
}

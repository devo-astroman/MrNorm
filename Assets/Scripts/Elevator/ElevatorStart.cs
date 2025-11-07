using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStart : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject travelerPrefab;
    [SerializeField] private Transform entrance;

    private GameObject activeTraveler = null;

    

    public void OpenElevator(){
        animator.SetTrigger("open");

        //this should run when the animation "EO_open" finishes
        //CloneTravelerAtEntrance();
    }

    public void CloseElevator(){
        animator.SetTrigger("close");
    }

    public void PlaceAtEntrace(Transform element){
        element.position = entrance.position;
    }


    public void CloneTravelerAtEntrance(){
        activeTraveler = Instantiate(travelerPrefab, entrance.position, Quaternion.identity);
    }

    public void SetTraveler(GameObject traveler){
        activeTraveler = traveler;
    }
}

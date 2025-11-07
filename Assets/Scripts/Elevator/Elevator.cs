using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject travelerPrefab;
    private GameObject activeTraveler = null;


    [SerializeField] private ElevatorStart elevatorStart;
    [SerializeField] private Transform entrance;


    [SerializeField] private ElevatorEnd elevatorEnd;

    private bool isTraveling = false;
    

    public void OpenElevatorStart(){

        elevatorStart.OpenElevator();
    }

    public void CloseElevatorStart(){
        elevatorStart.CloseElevator();
    }


    public void CloneTravelerAtEntrance(){        

        if(!isTraveling){
            activeTraveler = Instantiate(travelerPrefab, Vector2.zero, Quaternion.identity);
            elevatorStart.PlaceAtEntrace(activeTraveler.transform);

            CloseElevatorStart();

            elevatorEnd.SetTravelerTarget(activeTraveler);

            isTraveling = true;
        }        
    }


    public void OpenElevatorEnd(){
        elevatorEnd.OpenElevator();
        HorizontalMovement horizontalMovement = activeTraveler.GetComponent<HorizontalMovement>();
        horizontalMovement.SetHorizontalSpeed(0);
    }

    public void DestroyTraveler(){        

        if(isTraveling){
            Destroy(activeTraveler);
            CloseElevatorEnd();

            isTraveling = false;
        }        
    }

    public void CloseElevatorEnd(){
        elevatorEnd.CloseElevator();
    }
}

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

            isTraveling = true;
        }
        
    }
}

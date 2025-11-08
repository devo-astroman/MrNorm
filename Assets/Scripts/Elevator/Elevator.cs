using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject travelerPrefab;
    private GameObject activeTraveler = null;


    [SerializeField] private ElevatorStart elevatorStart;
    //[SerializeField] private Transform entrance;

    private SetIntervalUtility interval;
    private bool isLooping = false;
    [SerializeField] private float loopTime = 2f;
    [SerializeField] private bool changeEnemyStartEnemyDirection = false;
    private bool flippedEnemyDirection = false;


    [SerializeField] private ElevatorEnd elevatorEnd;

    private bool isTraveling = false;

    void Start()
    {
        interval = new SetIntervalUtility(this);
    }
    
    public void LoopOpenElevator(){

        if(!isLooping){
            isLooping = true;
            OpenElevatorStart();
            interval.SetInterval(() => {
                if(!isTraveling)
                    OpenElevatorStart();
                
            }, loopTime);
        }
    }
    

    public void OpenElevatorStart(){

        if(!isTraveling)
            elevatorStart.OpenElevator();
    }

    public void CloseElevatorStart(){
        elevatorStart.CloseElevator();
    }


    public void CloneTravelerAtEntrance(){        

        if(!isTraveling){
            //activeTraveler = Instantiate(travelerPrefab, Vector2.zero, Quaternion.identity);
            activeTraveler = CreateTraveler();
            elevatorStart.PlaceAtEntrace(activeTraveler.transform);

            CloseElevatorStart();

            elevatorEnd.SetTravelerTarget(activeTraveler);

            isTraveling = true;
        }        
    }

    private GameObject CreateTraveler(){
        activeTraveler = Instantiate(travelerPrefab, Vector2.zero, Quaternion.identity);

        if(changeEnemyStartEnemyDirection){
            HorizontalMovement horizontalMovement = activeTraveler.GetComponent<HorizontalMovement>();

            SpriteFlipperX spriteFlipperX = activeTraveler.GetComponent<SpriteFlipperX>();

            if(flippedEnemyDirection){
                horizontalMovement.InvertSpeed();
                spriteFlipperX.FlipX();
                
            }

            flippedEnemyDirection = !flippedEnemyDirection;

        }

        return activeTraveler;
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

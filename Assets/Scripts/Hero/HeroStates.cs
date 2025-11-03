using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStates : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private HeroDamageHandler heroDamageHandler;
    //[SerializeField] private HeroHorizontalMovement heroHorizontalMovement;
    [SerializeField] private HeroJetpackMovement heroJetpackMovement;

    private void HandleDied(){
        //heroHorizontalMovement.enabled =false;
        heroJetpackMovement.enabled =false;
    }

    void Start(){
        heroDamageHandler.OnDied += HandleDied;
    }


    void Update()
    {
        
    }


}

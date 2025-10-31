using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private HeroHorizontalMovement heroHorizontalMovement;
    [SerializeField] private AnimationManager animationManager;


    [Header("Notifiers")]
    public Action OnPlayDead;

    // Start is called before the first frame update
    void Start()
    {
        animationManager.OnPlayDead += OnPlayDead; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

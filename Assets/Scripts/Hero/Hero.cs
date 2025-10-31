using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private HeroHorizontalMovement heroHorizontalMovement;
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private Collider2D heroCollider;


    [Header("Notifiers")]
    public Action OnPlayDead;
    public Action OnFinishDeadAnimation;

    public Collider2D GetHeroCollider(){
        return heroCollider;
    }


    public void InactiveHero(){
        gameObject.SetActive(false);
    }

    public void RespawnAt(Vector3 position){        
        transform.position = position;
        gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        animationManager.OnFinishDeadAnimation += OnFinishDeadAnimation; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public struct DependenciesSawDisc
{
    public SetTimeoutUtility timeoutWaitToShow;
    public SetTimeoutUtility timeoutWaitToHide;
    public Animator animator;
    public Collider2D collider2D;
    public float timeToSpin;
    public float timeToStopSpin;
}

public class SawDiscFSM : AbstractFiniteStateMachine
{   

    [Header("References")]
    [SerializeField]  private Animator animator;
    [SerializeField]  private Collider2D coll2D;
    [SerializeField]  private float timeToSpin = 2f;
    [SerializeField]  private float timeToStopSpin = 2f;

    

    public DependenciesSawDisc deps = new DependenciesSawDisc
    {
        timeoutWaitToShow = null,
        timeoutWaitToHide = null,
        animator = null,
        collider2D = null,
        timeToSpin = 2f,
        timeToStopSpin = 2f,
    };


    public enum States
    {
        NO_SPIN,
        SPIN
    }
    private void Awake()
    {
        deps.timeoutWaitToShow = new SetTimeoutUtility(this);
        deps.timeoutWaitToHide = new SetTimeoutUtility(this);
        deps.animator = animator;
        deps.collider2D = coll2D;
        deps.timeToSpin = timeToSpin;
        deps.timeToStopSpin = timeToStopSpin;

        NoSpinState hide = AbstractState.Create<NoSpinState, States>(States.NO_SPIN, this);
        hide.Setup(ref deps);

        SpinState show = AbstractState.Create<SpinState, States>(States.SPIN, this);
        show.Setup(ref deps);


        Init(States.NO_SPIN,
            hide,
            show
        );
    }

    public class NoSpinState : AbstractState
    {
        private DependenciesSawDisc deps;

        public void Setup(ref DependenciesSawDisc dependencies) {
            deps = dependencies;
        }


        public override void OnEnter()
        {
            
            deps.animator.SetBool("spin",true);
            deps.timeoutWaitToShow.SetTimeout(() => {
                deps.collider2D.tag = "Damager_25";
                TransitionToState(States.SPIN);                
            }, deps.timeToSpin);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()        
        {
            
        }
    }
    public class SpinState : AbstractState
    {
        private DependenciesSawDisc deps;

        public void Setup(ref DependenciesSawDisc dependencies) {
            deps = dependencies;
        }

        private void Awake()
        {
        }

        public override void OnEnter()
        {
            
            deps.animator.SetBool("spin",false);
            deps.timeoutWaitToHide.SetTimeout(() => {
                deps.collider2D.tag = "Damager_50";
                TransitionToState(States.NO_SPIN);
                
            }, deps.timeToStopSpin);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {

        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public struct Dependencies
{
    public SetTimeoutUtility timeoutWaitToShow;
    public SetTimeoutUtility timeoutWaitToHide;
    public Animator animator;
    public Collider2D collider2D;
    public float timeToShow;
    public float timeToHide;
}

public class PinsFSM : AbstractFiniteStateMachine
{   

    [Header("References")]
    [SerializeField]  private Animator animator;
    [SerializeField]  private Collider2D coll2D;
    [SerializeField]  private float timeToShow = 2f;
    [SerializeField]  private float timeToHide = 2f;

    

    public Dependencies deps = new Dependencies
    {
        timeoutWaitToShow = null,
        timeoutWaitToHide = null,
        animator = null,
        collider2D = null,
        timeToShow = 2f,
        timeToHide = 2f,
    };


    public enum States
    {
        STATE_IDLEHIDE,
        STATE_IDLESHOW
    }
    private void Awake()
    {
        deps.timeoutWaitToShow = new SetTimeoutUtility(this);
        deps.timeoutWaitToHide = new SetTimeoutUtility(this);
        deps.animator = animator;
        deps.collider2D = coll2D;
        deps.timeToShow = timeToShow;
        deps.timeToHide = timeToHide;

        StateIdlehideState hide = AbstractState.Create<StateIdlehideState, States>(States.STATE_IDLEHIDE, this);
        hide.Setup(ref deps);

        StateIdleshowState show = AbstractState.Create<StateIdleshowState, States>(States.STATE_IDLESHOW, this);
        show.Setup(ref deps);


        Init(States.STATE_IDLEHIDE,
            hide,
            show
        );
    }

    public class StateIdlehideState : AbstractState
    {
        private Dependencies deps;

        public void Setup(ref Dependencies dependencies) {
            deps = dependencies;
        }


        public override void OnEnter()
        {
            
            deps.animator.SetBool("show",true);
            deps.timeoutWaitToShow.SetTimeout(() => {
                deps.collider2D.enabled = false;
                TransitionToState(States.STATE_IDLESHOW);                
            }, deps.timeToShow);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()        
        {
            
        }
    }
    public class StateIdleshowState : AbstractState
    {
        private Dependencies deps;

        public void Setup(ref Dependencies dependencies) {
            deps = dependencies;
        }

        private void Awake()
        {
        }

        public override void OnEnter()
        {
            
            deps.animator.SetBool("show",false);
            deps.timeoutWaitToHide.SetTimeout(() => {
                deps.collider2D.enabled = true;
                TransitionToState(States.STATE_IDLEHIDE);
                
            }, deps.timeToHide);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {

        }
    }

}

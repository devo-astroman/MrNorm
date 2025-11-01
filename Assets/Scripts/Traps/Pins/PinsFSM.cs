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
}

public class PinsFSM : AbstractFiniteStateMachine
{   

    [SerializeField]  private Animator animator;

    public Dependencies deps = new Dependencies
    {
        timeoutWaitToShow = null,
        timeoutWaitToHide = null,
        animator = null
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
                 TransitionToState(States.STATE_IDLESHOW);
                
            }, 2f);
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
        //    timeoutWaitToHide = new SetTimeoutUtility(this);
        }

        public override void OnEnter()
        {
            
            deps.animator.SetBool("show",false);
            deps.timeoutWaitToHide.SetTimeout(() => {
                 TransitionToState(States.STATE_IDLEHIDE);
                
            }, 2f);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            
        }
    }

}

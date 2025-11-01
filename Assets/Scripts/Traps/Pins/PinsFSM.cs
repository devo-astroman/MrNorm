using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class PinsFSM : AbstractFiniteStateMachine
{   

    private SetTimeoutUtility timeoutWaitToShow;
    private SetTimeoutUtility timeoutWaitToHide;

    public enum States
    {
        STATE_IDLEHIDE,
        STATE_IDLESHOW
    }
    private void Awake()
    {
        timeoutWaitToShow = new SetTimeoutUtility(this);
        timeoutWaitToHide = new SetTimeoutUtility(this);

        StateIdlehideState hide = AbstractState.Create<StateIdlehideState, States>(States.STATE_IDLEHIDE, this);
        hide.Setup(timeoutWaitToShow);

        StateIdleshowState show = AbstractState.Create<StateIdleshowState, States>(States.STATE_IDLESHOW, this);
        show.Setup(timeoutWaitToHide);


        Init(States.STATE_IDLEHIDE,
            hide,
            show
        );
    }

    public class StateIdlehideState : AbstractState
    {
        private SetTimeoutUtility timeoutWaitToShow;

        public void Setup(SetTimeoutUtility timeout) => timeoutWaitToShow = timeout;


        public override void OnEnter()
        {
            Debug.Log("IdleHide OnEnter");
            timeoutWaitToShow.SetTimeout(() => {
                 TransitionToState(States.STATE_IDLESHOW);
                
            }, 2f);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()        
        {
            Debug.Log("IdleHide OnExit");
        }
    }
    public class StateIdleshowState : AbstractState
    {
        private SetTimeoutUtility timeoutWaitToHide;
        

        public void Setup(SetTimeoutUtility timeout) => timeoutWaitToHide = timeout;

        private void Awake()
        {
        //    timeoutWaitToHide = new SetTimeoutUtility(this);
        }

        public override void OnEnter()
        {
            Debug.Log("IdleShow OnEnter");
            timeoutWaitToHide.SetTimeout(() => {
                 TransitionToState(States.STATE_IDLEHIDE);
                
            }, 2f);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            Debug.Log("IdleShow OnExit");
        }
    }

}

using UnityEngine;
using UnityEngine.Events;

public class ElevatorAnimationsEventHandler : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent fireAnimationStarts;
    public UnityEvent fireAnimationEnds;
    public UnityEvent onElevatorOpened;

    //Events of the animation
    public void OnAnimationStartsEvent(){
        fireAnimationStarts?.Invoke();
    }

    public void OnAnimationEndsEvent(){
        fireAnimationEnds?.Invoke();
    }


    public void OnElevatorOpened(){
        onElevatorOpened?.Invoke();
    }
}

using UnityEngine;

public class ElevatorStart : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject travelerPrefab;
    [SerializeField] private Transform entrance;

    public void OpenElevator(){
        animator.SetTrigger("open");
    }

    public void CloseElevator(){
        animator.SetTrigger("close");
    }

    public void PlaceAtEntrace(Transform element){
        element.position = entrance.position;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class FloorCheck : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform floorCheckPoint;

    [Header("Settings")]
    [SerializeField] private float checkDistance = 0.3f;
    [SerializeField] private LayerMask floorLayer;

    [Header("Events")]
    public UnityEvent OnFloorEnter; // when starts touching floor
    public UnityEvent OnFloorExit;  // when stops touching floor

    private bool wasOnFloor = false;

    void FixedUpdate()
    {
        if (floorCheckPoint == null)
            return;

        bool isOnFloor = Physics2D.Raycast(
            floorCheckPoint.position,
            Vector2.down,
            checkDistance,
            floorLayer
        );

        // Detect transitions:
        if (isOnFloor && !wasOnFloor)
        {
            OnFloorEnter?.Invoke();
        }
        else if (!isOnFloor && wasOnFloor)
        {
            OnFloorExit?.Invoke();
        }

        wasOnFloor = isOnFloor;

        // Debug visualization
        Debug.DrawRay(floorCheckPoint.position,
                      Vector2.down * checkDistance,
                      isOnFloor ? Color.green : Color.red);
    }
}

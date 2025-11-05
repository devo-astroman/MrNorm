using UnityEngine;
using UnityEngine.Events;

public class ObstacleCheck : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform obstacleChecker;

    [Header("Detection Settings")]
    [SerializeField] private float checkDistance = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private bool right = false;
    [SerializeField] private bool left = false;
    [SerializeField] private bool up = false;
    [SerializeField] private bool down = false;


    [SerializeField] private bool checkAbsence = false;


    [Header("Events")]
    public UnityEvent OnObstacleDetected;

    private bool obstaclePreviouslyDetected = false;


    private Vector2 GetDirection(){

        if(right)   return obstacleChecker.right;
        else if(left)    return obstacleChecker.right*-1;
        else if(up)    return obstacleChecker.up;
        
        return obstacleChecker.up*-1;
    }


    void FixedUpdate()
    {
        if (obstacleChecker == null)
            return;

        // Cast a ray forward from the checker
        bool hit = Physics2D.Raycast(
            obstacleChecker.position,
            GetDirection(),
            checkDistance,
            obstacleLayer
        );

        if(checkAbsence) hit = !hit;

        // if obstacle detected now but wasn't before → fire event
        if (hit && !obstaclePreviouslyDetected)
        {
            obstaclePreviouslyDetected = true;
            OnObstacleDetected?.Invoke();
        }

        // Reset state when obstacle is no longer detected
        if (!hit)
        {
            obstaclePreviouslyDetected = false;
        }

        // **Visual Debug**
        Debug.DrawRay(obstacleChecker.position, GetDirection() * checkDistance, hit ? Color.red : Color.black);
    }
}

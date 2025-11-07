using UnityEngine;
using UnityEngine.Events;

public class HorizontalStateNotifier : MonoBehaviour
{
 [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float velocityThreshold = 0.01f; 
    // Helps avoid floating point jitter

    [Header("Events")]
    public UnityEvent OnStartMoving;
    public UnityEvent OnStopMoving;

    private bool wasMoving = false;

    void FixedUpdate()
    {
        if (rb == null) return;

        // Check horizontal velocity magnitude
        bool isMoving = Mathf.Abs(rb.velocity.x) > velocityThreshold;

        // Detect transitions
        if (isMoving && !wasMoving)
        {
            OnStartMoving?.Invoke();
        }
        else if (!isMoving && wasMoving)
        {
            OnStopMoving?.Invoke();
        }

        wasMoving = isMoving;
    }
}

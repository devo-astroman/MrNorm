using UnityEngine;

public class SimpleDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Tooltip("Empty object placed in front of the player")]
    [SerializeField] private Transform wallCheck;

    [Tooltip("Layer(s) considered walls/obstacles")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float wallCheckDistance = 0.3f;

    private bool isDashing = false;
    private Vector2 dashTarget;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isDashing)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (!isDashing) return;

        // ✅ Stop dash if there is a wall in front
        if (Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer))
        {
            StopDash();
            return;
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, dashTarget, dashSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, dashTarget) < 0.05f)
        {
            StopDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTarget = rb.position + Vector2.right * dashDistance;
    }

    private void StopDash()
    {
        isDashing = false;
        // Optional: Stop movement instantly
        rb.velocity = Vector2.zero;
    }
}

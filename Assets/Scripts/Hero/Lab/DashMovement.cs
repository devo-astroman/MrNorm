using UnityEngine;

public class DashMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Tooltip("Wall check empties in each direction")]
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private Transform upWallCheck;
    [SerializeField] private Transform downWallCheck;

    [Tooltip("Layer(s) considered walls/obstacles")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float wallCheckDistance = 0.3f;
    [SerializeField] private float dashCooldown = 0.35f; // seconds

    [Tooltip("Hard limit for how long a dash can last, in seconds")]
    [SerializeField] private float dashMaxDuration = 0.20f; // NEW: timeout

    private enum Dir { None, Left, Right, Up, Down }

    private bool isDashing = false;
    private Vector2 dashTarget;
    private Dir currentDir = Dir.None;
    private float nextDashTime = 0f;

    // NEW: absolute time when dash should stop
    private float dashEndTime = 0f;

    void Update()
    {
        // Keys: 1=Left, 2=Right, 3=Up, 4=Down
        if (Input.GetKeyDown(KeyCode.Alpha1)) DashToLeft();
        if (Input.GetKeyDown(KeyCode.Alpha2)) DashToRight();
        if (Input.GetKeyDown(KeyCode.Alpha3)) DashToUp();
        if (Input.GetKeyDown(KeyCode.Alpha4)) DashToDown();
    }

    void FixedUpdate()
    {
        if (!isDashing) return;

        // NEW: stop if we exceeded max duration
        if (Time.time >= dashEndTime)
        {
            StopDash();
            return;
        }

        // Stop dash early if a wall appears in front
        if (IsWallInFront(currentDir))
        {
            StopDash();
            return;
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, dashTarget, dashSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, dashTarget) <= 0.05f)
        {
            StopDash();
        }
    }

    #region Public Dash API (callable externally)
    public void DashToLeft()  => TryStartDash(Dir.Left);
    public void DashToRight() => TryStartDash(Dir.Right);
    public void DashToUp()    => TryStartDash(Dir.Up);
    public void DashToDown()  => TryStartDash(Dir.Down);
    #endregion

    private void TryStartDash(Dir dir)
    {
        if (isDashing) return;
        if (Time.time < nextDashTime) return;

        Vector2 dirVec = ToVector(dir);
        if (dirVec == Vector2.zero) return;

        // If there's a wall immediately in front, don't start
        if (IsWallInFront(dir)) return;

        // Precompute target; clamp to wall if one is within dashDistance
        float allowedDistance = GetFreeDistance(dir, dashDistance);
        dashTarget = rb.position + dirVec * allowedDistance;

        currentDir = dir;
        isDashing = true;

        // NEW: set absolute dash deadline (timeout)
        dashEndTime = Time.time + Mathf.Max(0f, dashMaxDuration);

        // cooldown starts at dash begin (keeps behavior simple/predictable)
        nextDashTime = Time.time + dashCooldown;
    }

    private void StopDash()
    {
        isDashing = false;
        currentDir = Dir.None;
        rb.velocity = Vector2.zero; // hard stop (optional)
    }

    private bool IsWallInFront(Dir dir)
    {
        Transform origin = GetCheckTransform(dir);
        Vector2 dirVec = ToVector(dir);
        if (origin == null || dirVec == Vector2.zero) return false;

        return Physics2D.Raycast(origin.position, dirVec, wallCheckDistance, wallLayer);
    }

    /// <summary>
    /// Raycasts ahead (from the corresponding wall check) up to maxDist
    /// and returns the maximum free distance before a wall. Uses a small
    /// skin to avoid intersecting the collider at the end.
    /// </summary>
    private float GetFreeDistance(Dir dir, float maxDist)
    {
        const float skin = 0.05f;
        Transform origin = GetCheckTransform(dir);
        Vector2 dirVec = ToVector(dir);

        if (origin == null) return maxDist;

        RaycastHit2D hit = Physics2D.Raycast(origin.position, dirVec, maxDist, wallLayer);
        if (hit.collider == null) return maxDist;

        float d = Mathf.Max(0f, hit.distance - skin);
        return Mathf.Min(d, maxDist);
    }

    private Transform GetCheckTransform(Dir dir)
    {
        switch (dir)
        {
            case Dir.Left:  return leftWallCheck;
            case Dir.Right: return rightWallCheck;
            case Dir.Up:    return upWallCheck;
            case Dir.Down:  return downWallCheck;
            default:        return null;
        }
    }

    private static Vector2 ToVector(Dir dir)
    {
        switch (dir)
        {
            case Dir.Left:  return Vector2.left;
            case Dir.Right: return Vector2.right;
            case Dir.Up:    return Vector2.up;
            case Dir.Down:  return Vector2.down;
            default:        return Vector2.zero;
        }
    }

#if UNITY_EDITOR
    // Optional gizmos to visualize checks and target
    private void OnDrawGizmosSelected()
    {
        DrawCheckGizmo(leftWallCheck, Vector2.left);
        DrawCheckGizmo(rightWallCheck, Vector2.right);
        DrawCheckGizmo(upWallCheck, Vector2.up);
        DrawCheckGizmo(downWallCheck, Vector2.down);

        if (isDashing)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(dashTarget, 0.1f);
        }
    }

    private void DrawCheckGizmo(Transform t, Vector2 dir)
    {
        if (!t) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(t.position, (Vector2)t.position + dir.normalized * wallCheckDistance);
    }
#endif
}

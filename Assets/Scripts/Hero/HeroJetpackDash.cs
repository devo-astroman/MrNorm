using System.Collections;
using UnityEngine;

public class HeroJetpackDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private HeroJetpackMovement jetpack; // for GetIsJetpackOn()
    [SerializeField] private GroundCheck groundCheck;     // for IsGrounded

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashDuration = 0.18f;
    [SerializeField] private float dashCooldown = 0.35f;
    [SerializeField, Tooltip("Max time allowed between taps")]
    private float doubleTapWindow = 0.25f;
    [SerializeField] private bool allowVerticalDash = true;   // W/S
    [SerializeField] private bool requireJetpackOn = true;    // gate can be turned off for testing

    public bool IsDashing { get; private set; }

    float lastTapW = -999f, lastTapA = -999f, lastTapS = -999f, lastTapD = -999f;
    float lastDashTime = -999f;
    Vector2 dashDir;

    void Update()
    {
        if (IsDashing) return;

        // ---- Diagnostics for gates ----
        if (rb2D == null) { Debug.LogWarning("[Dash] Missing rb2D"); return; }
        if (jetpack == null) { Debug.LogWarning("[Dash] Missing jetpack ref"); return; }
        if (groundCheck == null) { Debug.LogWarning("[Dash] Missing groundCheck ref"); return; }

        if (requireJetpackOn && !jetpack.GetIsJetpackOn())
        {
            // You must be pressing W (your jetpack code sets engineOn while Vertical>0)
            // Debug.Log("[Dash] Gate: jetpack not on");
            return;
        }

        if (groundCheck.IsGrounded)
        {
            // Debug.Log("[Dash] Gate: grounded");
            return;
        }

        if (Time.time < lastDashTime + dashCooldown)
        {
            // Debug.Log("[Dash] Gate: cooldown");
            return;
        }

        // ---- Double tap detection ----
        if (CheckDoubleTap(KeyCode.D, ref lastTapD))      TryStartDash(Vector2.right);
        else if (CheckDoubleTap(KeyCode.A, ref lastTapA)) TryStartDash(Vector2.left);
        else if (allowVerticalDash && CheckDoubleTap(KeyCode.W, ref lastTapW)) TryStartDash(Vector2.up);
        else if (allowVerticalDash && CheckDoubleTap(KeyCode.S, ref lastTapS)) TryStartDash(Vector2.down);
    }

    bool CheckDoubleTap(KeyCode key, ref float lastTapTime)
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log($"[Dash] Tap {key} at {Time.time:F2} (last {lastTapTime:F2})");
            if (Time.time - lastTapTime <= doubleTapWindow)
            {
                Debug.Log($"[Dash] DOUBLE-TAP {key}");
                return true;
            }
            lastTapTime = Time.time; // first tap recorded
        }
        return false;
    }

    void TryStartDash(Vector2 dir)
    {
        Debug.Log("[Dash] TryStartDash " + dir);
        dashDir = dir.normalized;
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        IsDashing = true;
        lastDashTime = Time.time;

        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;

        float tEnd = Time.time + dashDuration;
        while (Time.time < tEnd)
        {
            rb2D.velocity = dashDir * dashSpeed;
            yield return null;
        }

        rb2D.gravityScale = originalGravity;
        IsDashing = false;
    }

#if UNITY_EDITOR
    // Quick on-screen debug for timers (optional)
    void OnGUI()
    {
        GUI.Label(new Rect(10, 80, 400, 20), $"W/A/S/D last taps: {lastTapW:F2} / {lastTapA:F2} / {lastTapS:F2} / {lastTapD:F2}");
        GUI.Label(new Rect(10,100, 400, 20), $"JetpackOn: {jetpack?.GetIsJetpackOn()}  Grounded: {groundCheck?.IsGrounded}  CooldownLeft: {Mathf.Max(0,(lastDashTime+dashCooldown)-Time.time):F2}");
    }
#endif
}

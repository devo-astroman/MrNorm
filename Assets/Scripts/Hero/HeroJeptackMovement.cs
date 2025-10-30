using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJetpackMovement : MonoBehaviour
{
    [Header("References")]    
    [SerializeField] private Rigidbody2D rigidbody2D;          // Upward force while holding Ctrl

    [Header("Jetpack")]
    [SerializeField] private float thrustForce = 15f;          // Upward force while holding Ctrl
    [SerializeField] private float horizontalSpeed = 5f;        // Optional left/right move (A/D or arrows)
    [SerializeField] private float maxVerticalSpeed = 10f;      // Clamp vertical velocity
    [SerializeField] private float fuelSeconds = 999f;          // Seconds of thrust available (set big for unlimited)
    [SerializeField] private float fuelUsePerSecond = 1f;       // Fuel consumption when thrusting
    [SerializeField] private float fuelRegenPerSecond = 0.3f;   // Fuel regen when not thrusting (set 0 to disable)

    [Header("Feel")]
    [SerializeField] private float tiltAmount = 10f;            // Visual tilt when moving horizontally
    [SerializeField] private float tiltLerp = 8f;

    

    /* [SerializeField] private Animator jetBackpackAnimator;
    [SerializeField] private Animator jetBackpackAnimator2;
    [SerializeField] private Animator bodyAnimator; */

    public bool isJetpackOn = false;


    /* private Rigidbody2D rb; */
    private float fuel;

    void Awake()
    {
      /*   rb = GetComponent<Rigidbody2D>();
        fuel = fuelSeconds; */
        // Optional: slightly higher gravity for snappier feel (tune in Inspector)
        // rb.gravityScale = 2f;
    }

    void Update()
    {
        // Horizontal input (optional)
        float x = Input.GetAxisRaw("Horizontal"); // A/D or arrows
        Vector2 v = rigidbody2D.velocity;
        v.x = x * horizontalSpeed;
        rigidbody2D.velocity = new Vector2(v.x, rigidbody2D.velocity.y);

        // Visual tilt based on horizontal movement
        float targetZ = -x * tiltAmount;
        float z = Mathf.LerpAngle(transform.eulerAngles.z, targetZ, Time.deltaTime * tiltLerp);
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }

    void FixedUpdate()
    {
        
        bool thrusting = Input.GetKey(KeyCode.W);

        if (thrusting && fuel > 0f)
        {
            isJetpackOn = true;

            // Apply continuous upward force while Ctrl is held
            rigidbody2D.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);

            // Consume fuel (FixedUpdate: use deltaTime to be time-consistent)
            fuel = Mathf.Max(0f, fuel - fuelUsePerSecond * Time.fixedDeltaTime);
        }
        else
        {
            isJetpackOn = false;
            
            // Regenerate fuel when not thrusting
            if (fuelRegenPerSecond > 0f)
                fuel = Mathf.Min(fuelSeconds, fuel + fuelRegenPerSecond * Time.fixedDeltaTime);
        }

        // Clamp vertical speed for control
        if (rigidbody2D.velocity.y > maxVerticalSpeed)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxVerticalSpeed);
    }

    // Optional helper to refill from other scripts / pickups
    public void RefillFuel(float seconds) => fuel = Mathf.Clamp(fuel + seconds, 0f, fuelSeconds);

    // For quick HUD/debug
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 30), $"Fuel: {fuel:0.0}/{fuelSeconds:0.0}");
        GUI.Label(new Rect(10, 30, 250, 30), $"Vel: {rigidbody2D.velocity}");
        GUI.Label(new Rect(10, 50, 250, 30), "Hold CTRL to thrust");
    }

    public bool GetIsJetpackOn(){
        return isJetpackOn;
    }
}

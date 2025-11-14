using UnityEngine;

public class HeroJetpackMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2D;          // Upward force while holding Ctrl
    [SerializeField] private PhysicsMaterial2D pm2D;          // No friction material
    [SerializeField] private GroundCheck groundCheck;


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


    private bool engineOn = false;    

    private float fuel;

    void Start(){
        groundCheck.OnGround += RemoveNoFrictionMaterial;
        groundCheck.OnGroundOff += AddNoFrictionMaterial; 
    }

    private void AddNoFrictionMaterial(){
        rb2D.sharedMaterial = pm2D;
    }

    private void RemoveNoFrictionMaterial(){
        rb2D.sharedMaterial = null;
    }

    void Update()
    {
        // Horizontal input (optional)
        float x = Input.GetAxisRaw("Horizontal"); // A/D or arrows
        Vector2 v = rb2D.velocity;
        v.x = x * horizontalSpeed;
        rb2D.velocity = new Vector2(v.x, rb2D.velocity.y);

        // Visual tilt based on horizontal movement
        float targetZ = -x * tiltAmount;
        float z = Mathf.LerpAngle(transform.eulerAngles.z, targetZ, Time.deltaTime * tiltLerp);
        transform.rotation = Quaternion.Euler(0f, 0f, z);


        float y = Input.GetAxisRaw("Vertical"); // w/s or arrows
        engineOn = y > 0;

    }


    void FixedUpdate()
    {
        
        if (engineOn && fuel > 0f)
        {

            // Apply continuous upward force while Ctrl is held
            rb2D.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);

            // Consume fuel (FixedUpdate: use deltaTime to be time-consistent)
            fuel = Mathf.Max(0f, fuel - fuelUsePerSecond * Time.fixedDeltaTime);
        }
        else
        {
            // Regenerate fuel when not thrusting
            if (fuelRegenPerSecond > 0f)
                fuel = Mathf.Min(fuelSeconds, fuel + fuelRegenPerSecond * Time.fixedDeltaTime);
        }

        // Clamp vertical speed for control
        if (rb2D.velocity.y > maxVerticalSpeed)
            rb2D.velocity = new Vector2(rb2D.velocity.x, maxVerticalSpeed);
    }

    // Optional helper to refill from other scripts / pickups
    public void RefillFuel(float seconds) => fuel = Mathf.Clamp(fuel + seconds, 0f, fuelSeconds);

    public bool GetIsJetpackOn(){
        return engineOn;
    }

    public void TurnJetpackOff(){
        engineOn = false;
    }

    void OnDisable(){
        engineOn = false;        
    }
}

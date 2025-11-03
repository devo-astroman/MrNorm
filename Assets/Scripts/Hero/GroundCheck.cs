using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform groundCheck;     
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    private bool previousGroundedState = false; // Stores the last state

    [Header("Events")]
    public Action OnGround;
    public Action OnGroundOff;

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        // Check if touching ground
        bool currentlyGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        // Draw debug ray
        Debug.DrawRay(groundCheck.position, Vector2.down * checkDistance, 
            currentlyGrounded ? Color.green : Color.red);


        // If the state changed → notify once
        if (currentlyGrounded != previousGroundedState)
        {
            if (currentlyGrounded)
                OnGround?.Invoke();
            else
                OnGroundOff?.Invoke();

            // Store latest state
            previousGroundedState = currentlyGrounded;
        }

        
        IsGrounded = currentlyGrounded;
    }
}

using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField]  private Rigidbody2D rb;

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 5f;

    public void SetHorizontalSpeed(float hSpeed)
    {
        horizontalSpeed = hSpeed;
    }

    public void InvertSpeed()
    {
        horizontalSpeed *= -1;
    }

    void Update()
    {
        float x = 1;
        Vector2 v = rb.velocity;
        v.x = x * horizontalSpeed;
        rb.velocity = new Vector2(v.x, rb.velocity.y);
    }
}

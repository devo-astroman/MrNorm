using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroHorizontalMovement : MonoBehaviour
{

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 5f;        // Optional left/right move (A/D or arrows)

    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal input (optional)
        float x = Input.GetAxisRaw("Horizontal"); // A/D or arrows
        Vector2 v = rb.velocity;
        v.x = x * horizontalSpeed;
        rb.velocity = new Vector2(v.x, rb.velocity.y);
        
    }
}

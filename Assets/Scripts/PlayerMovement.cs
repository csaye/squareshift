using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public float movementForce, jumpForce;

    public float maxVelocity;

    [Range(0, 1)] public float friction;
    [Range(0, 1)] public float airResistance;

    // The movement vector of the player
    private Vector2 movement;

    private float frictionFactor, airResistanceFactor;

    // private bool leftRight, rightLeft;

    void Start()
    {
        // Invert friction factor
        frictionFactor = Mathf.Abs(friction - 1);

        // Invert air resistance factor
        airResistanceFactor = Mathf.Abs(airResistance - 1);
    }

    void Update()
    {
        UpdateMovement();
    }

    void FixedUpdate() {
        
        // If traveling in correct direction, reset parameter
        // if (rb.velocity.x > 0) leftRight = false;
        // if (rb.velocity.x < 0) rightLeft = false;

        // Left and right movement using a and d
        if (Input.GetKey("a")) {
            rb.AddForce(new Vector2(-1 * movementForce * Time.deltaTime, 0));
        }
        if (Input.GetKey("d")) {
            rb.AddForce(new Vector2(movementForce * Time.deltaTime, 0));
        }

        // If no inputs, both inputs, or conflicting inputs, slow
        if ((Input.GetKey("a") == Input.GetKey("d")) || (rb.velocity.x > 0 && Input.GetKey("a") || (rb.velocity.x < 0 && Input.GetKey("d")))) {

            // If on ground
            if (rb.velocity.y == 0) {

                // Slow by friction
                rb.velocity = new Vector2(rb.velocity.x * frictionFactor, rb.velocity.y);
            
            // If in air
            } else {
                
                // Slow by air resistance
                rb.velocity = new Vector2(rb.velocity.x * airResistanceFactor, rb.velocity.y);
            }

        }
        
        // If too fast, cap velocity
        if (rb.velocity.x > maxVelocity) rb.velocity = new Vector2(maxVelocity, rb.velocity.y);

    }

    void UpdateMovement() {

        // If on ground and space key pressed, jump
        if (rb.velocity.y == 0 && Input.GetKeyDown("space")) rb.AddForce(new Vector2(0, jumpForce));
    }
}

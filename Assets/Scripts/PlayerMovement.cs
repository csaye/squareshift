using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static Vector2 playerCenter;

    public Rigidbody2D rb;

    public float movementForce, jumpForce;

    public float maxVelocity, minVelocity;

    [Range(0, 1)] public float jumpKill;

    [Range(0, 1)] public float friction;
    [Range(0, 1)] public float airResistance;

    // The movement vector of the player
    private Vector2 movement;

    private float frictionFactor, airResistanceFactor;

    // Allows for early space bar presses to trigger jumps
    private float spaceDelay, spaceDelayDefault = 20;

    // Alows for late space bar presses to trigger jumps
    private float groundDelay, groundDelayDefault = 10;

    void Start()
    {
        // Invert friction factor
        frictionFactor = Mathf.Abs(friction - 1);

        // Invert air resistance factor
        airResistanceFactor = Mathf.Abs(airResistance - 1);
    }

    void Update()
    {
        
        // If the player is not currently shifting and level not complete
        if (!PlayerShifting.shifting && !LevelBounds.levelComplete) UpdateMovement();
    }

    void FixedUpdate() {

        // If the player is not currently shifting and level not complete
        if (!PlayerShifting.shifting && !LevelBounds.levelComplete) {

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
                if (grounded()) {

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

        } else {

            // If player is shifting, cancel velocity
            rb.velocity = Vector2.zero;
        }

    }

    void UpdateMovement() {

        // If the space bar is pressed, reset the space bar delay
        if (Input.GetKeyDown("space")) spaceDelay = spaceDelayDefault;

        // If grounded, reset the grounded delay
        if (grounded()) groundDelay = groundDelayDefault;

        // If on ground or recently on ground
        if (groundDelay > 0)
        
            // If space bar recently pressed and still pressed, jump
            if (spaceDelay > 0 && Input.GetKey("space")) {
                spaceDelay = 0;
                rb.AddForce(new Vector2(0, jumpForce));
            }

            // If space bar pressed, jump
            else if (Input.GetKeyDown("space")) {
                rb.AddForce(new Vector2(0, jumpForce));
            }

        // If space key released while jumping up, kill upward velocity
        if (!grounded() && Input.GetKeyUp("space") && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpKill);

        // Decrement the space and ground delays
        if (spaceDelay > 0) spaceDelay--;
        if (groundDelay > 0) groundDelay--;
    }

    // Whether the player is currently grounded
    public bool grounded() {
        return (Mathf.Abs(rb.velocity.y) <= minVelocity);
    }
}

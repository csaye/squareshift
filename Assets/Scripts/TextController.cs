using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{

    public GameObject text;

    private Collider2D textTrigger;

    private Animator animator;

    void Start()
    {
        if (textTrigger == null) textTrigger = GetComponent<Collider2D>();

        if (animator == null) animator = text.GetComponent<Animator>();
    }

    void Update()
    {
        CheckIntersect();

        // Update text position based on player position
        text.transform.position = new Vector2(PlayerMovement.playerCenter.x, PlayerMovement.playerCenter.y + 2);
    }

    // Whether the center of the player position intersects with the collider
    void CheckIntersect() {

        // If the text collider contains the player center, activate the text
        if (textTrigger.bounds.Contains(PlayerMovement.playerCenter)) ActivateText();
    }

    // Animate the desired text object
    void ActivateText() {
        
        animator.SetBool("TextExpand", true);
    }

}

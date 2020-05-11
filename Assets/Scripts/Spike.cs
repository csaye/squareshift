using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    private GameObject player;

    private Collider2D killTrigger;

    void Start()
    {
        if (killTrigger == null) killTrigger = GetComponent<Collider2D>();

        if (player == null) player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        CheckIntersect();
    }

    // Whether the center of the player position intersects with the collider
    void CheckIntersect() {

        // Check each of the colliders of the player
        foreach (Transform child in player.transform) {

            // If the spike collider overlaps a player collider, kill the player
            if (killTrigger.bounds.Intersects(child.GetComponent<Collider2D>().bounds)) KillPlayer();
        }
    }

    // Kill the player
    void KillPlayer() {

        PlayerMovement.killed = true;
    }
}

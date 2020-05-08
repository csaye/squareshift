using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [Range(0.1f, 1)] public float shiftSlowness;

    private GameObject player;

    private Vector2 playerPos = Vector2.zero;

    // The x and y positions of the center of the current formation of the player
    private float xDifference, yDifference;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        // If the player is not currently shifting, move to player position
        if (!PlayerShifting.shifting) MoveToPlayer();
    }

    void MoveToPlayer() {

        // Update the player position
        UpdatePlayerPosition();

        // Move to player position
        transform.position = new Vector3(player.transform.position.x + xDifference, player.transform.position.y + yDifference, transform.position.z);
    }

    // Move to player position based on their current formation
    public void UpdatePosition() {

        // Currently shifting
        PlayerShifting.shifting = true;

        UpdatePlayerPosition();

        // Start moving to the player position
        StartCoroutine(MoveCamera(xDifference, yDifference));
    }

    private void UpdatePlayerPosition() {

         // Two by two shapes
        if (PlayerShifting.currentShift == 1) {
            xDifference = -1;
            yDifference = 0;

        // Two by three shapes
        } else if (PlayerShifting.currentShift == 2 || PlayerShifting.currentShift == 4 || PlayerShifting.currentShift == 5 || PlayerShifting.currentShift == 7) {
            xDifference = -1;
            yDifference = 0.5f;

        // Three by two shapes
        } else if (PlayerShifting.currentShift == 3 || PlayerShifting.currentShift == 6 || PlayerShifting.currentShift == 8) {
            xDifference = -0.5f;
            yDifference = 0;

        // One by four shapes
        } else if (PlayerShifting.currentShift == 9) {
            xDifference = -1.5f;
            yDifference = 1;

        // Four by one shapes
        } else if (PlayerShifting.currentShift == 0) {
            xDifference = 0;
            yDifference = -0.5f;
        }
    }

    IEnumerator MoveCamera(float x, float y) {

        Vector2 startingPos = transform.position;

        // Move to the new player position over the course of a half second
        for (int i = 0; i <= 25; i++) {

            // Set the local position to the desired position plus the distance moved since called
            float localX = x + player.transform.position.x;
            float localY = y + player.transform.position.y;

            Debug.Log("moving from position: " + startingPos.y + " to: " + localY);

            // Increment to new player position
            float incrementedX = (((Mathf.Abs(i - 100) / 100.0f) * transform.position.x) + ((i / 100.0f) * localX));
            float incrementedY = (((Mathf.Abs(i - 100) / 100.0f) * transform.position.y) + ((i / 100.0f) * localY));
            transform.position = new Vector3(incrementedX, incrementedY, transform.position.z);

            // Wait
            yield return new WaitForSeconds(shiftSlowness / 100.0f);
        }

        // Done shifting
        PlayerShifting.shifting = false;
    }
}

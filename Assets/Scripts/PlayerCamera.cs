using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [Range(0.1f, 1)] public float shiftSlowness;

    private GameObject player;

    private Vector2 playerPos = Vector2.zero;

    // The x and y positions of the center of the current formation of the player
    private float xOffset, yOffset;

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

        // Update the center position of the player
        UpdatePlayerCenter();

        // Move to player position
        // If camera position does not exceed bounds
        if (PlayerMovement.playerCenter.x < LevelEnd.screenXMax) {
            transform.position = new Vector3(PlayerMovement.playerCenter.x, PlayerMovement.playerCenter.y, transform.position.z);
        
        // If camera position exceeds bounds
        } else {
            transform.position = new Vector3(LevelEnd.screenXMax, PlayerMovement.playerCenter.y, transform.position.z);
        }
    }

    // Move to player position based on their current formation
    public void UpdatePosition() {

        // Currently shifting
        PlayerShifting.shifting = true;

        // Update the center position of the player
        UpdatePlayerCenter();

        // Start moving to the player position
        StartCoroutine(MoveCamera(PlayerMovement.playerCenter));
    }

    private void UpdatePlayerCenter() {

         // Two by two shapes
        if (PlayerShifting.currentShift == 1) {
            xOffset = -1;
            yOffset = 0;

        // Two by three shapes
        } else if (PlayerShifting.currentShift == 2 || PlayerShifting.currentShift == 4 || PlayerShifting.currentShift == 5 || PlayerShifting.currentShift == 7) {
            xOffset = -1;
            yOffset = 0.5f;

        // Three by two shapes
        } else if (PlayerShifting.currentShift == 3 || PlayerShifting.currentShift == 6 || PlayerShifting.currentShift == 8) {
            xOffset = -0.5f;
            yOffset = 0;

        // One by four shapes
        } else if (PlayerShifting.currentShift == 9) {
            xOffset = -1.5f;
            yOffset = 1;

        // Four by one shapes
        } else if (PlayerShifting.currentShift == 0) {
            xOffset = 0;
            yOffset = -0.5f;
        }

        // Set the player center to the current position plus the center offset
        PlayerMovement.playerCenter = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
    }

    IEnumerator MoveCamera(Vector2 center) {

        // Get the starting position of the player
        Vector2 startingPos = player.transform.position;

        // Move to the new player position over the course of a half second
        for (int i = 0; i <= 25; i++) {

            // Set the local position to the desired position plus the distance moved since called
            float localX = center.x - startingPos.x + player.transform.position.x;
            float localY = center.y - startingPos.y + player.transform.position.y;

            // Increment to new player position
            float incrementedX = (((Mathf.Abs(i - 100) / 100.0f) * transform.position.x) + ((i / 100.0f) * localX));
            float incrementedY = (((Mathf.Abs(i - 100) / 100.0f) * transform.position.y) + ((i / 100.0f) * localY));
            
            // If camera position does not exceed bounds
            if (incrementedX < LevelEnd.screenXMax) {
                transform.position = new Vector3(incrementedX, incrementedY, transform.position.z);
            
            // If camera position exceed bounds
            } else {
                transform.position = new Vector3(LevelEnd.screenXMax, incrementedY, transform.position.z);
            }

            // Delay camera transition
            yield return new WaitForSeconds(shiftSlowness / 100.0f);
        }

        // Done shifting
        PlayerShifting.shifting = false;
    }
}

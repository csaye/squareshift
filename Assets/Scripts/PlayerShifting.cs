using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShifting : MonoBehaviour
{

    // The current shift that the player is in
    public static float currentShift = 1;

    // Whether the player is currently shifting
    public static bool shifting = false;

    // The four boxes which make up the player
    public GameObject box1, box2, box3, box4;

    // The player camera
    public GameObject playerCamera;

    private GameObject player;

    // Vector pos positioning:
    // 0_3
    // 0_2 1_2
    // 0_1 1_1 2_1
    // 0_0 1_0 2_0 3_0
    private Vector2 pos0_3 = new Vector2(-1.5f, 2.5f);
    private Vector2 pos0_2 = new Vector2(-1.5f, 1.5f), pos1_2 = new Vector2(-0.5f, 1.5f);
    private Vector2 pos0_1 = new Vector2(-1.5f, 0.5f), pos1_1 = new Vector2(-0.5f, 0.5f), pos2_1 = new Vector2(0.5f, 0.5f);
    private Vector2 pos0_0 = new Vector2(-1.5f, -0.5f), pos1_0 = new Vector2(-0.5f, -0.5f), pos2_0 = new Vector2(0.5f, -0.5f), pos3_0 = new Vector2(1.5f, -0.5f);

    private PlayerCamera playerCameraScript;
    private PlayerMovement playerMovementScript;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");

        playerCameraScript = playerCamera.GetComponent<PlayerCamera>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {

        // If player is on the ground and not currently shifting, check for inputs
        if (playerMovementScript.grounded() && !shifting) UpdateBoxPosition();
    }

    // Checks for the key presses corresponding to the box formations
    void UpdateBoxPosition() {
        if (Input.GetKeyDown("1")) SetBoxPosition(1, pos0_0, pos1_0, pos0_1, pos1_1);
        if (Input.GetKeyDown("2")) SetBoxPosition(2, pos0_0, pos1_1, pos0_1, pos1_2);
        if (Input.GetKeyDown("3")) SetBoxPosition(3, pos0_0, pos1_0, pos1_1, pos2_1);
        if (Input.GetKeyDown("4")) SetBoxPosition(4, pos0_0, pos0_1, pos0_2, pos1_2);
        if (Input.GetKeyDown("5")) SetBoxPosition(5, pos1_0, pos0_1, pos1_1, pos0_2);
        if (Input.GetKeyDown("6")) SetBoxPosition(6, pos2_0, pos1_0, pos1_1, pos0_1);
        if (Input.GetKeyDown("7")) SetBoxPosition(7, pos1_0, pos1_1, pos1_2, pos0_2);
        if (Input.GetKeyDown("8")) SetBoxPosition(8, pos1_0, pos0_1, pos1_1, pos2_1);
        if (Input.GetKeyDown("9")) SetBoxPosition(9, pos0_0, pos0_1, pos0_2, pos0_3);
        if (Input.GetKeyDown("0")) SetBoxPosition(0, pos0_0, pos1_0, pos2_0, pos3_0);
    }

    // Sets the box positions to the desired formation
    void SetBoxPosition(float index, Vector2 box1pos, Vector2 box2pos, Vector2 box3pos, Vector2 box4pos) {

        // If the desired formation is not obstructed
        if (!obstructed(box1pos, box2pos, box3pos, box4pos)) {

            // Because success in shifting to formation, set current shift to current index
            currentShift = index;

            // Update camera based on new formation
            playerCameraScript.UpdatePosition();

            // Move the boxes to their formation position
            box1.transform.localPosition = box1pos;
            box2.transform.localPosition = box2pos;
            box3.transform.localPosition = box3pos;
            box4.transform.localPosition = box4pos;
        }
    }

    // Whether the desired box formation is obstructed or not
    bool obstructed(Vector2 box1pos, Vector2 box2pos, Vector2 box3pos, Vector2 box4pos) {
        
        Vector2[] positions = {box1pos, box2pos, box3pos, box4pos};

        // Check each desired box position
        foreach (Vector2 boxPos in positions) {

            // Convert box position to local box position by adding overall player position
            Vector2 localBoxPos = boxPos + (Vector2)(transform.position);

            // Set size check to just under full block to prevent interference
            Vector2 size = new Vector2(0.49f, 0.49f);

            // Check each collider within each desired box position
            foreach (Collider2D collider in (Physics2D.OverlapBoxAll(localBoxPos, size, 0))) {
                
                // If the position has a collider which is not a trigger and does not have tag player box, return obstructed
                if (!collider.isTrigger && !collider.gameObject.CompareTag("Player Box")) return true;
            }
        }

        // If no positions were found to have interference, return true;
        return false;
    }
}

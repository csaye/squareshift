using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{

    // Whether the level completion has been triggered
    public static bool levelComplete;

    // The maximum value of the screen the camera can move to
    public static float screenXMin;
    public static float screenXMax;

    public float cameraXMin;
    public float cameraXMax;

    // The collider in which the player can trigger the end level sequence
    private Collider2D endTrigger;

    void Start()
    {

        // Reset level complete
        levelComplete = false;

        // Set the screen bounds to the set camera bounds
        screenXMin = cameraXMin;
        screenXMax = cameraXMax;

        if (endTrigger == null) endTrigger = GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckIntersect();
    }

    // Whether the center of the player position intersects with the collider
    void CheckIntersect() {
        if (endTrigger.bounds.Contains(PlayerMovement.playerCenter)) EndLevel();
    }

    // Trigger end level sequence
    void EndLevel() {
        levelComplete = true;
    }
}

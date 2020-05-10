using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    // The maximum value of the screen the camera can move to
    public static float screenXMax;

    public float cameraXBound;

    private Collider2D endTrigger;

    void Start()
    {
        screenXMax = transform.position.x - cameraXBound;

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
        Debug.Log("ending level");
    }
}

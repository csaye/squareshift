using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShifting : MonoBehaviour
{

    // The four boxes which make up the player
    public GameObject box1, box2, box3, box4;

    // Vector pos positioning:
    // 5 6
    // 3 4
    // 1 2
    private Vector2 pos1 = new Vector2(-0.5f, -0.5f);
    private Vector2 pos2 = new Vector2(0.5f, -0.5f);
    private Vector2 pos3 = new Vector2(-0.5f, 0.5f);
    private Vector2 pos4 = new Vector2(0.5f, 0.5f);
    private Vector2 pos5 = new Vector2(-0.5f, 1.5f);
    private Vector2 pos6 = new Vector2(0.5f, 1.5f);

    void Start()
    {
        
    }

    void Update()
    {
        UpdateBoxPosition();
    }

    // Checks for the key presses corresponding to the box formations
    void UpdateBoxPosition() {
        if (Input.GetKeyDown("1")) SetBoxPosition(pos1, pos2, pos3, pos4);
        if (Input.GetKeyDown("2")) SetBoxPosition(pos1, pos4, pos3, pos6);
    }

    // Sets the box positions to the desired formation
    void SetBoxPosition(Vector2 box1pos, Vector2 box2pos, Vector2 box3pos, Vector2 box4pos) {

        // If the desired formation is not obstructed
        if (!obstructed(box1pos, box2pos, box3pos, box4pos)) {
            box1.transform.localPosition = box1pos;
            box2.transform.localPosition = box2pos;
            box3.transform.localPosition = box3pos;
            box4.transform.localPosition = box4pos;
        }
    }

    // Whether the desired box configuration is obstructed or not
    bool obstructed(Vector2 box1pos, Vector2 box2pos, Vector2 box3pos, Vector2 box4pos) {
        return false;
    }
}

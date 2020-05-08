using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private GameObject player;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        UpdatePosition();
    }

    // Move to player position based on their current formation
    public void UpdatePosition() {

        // For two wide shapes, move camera one unit left of player
        if (PlayerShifting.currentShift == 1 || PlayerShifting.currentShift == 2 || PlayerShifting.currentShift == 4 || PlayerShifting.currentShift == 5 || PlayerShifting.currentShift == 7) {
            transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y, transform.position.z);

        // For three wide shapes, move camera half a unit left of player
        } else if (PlayerShifting.currentShift == 3 || PlayerShifting.currentShift == 6 || PlayerShifting.currentShift == 8) {
            transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, transform.position.z);

        // For one wide shapes, move camera one and a half units left of player
        } else if (PlayerShifting.currentShift == 9) {
            transform.position = new Vector3(player.transform.position.x - 1.5f, player.transform.position.y, transform.position.z);

        // For four wide shapes, move camera to player
        } else if (PlayerShifting.currentShift == 0) {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}

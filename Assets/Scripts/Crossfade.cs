using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckFade();
    }

    void CheckFade() {

        // If the player has been killed, begin crossfade
        if (PlayerMovement.killed) animator.SetBool("Fade", true);

        // If the level completion has been triggered, begin crossfade
        if (LevelBounds.levelComplete) animator.SetBool("Fade", true);
    }

    void SwitchScene() {

        // If player killed
        if (PlayerMovement.killed) {

            PlayerMovement.killed = false;

            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // If level complete
        } else if (LevelBounds.levelComplete) {

            // Load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

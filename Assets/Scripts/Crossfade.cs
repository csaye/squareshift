using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{

    // The scene to be loaded after the animation sequence
    public string nextScene;

    private Animator animator;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckLevelComplete();
    }

    void CheckLevelComplete() {

        // If the level completion has been triggered, begin crossfade
        if (LevelBounds.levelComplete) animator.SetBool("LevelComplete", true);
    }

    void SwitchScene() {
        SceneManager.LoadScene(nextScene);
    }
}

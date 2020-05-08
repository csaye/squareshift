using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public Animator textAnimator;

    private float frames = 60;

    void Start()
    {

    }

    void Update()
    {
        if (frames == -1) textAnimator.SetBool("TextExpand", false);
        if (frames == 0) {

            frames = -1;

            // Trigger start text
            textAnimator.SetBool("TextExpand", true);
        }
        if (frames > 0) frames--;
    }
}

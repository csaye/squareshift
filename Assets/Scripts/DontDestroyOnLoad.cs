using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    static GameObject instance;

    void Awake()
    {

        // If not already created, set to instance
        if (instance == null) {

            instance = gameObject;
            DontDestroyOnLoad(gameObject);

        // If already created, delete instance
        } else {

            Destroy(gameObject);
        }
    }

}

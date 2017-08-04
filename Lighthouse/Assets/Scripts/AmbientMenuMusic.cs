using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientMenuMusic : MonoBehaviour {
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
  
    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "Main") {
            DestroyImmediate(gameObject);
        }
    }

	}


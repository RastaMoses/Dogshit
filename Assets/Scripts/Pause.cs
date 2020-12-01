using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
    [SerializeField] PauseAnimation pauseBackgroundAnimation;
    [SerializeField] TextMeshProUGUI pauseText;
    bool paused;

    private void Start()
    {
        pauseText.enabled = false;
        pauseBackgroundAnimation = FindObjectOfType<PauseAnimation>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                SetPause();
            }
            
        }
    }

    private void SetPause()
    {

        pauseBackgroundAnimation.StartAnimation();
        pauseText.enabled = true;
        paused = true;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        pauseText.enabled = false;
        Time.timeScale = 1;
        pauseBackgroundAnimation.StopAnimation();
        paused = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public string MenuScene;
    public bool isPaused = false;
    public GameObject PauseMenuCanvas;
    public Button FirstButton;

    public void GoBackToSplashScreen()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        PauseMenuCanvas.SetActive(isPaused);
        if (isPaused)
        {
            FirstButton.Select();
            FirstButton.OnSelect(null);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

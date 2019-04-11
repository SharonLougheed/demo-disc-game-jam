﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempWin : MonoBehaviour
{
    //Temp win overlay
    public GameObject GameOverCanvas;
    public Text GameOverText;
    public Camera cam;
    private LevelManager lm;

    // Start is called before the first frame update
    void Awake()
    {
        //Temp win overlay
        GameOverCanvas.SetActive(false);
        lm = FindObjectOfType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lm.isGameOver)
        {
            GameOver(lm.WinningPlayer);
        }

    }

    private void GameOver(Player lastPlayer)
    {
        GameOverCanvas.SetActive(true);
        cam.gameObject.SetActive(true);
        GameOverText.text = "Player " + lastPlayer.PlayerNumber + " Wins!";

        StartCoroutine(RestartGame(6));
    }

    private IEnumerator RestartGame(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Application.LoadLevel("Scene-Ben");
    }
}
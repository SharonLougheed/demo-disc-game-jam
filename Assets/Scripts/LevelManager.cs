using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool isGameOver;
    public Player WinningPlayer;


    private void Awake()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        CheckForWinner();
    }

    private void CheckForWinner()
    {
        var players = FindObjectsOfType<Player>();
        var livingPlayers = new List<Player>();
        foreach (var player in players)
        {
            if (player.IsAlive)
            {
                livingPlayers.Add(player);
            }
        }

        if (livingPlayers.Count <= 1)
        {
            GameOver(livingPlayers[0]);
        }
    }

    private void GameOver(Player lastPlayer)
    {
        WinningPlayer = lastPlayer;
        isGameOver = true;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool isGameOver;
    public Player WinningPlayer;
    public GameObject PlayerPrefab;
    public int NumberOfPlayers;
    private RectCollection rectCollection;
    public RectCollection FourWaySplit;
    public RectCollection TwoWaySplit;
    public RectCollection NoSplit;


    private void Awake()
    {
        isGameOver = false;
        switch (NumberOfPlayers)
        {
            case 1:
                rectCollection = NoSplit;
                break;
            case 2:
                rectCollection = TwoWaySplit;
                break;
            default:
                rectCollection = FourWaySplit;
                break;
        }
        LoadPlayers(NumberOfPlayers);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        CheckForWinner();
    }

    private void LoadPlayers(int numberOfPlayers)
    {
        GameObject[] players = new GameObject[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = Instantiate(PlayerPrefab, new Vector3(i * 2, 1, i), Quaternion.identity);

            var player = players[i].GetComponent<Player>();
            player.PlayerNumber = i + 1;

            var controller = players[i].GetComponent<PlayerController>();
            controller.ControllerNumber = i + 1;

            var cam = players[i].GetComponentInChildren<Camera>();
            //cam.rect = new Rect(GetCordFromPlayerNumber(player.PlayerNumber), GetSizeFromNumberOfPlayers(numberOfPlayers));
            cam.rect = rectCollection.Rects[i];
        }
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

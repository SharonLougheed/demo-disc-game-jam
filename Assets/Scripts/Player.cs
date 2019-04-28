﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerNumber;
    public int PlayerNumber
    {
        get { return playerNumber; }
        set
        {
            playerNumber = value;
            SetHealth();
            SetLives();
        }
    }

    public PlayerDefaults defaults;
    public IntHealthDict HealthData;
    public Health health;
    public bool IsAlive;
    public int Lives;

    public Puncher leftHand;
    public Puncher rightHand;
    public PlayerRenderer playerRenderer;
    public UserInterface userInterface;
    public GameObject dropOnDeathPrefab;

    private Color originalColor;

    public void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;

        //Not set yet, but they're all referencing the same thing
        leftHand.players = playerRenderer.allPlayers;
        rightHand.players = playerRenderer.allPlayers;
    }

    private void Update()
    {
        if (playerNumber == 2)
        {
            Debug.Log("Player Pos: " + transform.position + " | Frame: " + Time.frameCount);
        }
    }
    public void SetLives()
    {
        Lives = defaults.StartLives;
    }

    public void SetHealth()
    {
        HealthData.AddEntry(PlayerNumber, health);

        health.MaxValue = defaults.MaxHealth;
        health.MinValue = defaults.MinHealth;
        health.Value = defaults.StartHealth;
	}

    public void GiveHeath(int amount)
    {
        health.Increase(amount);
        userInterface.healthText.text = "Health: " + health.Value;
    }

    public void TakeDamage(int amount)
    {
        health.Decrease(amount);
        StartCoroutine(FlashPlayer());

        // Rock camera back

        Debug.Log("Player " + PlayerNumber + " has health: " + HealthData.Entry[PlayerNumber].Value);

        if (health.Value <= defaults.MinHealth)
        {
            Lives--;
            if (Lives > 0)
            {
                Respawn();
            }
            else
            {
                Kill();
            }
        }
        else
        {
            userInterface.healthText.text = "Health: " + health.Value;
        }
    }

    public void SetUserInterface()
    {
        userInterface.healthText.text = "Health: " + health.Value;
        leftHand.userInterface = userInterface;
        rightHand.userInterface = userInterface;
    }

    public IEnumerator FlashPlayer()
    {
        if (playerRenderer == null)
        {
            GetComponent<Renderer>().material.color = Color.magenta;

            yield return new WaitForSecondsRealtime(0.1f);

            GetComponent<Renderer>().material.color = originalColor;
        }
        else
        {
            playerRenderer.FlashPlayer();
        }
    }

    public void Kill()
    {
        // Take away all health here in case of insta-death
        health.Value = health.MinValue;
        IsAlive = false;
        userInterface.healthText.text = "x_x";

        //if (dropOnDeathPrefab != null)
        //{
        //    GameObject drop = GameObject.Instantiate(dropOnDeathPrefab);
        //    drop.transform.position = transform.position;
        //    drop.GetComponentInChildren<SpriteRotator>().allPlayers = playerRenderer.allPlayers;
        //}

        // Destroy for now : We will want to leave the corpses
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    private void Respawn()
    {

        if (dropOnDeathPrefab != null)
        {
            GameObject drop = GameObject.Instantiate(dropOnDeathPrefab);
            drop.transform.position = transform.position;
            drop.GetComponentInChildren<SpriteRotator>().allPlayers = playerRenderer.allPlayers;
        }

        var levelManager = FindObjectOfType<LevelManager>();
        var spawnPoint = levelManager.NextPlayerSpawnPoint();
        var playerController = GetComponent<PlayerController>();
        playerController.SetNewPositionRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

        SetHealth();
		userInterface.healthText.text = "Health: " + health.Value;
		Debug.Log("Player " + playerNumber + " new pos is:" + transform.position);
    }
}

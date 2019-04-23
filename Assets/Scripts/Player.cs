using System;
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
        }
    }

    public PlayerDefaults defaults;
    public IntHealthDict HealthData;
    public Health health;
    public bool IsAlive;

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

			userInterface.healthText.text = "x_x";
			Kill();
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

		if(dropOnDeathPrefab != null)
		{
			GameObject drop = GameObject.Instantiate(dropOnDeathPrefab);
			drop.transform.position = transform.position;
			drop.GetComponentInChildren<SpriteRotator>().allPlayers = playerRenderer.allPlayers;
		}

		// Destroy for now : We will want to leave the corpses
		Destroy(gameObject);
        gameObject.SetActive(false);
    }
}

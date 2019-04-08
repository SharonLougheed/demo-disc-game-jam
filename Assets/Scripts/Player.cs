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

    private Color originalColor;


    public void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    public void SetHealth()
    {
        HealthData.AddEntry(PlayerNumber, health);

        health.MaxValue = defaults.MaxHealth;
        health.MinValue = defaults.MinHealth;
        health.Value = defaults.StartHealth;
    }

    public void GiveHeath(int amount) => health.Increase(amount);

    public void TakeDamage(int amount)
    {
        health.Decrease(amount);
        StartCoroutine(FlashPlayer());

        Debug.Log("Player " + PlayerNumber + " has health: " + HealthData.Entry[PlayerNumber].Value);

        if (health.Value <= defaults.MinHealth)
        {
            Kill();
        }
    }

    public IEnumerator FlashPlayer()
    {
        GetComponent<Renderer>().material.color = Color.magenta;

        yield return new WaitForSecondsRealtime(0.1f);

        GetComponent<Renderer>().material.color = originalColor;
    }

    public void Kill()
    {
        // Take away all health here in case of insta-death
        health.Value = health.MinValue;
        IsAlive = false;

        // Destroy for now : We will want to leave the corpses
        Destroy(gameObject);
        gameObject.SetActive(false);
    }
}

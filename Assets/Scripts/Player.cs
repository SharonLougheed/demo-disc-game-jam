using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber = 1;
    public int Health;
    public PlayerDefaults defaults;
    public bool IsAlive;
    public Puncher leftHand;
    public Puncher rightHand;

    private Color originalColor;

    public void Awake()
    {
        Health = defaults.StartHealth;
        originalColor = GetComponent<Renderer>().material.color;
    }

    public void GiveHeath(int giveAmount)
    {
        Health += giveAmount;
        if (Health > defaults.MaxHealth)
        {
            Health = defaults.MaxHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log(damageAmount + " Damage done to " + PlayerNumber);
        Health -= damageAmount;

        StartCoroutine(FlashPlayer());

        if (Health <= 0)
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
        // Zero health here in case of insta-death
        Health = 0;
        IsAlive = false;

        // Destroy for now : We will want to leave the corpses
        Destroy(gameObject);
        gameObject.SetActive(false);
    }
}

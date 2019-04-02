using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber = 1;
    public int Health;
    public PlayerDefaults defaults;
    public bool IsAlive;

    public void Awake()
    {
        Health = defaults.StartHealth;
    }

    private void Update()
    {
        // Debug damage, DELETE ME
        if (Input.GetKeyDown(KeyCode.Q) && PlayerNumber == 2)
        {
            TakeDamage(15);
        }
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
        Health -= damageAmount;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        // Zero health here in case of insta-death
        Health = 0;
        IsAlive = false;

        // Destroy for now : We will want to leave the corpses
        Destroy(gameObject);
    }
}

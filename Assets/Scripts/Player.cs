using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int Lives;

    public Puncher leftHand;
    public Puncher rightHand;
    public PlayerRenderer playerRenderer;
    public UserInterface userInterface;
    public GameObject dropOnDeathPrefab;
    public float degreeOfDeath = 60f;

    private Color originalColor;

    public void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;

        //Not set yet, but they're all referencing the same thing
        leftHand.players = playerRenderer.allPlayers;
        rightHand.players = playerRenderer.allPlayers;
    }

    public void SetLives(int count)
    {
        Lives = count;
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
        userInterface.StartFlashScreen(Color.green);
        userInterface.healthText.text = "Health: " + health.Value;
        userInterface.healthSlider.value = health.Value;
    }

    public void TakeDamage(int amount)
    {
        health.Decrease(amount);
        userInterface.StartFlashScreen(Color.red);
        StartCoroutine(FlashPlayer());

        // Rock camera back

        Debug.Log("Player " + PlayerNumber + " has health: " + HealthData.Entry[PlayerNumber].Value);

        if (health.Value <= defaults.MinHealth)
        {
            Lives--;
            userInterface.lifePaws[Lives].SetActive(false); //Not ideal, but works in a pinch
            if (Lives > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {
                Kill();
            }
        }
        else
        {
            userInterface.healthText.text = "Health: " + health.Value;
            userInterface.healthSlider.value = health.Value;
        }
    }

    public void SetUserInterface()
    {
        userInterface.healthText.text = "Health: " + health.Value;
        userInterface.healthSlider.minValue = defaults.MinHealth;
        userInterface.healthSlider.maxValue = defaults.MaxHealth;
        userInterface.healthSlider.value = health.Value;
        userInterface.healthSliderBackground.color = playerRenderer.colorToApplyToSprites;
        foreach (GameObject lifePaw in userInterface.lifePaws)
        {
            lifePaw.GetComponent<Image>().color = playerRenderer.colorToApplyToSprites;
            userInterface.healthSliderBackground.color = playerRenderer.colorToApplyToSprites;
        }

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
            playerRenderer.StartFlashPlayer();
        }
    }

    public void Kill()
    {
        // Take away all health here in case of insta-death
        health.Value = health.MinValue;
        IsAlive = false;
        userInterface.healthText.text = "x_x";

        DropASteamer();

        DisablePlayer();
        DisableHands();
        DimView();
    }

    private void DimView()
    {
    }

    private void DisableHands()
    {
        /* var hands = gameObject.GetComponentsInChildren<Puncher>();
         foreach (var hand in hands)
         {
             hand.enabled = false;
             hand.gameObject.SetActive(false);
         }*/

        leftHand.gameObject.SetActive(false);
        rightHand.gameObject.SetActive(false);
    }

    private void EnableHands()
    {
        /*var hands = gameObject.GetComponentsInChildren<Puncher>();
        foreach (var hand in hands)
        {
            hand.enabled = true;
            hand.gameObject.SetActive(true);
        }*/

        leftHand.gameObject.SetActive(true);
        rightHand.gameObject.SetActive(true);

        leftHand.GetComponent<BoxCollider>().enabled = false;
        rightHand.GetComponent<BoxCollider>().enabled = false;
    }

    /* Deprecated for DisablePlayer() and EnablePlayer()

    private void DisableControls()
    {
        // Lerp me, please.
        this.transform.Rotate(new Vector3(0, 0, 1), degreeOfDeath);
        PlayerController controller = gameObject.GetComponent<PlayerController>();
        controller.ControllerActive = false;
    }

    private void DisableSprite()
    {
        // Need to disable the player sprite
        SpriteRenderer spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRender.enabled = false;
    }

    */

    IEnumerator Respawn()
    {
        health.Value = health.MinValue;

        DropASteamer();

        DisablePlayer();
        DisableHands();

        userInterface.healthText.text = "x_x  Respawn in 3";

        yield return new WaitForSeconds(1f);

        userInterface.healthText.text = "x_x  Respawn in 2";

        yield return new WaitForSeconds(1f);

        userInterface.healthText.text = "x_x  Respawn in 1";

        yield return new WaitForSeconds(1.2f);

        var levelManager = FindObjectOfType<LevelManager>();
        var spawnPoint = levelManager.NextPlayerSpawnPoint();
        var playerController = GetComponent<PlayerController>();
        playerController.SetNewPositionRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

        SetHealth();

        userInterface.healthText.text = "Health: " + health.Value;
        userInterface.healthSlider.value = health.Value;
        Debug.Log("Player " + playerNumber + " new pos is:" + transform.position);

        EnablePlayer();
        EnableHands();

        yield return null;
    }

    private void DropASteamer()
    {
        if (dropOnDeathPrefab != null)
        {
            GameObject drop = GameObject.Instantiate(dropOnDeathPrefab);
            drop.transform.position = transform.position;
            drop.GetComponentInChildren<SpriteRotator>().allPlayers = playerRenderer.allPlayers;
        }
    }

    private void DisablePlayer()
    {
        transform.Rotate(new Vector3(0, 0, 1), degreeOfDeath);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        userInterface.darknessPanel.SetActive(true);
        playerRenderer.DisableViewObjects();
    }

    private void EnablePlayer()
    {
        //GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<CharacterController>().enabled = true;
        userInterface.darknessPanel.SetActive(false);
        playerRenderer.EnableViewObjects();
    }
}

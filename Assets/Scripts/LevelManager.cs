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
    public GameObject HealthPickupPrefab;
    public GameObject WeaponPickupPrefab;

    public int NumberOfPlayers;
    public int NumberOfHealths;
    public int NumberOfWeapons;

    private RectCollection rectCollection;
    public RectCollection FourWaySplit;
    public RectCollection TwoWaySplit;
    public RectCollection NoSplit;

    public PlayerObjectFactory PlayerSpawnPoints;
    public HealthObjectFactory HealthSpawnPoints;
    public WeaponObjectFactory WeaponSpawnPoints;

    public GameObject[] players;
    public GameObject[] healthPickups;
    public GameObject[] weaponPickups;

    public float timerNextWeapon;
    public float timerNextHealth;
    public float timeBetweenWeapons;
    public float timeBetweenHealths;

    public Sprite boneSprite, cigarSprite, bottleSprite;

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

        PlayerSpawnPoints.LoadGameObjects();
        HealthSpawnPoints.LoadGameObjects();
        WeaponSpawnPoints.LoadGameObjects();

        LoadPlayers(NumberOfPlayers);
        LoadHealth(NumberOfHealths);
        LoadWeapon(NumberOfWeapons);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        CheckForWinner();
        CheckForHealthRespawn();
        CheckForWeaponRespawn();
    }

    private void CheckForHealthRespawn()
    {
        if (Time.time >= timerNextHealth)
        {
            LoadHealth(NumberOfHealths);
            timerNextHealth = Time.time + timeBetweenHealths;
        }
    }

    private void CheckForWeaponRespawn()
    {
        if (Time.time >= timerNextWeapon)
        {
            foreach (GameObject weapon in weaponPickups)
            {
                Destroy(weapon);
            }
            LoadWeapon(NumberOfWeapons);
            timerNextWeapon = Time.time + timeBetweenWeapons;
        }
    }

    private void LoadPlayers(int numberOfPlayers)
    {
        players = new GameObject[numberOfPlayers];
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

            var spawnPoint = PlayerSpawnPoints.GetNextObject();

            player.gameObject.transform.position = spawnPoint.transform.position;
            player.gameObject.transform.rotation = spawnPoint.transform.rotation;
        }

        SetupPlayerRenderers();
    }

    private void SetupPlayerRenderers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //Tell each player renderer who they are and who everyone else is
            var player = players[i].GetComponent<Player>();
            var pRenderer = player.playerRenderer;
            if (pRenderer == null) //old prefab
            {
                break;
            }
            pRenderer.playerNumber = player.PlayerNumber;
            pRenderer.allPlayers = players; //This is why players is public, faster than passing in copies

            //Disable mesh renderer if present
            var meshRenderer = players[i].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            //https://answers.unity.com/questions/348974/edit-camera-culling-mask.html
            //Turn on the view layer for this player
            var camera = players[i].GetComponentInChildren<Camera>();
            camera.cullingMask = -1; //Show Everything
                                     //Hide other player views
            for (int j = 0; j < players.Length; j++)
            {
                //If not this player
                if (j != i)
                {
                    //Hide that view layer
                    camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Player" + (j + 1) + "View"));
                }
            }

            pRenderer.Setup(); //Stuff it woulda called in Start
        }
    }

    private void LoadHealth(int numberOfHealths)
    {
        healthPickups = new GameObject[numberOfHealths];
        for (int i = 0; i < numberOfHealths; i++)
        {
            healthPickups[i] = Instantiate(HealthPickupPrefab, new Vector3(i * 2, 1, i), Quaternion.identity);

            var healthPickup = healthPickups[i].GetComponent<HealthPickup>();

            var spawnPoint = HealthSpawnPoints.GetNextObject();

            healthPickup.gameObject.transform.position = spawnPoint.transform.position;
            healthPickup.gameObject.transform.position += Vector3.up * 0.3f;
            //healthPickup.gameObject.transform.rotation = spawnPoint.transform.rotation;

            SpriteRotator spriteRotator = healthPickups[i].GetComponentInChildren<SpriteRotator>();
            if (spriteRotator != null)
            {
                if (players != null && players.Length != 0)
                {
                    spriteRotator.allPlayers = players;
                }
                else
                {
                    Debug.Log("Health pickup missing players.");
                }
            }
            else
            {
                Debug.Log("Health pickup missing a sprite rotator.");
            }
        }
    }

    private void LoadWeapon(int numberOfWeapons)
    {
        weaponPickups = new GameObject[numberOfWeapons];
        for (int i = 0; i < numberOfWeapons; i++)
        {
            weaponPickups[i] = Instantiate(WeaponPickupPrefab, new Vector3(i * 2, 1, i), Quaternion.identity);

            var weaponPickup = weaponPickups[i].GetComponent<WeaponPickup>();

            var spawnPoint = WeaponSpawnPoints.GetNextObject();

            weaponPickup.weaponType = GetRandomEnum<WeaponType>();
            weaponPickup.gameObject.transform.position = spawnPoint.transform.position;
            weaponPickup.gameObject.transform.position += Vector3.up * 0.3f;

            SpriteRotator spriteRotator = weaponPickups[i].GetComponentInChildren<SpriteRotator>();
            if (spriteRotator != null)
            {
                if (players != null && players.Length != 0)
                {
                    spriteRotator.allPlayers = players;
                    if (weaponPickup.weaponType == WeaponType.Cigar)
                    {
                        spriteRotator.spriteToChangeTo = cigarSprite;
                    }
                    else if (weaponPickup.weaponType == WeaponType.Bottle)
                    {
                        spriteRotator.spriteToChangeTo = bottleSprite; //A bottle of Sprite TM
                    }
                    else //Bone
                    {
                        spriteRotator.spriteToChangeTo = boneSprite;
                    }
                }
                else
                {
                    Debug.Log("Weapon pickup missing players.");
                }
            }
            else
            {
                Debug.Log("Weapon pickup missing a sprite rotator.");
            }
        }
    }

    //https://forum.unity.com/threads/random-range-from-enum.121933/
    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
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
        lastPlayer.transform.position = Vector3.zero;
        isGameOver = true;
    }
}

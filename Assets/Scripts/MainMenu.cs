using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelSettings settings;
    [SerializeField] string ToGame;
    private bool isStartingGame = false;

    private void Start()
    {
        // Set defaults in case nothing is changed
        settings.PlayerCount = 4;
        settings.LevelWeaponMode = WeaponMode.All;
        settings.Lives = 3;
    }

    public void LowResToggle()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayOptionSound();
        GameObject.Find("GameResolution").GetComponent<GameResolution>().isLowRes = GameObject.Find("Low Res Toggle").GetComponent<Toggle>().isOn;
    }

    public void PlayOptionSound()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayOptionSound();
    }

    public void PlayGame()
    {
        Debug.Log("New Play Game fired");
        if (!isStartingGame)
        {
            isStartingGame = true;

            StartCoroutine(BeginGame());
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetWeaponMode(int modeIndex) => settings.LevelWeaponMode = (WeaponMode)modeIndex;

    public void SetLives(int livesIndex) => settings.Lives = 5 - livesIndex;

    public void SetPlayerCount(int playerCountIndex) => settings.PlayerCount = 4 - playerCountIndex;

    IEnumerator BeginGame()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayStartSound();
        GameObject.Find("Loading Graphic").GetComponent<Image>().enabled = true;
        GameObject.Find("Loading Graphic").GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(2.1f);

        GameObject.Find("BlackOut").GetComponent<Image>().enabled = true;

        SceneManager.LoadScene(ToGame);
        yield return null;
    }
}

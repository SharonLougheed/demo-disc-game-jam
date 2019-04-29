using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  This script is for the Menu functions.

public class uiMainMenu : MonoBehaviour
{

	[SerializeField] GameObject Menu;
	//[SerializeField] GameObject firstbutton;
	[SerializeField] string ToGame;
	[SerializeField] string ToMainMenu;
	[SerializeField] string ToTutorial;
	[SerializeField] string ToCredits;

    private bool isStartingGame = false;

	public void MenuActivated ()
	{
		//MainMenu.SetActive (false);
	}
	
	public void MainMenu ()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayOptionSound();
        SceneManager.LoadScene(ToMainMenu);
	}

	public void PlayGame ()
    {
        if (!isStartingGame)
        {
            isStartingGame = true;

            StartCoroutine(BeginGame());
        }
	}

	public void Tutorial ()
	{
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayOptionSound();
		SceneManager.LoadScene(ToTutorial);
	}
	
	public void Credits ()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayOptionSound();
        SceneManager.LoadScene(ToCredits);
    }

    public void LowResToggle()
    {
        GameObject.Find("GameResolution").GetComponent<GameResolution>().isLowRes = GameObject.Find("Low Res Toggle").GetComponent<Toggle>().isOn;
    }

    public void QuitGame ()
	{
		Application.Quit();
	}

    IEnumerator BeginGame()
    {
        GameObject.Find("SplashTheme").GetComponent<SplashTheme>().PlayStartSound();
        GameObject.Find("Loading Graphic").GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(2.1f);

        SceneManager.LoadScene(ToGame);
        yield return null;
    }
}
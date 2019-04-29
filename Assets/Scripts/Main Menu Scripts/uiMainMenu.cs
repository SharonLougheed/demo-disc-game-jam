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
	
	public Button[] Buttons;

	public void MenuActivated ()
	{
		MainMenu.SetActive (false);
	}
	
	public void Credits ()
	{
		ToCredits.SetActive (true);
		
	}
	
	public void Destination (string scenename)
	{
		
	}
	
	public void MainMenu ()
	{
		SceneManager.LoadScene(ToMainMenu);
	}

	public void PlayGame ()
	{
		SceneManager.LoadScene(ToGame);
	}

	public void Tutorial ()
	{
		SceneManager.LoadScene(ToTutorial);
	}
	
	public void Credits ()
	{
		SceneManager.LoadScene(ToCredits);
	}

	public void QuitGame ()
	{
		Application.Quit();
	}
}
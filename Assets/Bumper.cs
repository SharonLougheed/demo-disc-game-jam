using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Animator animator;
	[SerializeField] string ToMainMenu;
	
	// Update is called once per frame
    void Update()
    {
        
    }
	
	public void OnFadeComplete ()
	{
		SceneManager.LoadScene(ToMainMenu);
	}
}

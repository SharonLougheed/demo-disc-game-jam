using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    public GameObject boomBox;

	private MeshRenderer[] renderers;

	private void Start()
	{
		renderers = GetComponentsInChildren<MeshRenderer>();
	}

	public void SmashJuke()
	{
		StartFlash();
		boomBox.GetComponent<MusicOffset>().ChangeTracks();
	}


	public void StartFlash()
	{
		StartCoroutine(Flash(Color.cyan));
	}

	//Shamelessly copied code from PlayerRenderer for time
	IEnumerator Flash(Color colorToChange)
	{
		float deltaT = 0.1f;
		float t = 0f;
		while (t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(Color.white, colorToChange, t);
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material.color = lerpedColor;
			}
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}

		//yield return new WaitForSecondsRealtime(0.1f); //Wait between colors
		t = 0f;
		while (t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(colorToChange, Color.white, t);
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material.color = lerpedColor;
			}
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}
	}
}

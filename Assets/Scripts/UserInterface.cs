using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
	public Text healthText;
	public Slider healthSlider;
	public Image healthSliderBackground;
	public GameObject[] lifePaws; //Not ideal way to handle this but I'm in a rush,
								  //would rather spawn images
	public Image weaponImage;
	public Sprite bottle, bone, cigar;
	public GameObject leftIdleFist, leftPunchingFist;
	public GameObject rightIdleFist, rightPunchingFist, rightBottle, rightBone, rightCigar;
	public GameObject darknessPanel;
	public GameObject flashPanel;

	private GameObject currentLeftFist;
	private GameObject currentRightFist;

	// Start is called before the first frame update
	void Start()
	{
		currentLeftFist = leftIdleFist;
		currentRightFist = rightIdleFist;
		weaponImage.gameObject.SetActive(false);
	}

	public void ChangeWeaponImage(WeaponType weaponType)
	{

		weaponImage.gameObject.SetActive(true);
		//ChangeSprite Here
		switch (weaponType)
		{
			case WeaponType.Bottle:
				weaponImage.sprite = bottle;
				break;
			case WeaponType.Bone:
				weaponImage.sprite = bone;
				break;
			case WeaponType.Cigar:
				weaponImage.sprite = cigar;
				break;
			default:
				weaponImage.gameObject.SetActive(false);
				break;
		}
	}

	public void ChangePaws(Side side, bool isPunching, WeaponType weaponType)
	{
		if(side == Side.Left)
		{
			currentLeftFist.SetActive(false);
			if (isPunching)
			{
				currentLeftFist = leftPunchingFist;
			}
			else
			{
				currentLeftFist = leftIdleFist;
			}
			currentLeftFist.SetActive(true);
		}
		else
		{
			currentRightFist.SetActive(false);
			if (isPunching)
			{
				switch (weaponType)
				{
					case WeaponType.BareFisted:
						currentRightFist = rightPunchingFist;
						break;
					case WeaponType.Bottle:
						currentRightFist = rightBottle;
						break;
					case WeaponType.Bone:
						currentRightFist = rightBone;
						break;
					case WeaponType.Cigar:
						currentRightFist = rightCigar;
						break;
					default:
						break;
				}
			}
			else
			{
				currentRightFist = rightIdleFist;
			}
			currentRightFist.SetActive(true);
		}
	}

	public void StartFlashScreen(Color colorToChange)
	{
		StartCoroutine(FlashScreen(colorToChange));
	}

	IEnumerator FlashScreen(Color colorToChange)
	{
		float deltaT = 0.1f;
		float t = 0f;
		while (t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(Color.clear, colorToChange, t);
			flashPanel.GetComponent<Image>().color = lerpedColor;
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}
		flashPanel.GetComponent<Image>().color = colorToChange;
		//yield return new WaitForSecondsRealtime(0.1f); //Wait between colors
		t = 0f;
		while (t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(colorToChange, Color.clear, t);
			flashPanel.GetComponent<Image>().color = lerpedColor;
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}
		flashPanel.GetComponent<Image>().color = Color.clear;
	}
}

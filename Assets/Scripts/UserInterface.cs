using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
	public Text healthText;
	public Image weaponImage;
	public Sprite bottle, bone, cigar;

	// Start is called before the first frame update
	void Start()
    {
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
}

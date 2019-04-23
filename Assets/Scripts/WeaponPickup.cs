using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType weaponType;
    public PickupSoundPlayer pickupSoundPlayer;

    private void Awake()
    {
        pickupSoundPlayer = FindObjectOfType<PickupSoundPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !weaponType.Equals(WeaponType.BareFisted))
        {
            Puncher[] punchers = other.gameObject.GetComponentsInChildren<Puncher>();
            foreach (Puncher puncher in punchers)
            {
                if (puncher.side == Side.Right)
                {
                    puncher.PickupWeapon(weaponType);

                    if (pickupSoundPlayer != null)
                    {
                        if (weaponType.Equals(WeaponType.Bottle))
                        {
                            pickupSoundPlayer.PlaySound(SoundType.BottlePickup);
                        }
                        else
                        {
                            pickupSoundPlayer.PlaySound(SoundType.WeaponPickup);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoundPlayer : MonoBehaviour
{
    // Change to dictionary
    public AudioClip healthPickupClip;
    public AudioClip weaponPickupClip;

    public void PlaySound(SoundType soundType)
    {
        var sound = GetComponent<AudioSource>();
        switch (soundType)
        {
            case SoundType.HealthPickup:
                sound.clip = healthPickupClip;
                break;
            case SoundType.WeaponPickup:
                sound.clip = weaponPickupClip;
                break;
            default:
                //SILENCE!!
                return;
        }
        sound.Play();
    }
}

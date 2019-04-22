using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public IntValueCollection healthValues;
    public HealthSize healthSize = HealthSize.Small;

    public PickupSoundPlayer pickupSoundPlayer;

    private void Awake()
    {
        pickupSoundPlayer = FindObjectOfType<PickupSoundPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.GiveHeath(healthValues.GetValueFor((int)healthSize));

            if (pickupSoundPlayer != null)
            {
                pickupSoundPlayer.PlaySound(SoundType.HealthPickup);
            }

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public IntValueCollection healthValues;
    public HealthSize healthSize = HealthSize.Small;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.GiveHeath(healthValues.GetValueFor((int)healthSize));
            Destroy(gameObject);
        }
    }
}

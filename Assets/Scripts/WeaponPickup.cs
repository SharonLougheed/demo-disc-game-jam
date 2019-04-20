using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    //public IntValueCollection healthValues;
    //public HealthSize healthSize = HealthSize.Small;
    public WeaponType weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Puncher[] punchers = other.gameObject.GetComponents<Puncher>();
            foreach (Puncher puncher in punchers)
            {
                if (puncher.side == Side.Right)
                {
                    puncher.weaponType = weaponType;
                }
            }
            Destroy(gameObject);
        }
    }
}

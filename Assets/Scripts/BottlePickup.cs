using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
        }
    }
}

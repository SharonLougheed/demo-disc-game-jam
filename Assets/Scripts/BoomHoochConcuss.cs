using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHoochConcuss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(20);
        }

        if (other.gameObject.tag.Equals("BOOMHOOCH") && !gameObject.transform.parent.gameObject.Equals(other.gameObject))
        {
            Destroy(other.gameObject);
            BoomHoochActivate killSwitch = other.gameObject.GetComponent<BoomHoochActivate>();
            killSwitch.GoBoomBoom();
        }
    }
}
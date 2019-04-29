using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHoochConcuss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && transform.parent.GetComponent<BoomHoochActivate>().isBoom)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(20);
        }

        if (other.gameObject.tag.Equals("BOOMHOOCH"))
        {
            BoomHoochActivate killSwitch = other.gameObject.GetComponent<BoomHoochActivate>();
            killSwitch.GoBoomBoom();
        }
    }
}
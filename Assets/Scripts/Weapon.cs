using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int AddedReach;

    public void DoDamage(Player player, int amount)
    {
        player.TakeDamage(amount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float PunchSpeed;

    public int PunchDamage;
    public float PunchReach;

    public int BottleDamage;
    public float BottleReach;
    public int BottleStrikes;

    public int BoneDamage;
    public float BoneReach;
    public int BoneStrikes;

    public int CigarDamage;
    public float CigarReach;
    public int CigarStrikes;
}

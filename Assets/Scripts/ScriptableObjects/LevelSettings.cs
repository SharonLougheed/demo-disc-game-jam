﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    public int Lives;
    public WeaponMode LevelWeaponMode;
    public int PlayerCount;
}

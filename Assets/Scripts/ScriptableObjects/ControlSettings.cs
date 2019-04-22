using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ControlSettings : ScriptableObject
{
    public Controller ControllerGroup;
    public float RotationSpeed;
    public float MovementSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ControlSettings : ScriptableObject
{
    public ControllerType ControllerGroup;
    public float RotationSpeed;
    public float MovementSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FloatVariable Health;
    public bool IsAlive;

    public void Kill()
    {
        IsAlive = false;
    }
}

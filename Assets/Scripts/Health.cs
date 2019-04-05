using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private int value;
    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = VerifyValues(value);
        }
    }

    public int MaxValue = 150;
    public int MinValue = 0;

    private int VerifyValues(int value) => Mathf.Clamp(value, MinValue, MaxValue);

    public void Increase(int amount) => Value += amount;

    public void Decrease(int amount) => Value -= amount;
}

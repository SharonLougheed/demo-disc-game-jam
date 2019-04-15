using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntValueCollection : ScriptableObject
{
    [SerializeField] private int[] values = new int[1];

    public int GetValueFor(int i, int defaultValue = 0)
    {
        if (i >= values.Length)
        {
            return defaultValue;
        }
        return values[i];
    }
}

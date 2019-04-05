using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntHealthDict : ScriptableObject
{
    public Dictionary<int, Health> Entry = new Dictionary<int, Health>();

    public void AddEntry(int i, Health health)
    {
        if (!Entry.ContainsKey(i))
        {
            Entry.Add(i, health);
        }
        else
        {
            Entry[i] = health;
        }
    }
}

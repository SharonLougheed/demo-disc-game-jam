using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameObjectFactory : ScriptableObject
{
    public int CurrentUnit = 0;
    public GameObject[] objects = new GameObject[1];

    // Player just to have a default
    public string Tag = "Player";

    public int LoadGameObjects()
    {
        objects = GameObject.FindGameObjectsWithTag(Tag);
        ResetCurrentUnit();
        return objects.Length;
    }

    public void ResetCurrentUnit(int newCurrent = 0)
    {
        CurrentUnit = newCurrent;
    }

    public GameObject GetNextObject()
    {
        int returnUnit = CurrentUnit;
        CurrentUnit++;
        if (CurrentUnit >= objects.Length)
        {
            ResetCurrentUnit(0);
        }
        return objects[returnUnit];
    }
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResolution : MonoBehaviour
{
    public bool isLowRes = true;

    private static GameResolution instance = null;

    public static GameResolution Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}

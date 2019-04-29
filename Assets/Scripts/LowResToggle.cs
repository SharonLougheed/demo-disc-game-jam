using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowResToggle : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.Find("GameResolution"))
        {
            GetComponent<Toggle>().isOn = GameObject.Find("GameResolution").GetComponent<GameResolution>().isLowRes;
        }
    }
}

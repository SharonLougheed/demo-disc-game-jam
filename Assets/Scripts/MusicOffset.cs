using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOffset : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FunkyJamShuffle());
    }

    IEnumerator FunkyJamShuffle()
    {
        var levelTheme = GetComponent<AudioSource>();
        var volumeIncrement = levelTheme.volume / 20.0f;

        levelTheme.time = Random.Range(0.0f, 120.0f);

        for(int k = 0; k <= 20; k++)
        {
            levelTheme.volume = (float)k * volumeIncrement;

            yield return new WaitForSeconds(.06f);
        }

        yield return null;
    }
}
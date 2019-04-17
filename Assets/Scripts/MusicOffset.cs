using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOffset : MonoBehaviour
{
    [Range(.5f, 3.5f)]
    public float FadeTime = 1.6f;

    void Start()
    {
        StartCoroutine(FunkyJamShuffle());
    }

    IEnumerator FunkyJamShuffle()
    {
        var levelTheme = GetComponent<AudioSource>();
        var volumeIncrement = levelTheme.volume / 20.0f;
        var timeIncrement = FadeTime / 20.0f;

        levelTheme.time = Random.Range(0.0f, 120.0f);

        for(int k = 0; k <= 20; k++)
        {
            levelTheme.volume = (float)k * volumeIncrement;

            yield return new WaitForSeconds(timeIncrement);
        }

        yield return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOffset : MonoBehaviour
{
    [Range(.5f, 3.5f)]
    public float FadeTime = 1.6f;

    [Range(.5f, 3.5f)]
    public float TransTime = .7f;

    private bool isChanging = true;

    private AudioSource trackOne;
    private AudioSource trackTwo;

    private float trackOneVol;
    private float trackTwoVol;

    void Start()
    {
        var sources = GetComponents<AudioSource>();

        trackOne = sources[0];
        trackTwo = sources[1];

        trackOneVol = trackOne.volume;
        trackTwoVol = trackTwo.volume;

        StartCoroutine(FunkyJamShuffle());
    }

    public void ChangeTracks()
    {
        if (!isChanging)
        {
            StartCoroutine(CrossFade());
        }
    }

    IEnumerator FunkyJamShuffle()
    {
        var volumeIncrement = trackOneVol / 20.0f;
        var timeIncrement = FadeTime / 20.0f;

        trackOne.Play();
        trackOne.time = Random.Range(0.0f, 120.0f);

        for (int k = 0; k <= 20; k++)
        {
            trackOne.volume = (float)k * volumeIncrement;

            yield return new WaitForSeconds(timeIncrement);
        }

        isChanging = false;

        yield return null;
    }

    IEnumerator CrossFade()
    {
        isChanging = true;

        var oldTrack = trackOne.isPlaying ? trackOne : trackTwo;
        var newTrack = trackOne.isPlaying ? trackTwo : trackOne;
        var oldInc = trackOne.isPlaying ? trackOneVol / 20.0f : trackTwoVol / 20.0f;
        var newInc = trackOne.isPlaying ? trackTwoVol / 20.0f : trackOneVol / 20.0f;

        var timeInc = TransTime / 20.0f;

        newTrack.Play();
        newTrack.time = oldTrack.time;

        for (int k = 0; k <= 20; k++)
        {
            oldTrack.volume = (float)(20 - k) * oldInc;
            newTrack.volume = (float)k * newInc;

            yield return new WaitForSeconds(timeInc);
        }

        oldTrack.Stop();

        isChanging = false;

        yield return null;
    }
}
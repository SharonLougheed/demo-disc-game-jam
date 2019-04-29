using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTheme : MonoBehaviour
{
    public AudioClip sfxOption;
    public AudioClip sfxPlayStart;

    private AudioSource pOne;
    private AudioSource pTwo;

    private static SplashTheme instance = null;

    public static SplashTheme Instance
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

        var players = GetComponents<AudioSource>();

        pOne = players[0];
        pTwo = players[1];
    }

    public void PlayOptionSound()
    {
        pTwo.PlayOneShot(sfxOption);
    }

    public void PlayStartSound()
    {
        pTwo.PlayOneShot(sfxPlayStart);
        StartCoroutine(SelfDestructSequence());
    }

    IEnumerator SelfDestructSequence()
    {
        float volumeIncrement = pOne.volume / 15.0f;
        
        for(int k = 14; k >= 0; k--)
        {
            pOne.volume = volumeIncrement * k;

            yield return new WaitForSeconds(.1f);
        }

        Destroy(gameObject);
    }
}

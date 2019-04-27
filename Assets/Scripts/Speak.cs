using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speak : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(MakeSomeNoise());
    }

    IEnumerator MakeSomeNoise()
    {
        var barks = GetComponents<AudioSource>();

        var barkQuantity = barks.Length;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));

            if (Random.Range(0, 2) > 0 && barkQuantity > 0)
            {
                barks[Random.Range(0, barkQuantity)].Play();
            }
        }

        yield return null;
    }
}
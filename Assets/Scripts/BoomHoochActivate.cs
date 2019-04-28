using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHoochActivate : MonoBehaviour
{
    public GameObject boomer;
    public GameObject sploder;
    public GameObject concusser;

    public void GoBoomBoom()
    {
        StartCoroutine(Splode());
    }

    IEnumerator Splode()
    {
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(.4f);

        boomer.SetActive(true);
        sploder.SetActive(true);
        concusser.SetActive(true);
        
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        concusser.SetActive(false);

        yield return new WaitForSeconds(4.5f);

        Destroy(gameObject);
    }
}
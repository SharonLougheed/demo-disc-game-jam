using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHoochActivate : MonoBehaviour
{
    public bool isBoom;

    public GameObject boomer;
    public GameObject sploder;
    public GameObject concusser;
    public GameObject bubbler;

    private GameObject dupe;

    public void GoBoomBoom()
    {
        if (isBoom)
        {
            StartCoroutine(Splode());
            StartCoroutine(FlashBoomHooch());
        }

        else
        {
            StartCoroutine(FallApart());
        }
    }

    IEnumerator FallApart()
    {
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(.05f);

        boomer.SetActive(true);
        sploder.SetActive(true);
        concusser.SetActive(true);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        concusser.SetActive(false);

        yield return new WaitForSeconds(32f);

        bubbler.SetActive(true);

        dupe = Instantiate(Resources.Load<GameObject>("Prefabs/CRATEHOOCH"), transform.parent.transform);
        dupe.transform.SetPositionAndRotation(transform.position, transform.rotation);

        yield return new WaitForSeconds(4.5f);

        Destroy(gameObject);
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

        yield return new WaitForSeconds(32f);

        bubbler.SetActive(true);
        
        dupe = Instantiate(Resources.Load<GameObject>("Prefabs/BOOMHOOCH"), transform.parent.transform);
        dupe.transform.SetPositionAndRotation(transform.position, transform.rotation);

        yield return new WaitForSeconds(4.5f);

        Destroy(gameObject);
    }

    public IEnumerator FlashBoomHooch()
    {
        var myMaterial = GetComponent<Renderer>().material;

        while (true)
        {
            myMaterial.EnableKeyword("_EMISSION");

            yield return new WaitForSecondsRealtime(0.1f);
            
            myMaterial.DisableKeyword("_EMISSION");

            yield return new WaitForSeconds(.1f);
        }
    }
}
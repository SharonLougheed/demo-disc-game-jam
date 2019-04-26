using System.Collections;
using UnityEngine;

public class StaticGenerate : MonoBehaviour
{
    //-- Attach this script to the Point Of Origin (POO) for Space Static.
    //-- SPECIAL NOTE: Children (T_Static Sprites) must have the script "SpriteSnap".

    public float delay = 0.18f;                                 // Amount of time to wait between Static regeneration

    private void Start()
    {
        StartCoroutine(StormDatSkyYo());
    }

    private IEnumerator StormDatSkyYo()
    {
        while(true)
        {
            foreach(Transform child in transform)
            {
                if(!child.gameObject.activeSelf)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        GameObject clone = Instantiate(child.gameObject, transform);

                        clone.gameObject.SetActive(true);

                        float ranX = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
                        float ranY = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
                        float ranZ = Random.Range(0.001f, 50.0f) * (Random.Range(0, 2) == 1 ? -1.0f : 1.0f);
                        
                        clone.transform.localPosition = new Vector3(ranX, ranY, ranZ);

                        clone.GetComponent<SpriteSnap>().PsyBlowie();
                        clone.GetComponent<SpriteSnap>().Wiggle(Random.Range(1.0f, 2.5f));
                    }
                }
            }

            yield return new WaitForSeconds(delay);

            foreach(Transform child in transform)
            {
                if(child.gameObject.activeSelf)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
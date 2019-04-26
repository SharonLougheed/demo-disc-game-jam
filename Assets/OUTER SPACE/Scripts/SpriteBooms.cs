using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBooms : MonoBehaviour
{
    //-- Attach this script to a SPRITE you don't want crowded.

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
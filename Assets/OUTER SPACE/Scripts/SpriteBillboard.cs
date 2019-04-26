using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    //-- Attach this script to a SPRITE that you want toward billboard to a particular Camera.

    public GameObject myCam;                                    // Camera toward which SPRITE will be billboarded

    private void LateUpdate()
    {
        transform.forward = myCam.transform.forward;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    public GameObject boomBox;

    public void SmashJuke()
    {
        boomBox.GetComponent<MusicOffset>().ChangeTracks();
    }
}

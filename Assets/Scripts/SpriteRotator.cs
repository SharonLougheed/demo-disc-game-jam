using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For unmoving objects only. PlayerRenderer already handles sprite rotation.
public class SpriteRotator : MonoBehaviour
{
	public GameObject[] allPlayers; //Must be in order! May leave this player object blank.
	public GameObject[] respectiveViewObjects; //Make sure the indices align with allPlayers. This is what they will see.

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < allPlayers.Length; i++)
		{
			//Rotate each sprite to face each player
			respectiveViewObjects[i].transform.LookAt(allPlayers[i].transform.position, Vector3.up);
		}
	}
}

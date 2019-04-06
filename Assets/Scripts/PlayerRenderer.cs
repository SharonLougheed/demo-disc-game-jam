using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains all images of this player
//and changes the images this player sees
public class PlayerRenderer : MonoBehaviour
{
	public Texture2D north; //0 degrees globally
	public Texture2D northeast;
	public Texture2D east;
	public Texture2D southeast;
	public Texture2D south;
	public Texture2D southwest;
	public Texture2D west;
	public Texture2D northwest;
	public GameObject[] players = new GameObject[4];
	public GameObject[] otherPlayers;
	public GameObject[] quads; //Make sure the indices align with otherPlayers. This is what they will see.
	public bool use4Directions = false; //If true, only uses N, S, E, W textures
	public bool applyMaterialToTextures = true;

	private PlayerRenderer[] otherPlayerRenderers;
	private Direction[] directionsOtherPlayersSee;
	private enum Direction { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST }

    //Start is called before the first frame update 
    void Start()
	{
		//Find all other player renderers now to save time later
		otherPlayerRenderers = new PlayerRenderer[otherPlayers.Length];
		for (int i=0; i<otherPlayers.Length; i++)
		{
			otherPlayerRenderers[i] = otherPlayers[i].GetComponent<PlayerRenderer>();
		}
		directionsOtherPlayersSee = new Direction[otherPlayers.Length];
	}

    //Update is called once per frame
	//Change the textures of other players in this player's view layer
    void Update()
    {
		//Use the direction this player is facing as north in a lil coordinate system
		float thisYRotation = transform.rotation.eulerAngles.y;
		//Go through all other players
		for (int i = 0; i < otherPlayers.Length; i++)
		{
			//First, rotate the corresponding quad to face that player

			//Then take the direction the other player is facing
			float thatYRotation = otherPlayers[i].transform.rotation.eulerAngles.y;
			//And rotate it by this player's direction to convert to this lil coordinate system
			float convertedYRotation = (thatYRotation + thisYRotation) % 360;
			//This is the direction relative to this player
			Direction relativeDirection = DegreesToDirection(convertedYRotation);
			//Change the image the 
			otherPlayerRenderers[i].RenderDirection(i, relativeDirection);
		}
	}

	//Called by other PlayerRenderers
	//Changes the texture of the quad corresponding to that player
	void RenderDirection(int playerNumber, Direction relativeDirection)
	{
		if (directionsOtherPlayersSee[playerNumber] == relativeDirection)
			return; //an attempt to be slightly more efficient

		switch (relativeDirection)
		{
			case Direction.NORTH:

				break;
			case Direction.NORTHEAST:

				break;
			case Direction.EAST:

				break;
			case Direction.SOUTHEAST:

				break;
			case Direction.SOUTH:

				break;
			case Direction.SOUTHWEST:

				break;
			case Direction.WEST:

				break;
			case Direction.NORTHWEST:

				break;
		}
	}

	//Converts angle in degrees to Direction enum
	private Direction DegreesToDirection(float angle)
	{
		if(use4Directions)
		{
			//315-360, 0-45 = NORTH
			if (angle > 315 && angle <= 360 || angle >= 0 && angle <= 45)
			{
				return Direction.NORTH;
			}
			//45-135 = EAST
			else if (angle > 45 && angle <= 135)
			{
				return Direction.EAST;
			}
			//135-225 = SOUTH
			else if (angle > 135 && angle <= 225)
			{
				return Direction.SOUTH;
			}
			else //225-315 = WEST
			{
				return Direction.WEST;
			}
		}
		else
		{
			//337.5-360, 0-22.5 = NORTH
			if (angle > 337.5 && angle <= 360 || angle >= 0 && angle <= 22.5)
			{
				return Direction.NORTH;
			}
			//22.5-67.5 = NORTHEAST
			else if (angle > 22.5 && angle <= 67.5)
			{
				return Direction.NORTHEAST;
			}
			//67.5-112.5 = EAST
			else if (angle > 67.5 && angle <= 112.5)
			{
				return Direction.EAST;
			}
			//112.5-157.5 = SOUTHEAST
			else if (angle > 112.5 && angle <= 157.5)
			{
				return Direction.SOUTHEAST;
			}
			//157.5-202.5 = SOUTH
			else if (angle > 157.5 && angle <= 202.5)
			{
				return Direction.SOUTH;
			}
			//202.5-247.5 = SOUTHWEST
			else if (angle > 202.5 && angle <= 247.5)
			{
				return Direction.SOUTHWEST;
			}
			//247.5-292.5 = WEST
			else if (angle > 247.5 && angle <= 292.5)
			{
				return Direction.WEST;
			}
			else //292.5-337.5 = NORTHWEST
			{
				return Direction.NORTHWEST;
			}
		}
	}
}

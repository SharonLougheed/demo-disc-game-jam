using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains all images of this player
//and changes the images this player sees
public class PlayerRenderer : MonoBehaviour
{
	public int playerNumber = 1; //Will be reduced by 1 to match indices of arrays
	public GameObject[] allPlayers; //Must be in order! May leave this player object blank.
	public GameObject[] respectiveViewObjects; //Make sure the indices align with allPlayers. This is what they will see.
												//Own player number will be ignored
	public bool use4Directions = false; //If true, only uses N, S, E, W textures
	public bool useColorFromParentMaterial = true; //If true, get parent's MeshRenderer's Material's color, overrides colorToApplyToSprites
	public Color colorToApplyToSprites = Color.white; //If white, no change

	private PlayerRenderer[] playerRenderers; //To change direction for this player
	private Animator[] animators; //To change direction for other players
	private Direction[] directionEachPlayerSees; //For efficiency, if hasn't changed
	private bool viewObjectsDisabled = false;

	enum Direction { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST }

	//Formerly Start function
	public void Setup()
	{
		playerNumber = playerNumber - 1; //Reduce by 1 to match indices of arrays

		//Initialize all private arrays
		playerRenderers = new PlayerRenderer[allPlayers.Length];
		animators = new Animator[allPlayers.Length];
		for (int i=0; i< allPlayers.Length; i++)
		{
			if(i != playerNumber && allPlayers[i] != null)
			{
				playerRenderers[i] = allPlayers[i].GetComponentInChildren<PlayerRenderer>();
				animators[i] = respectiveViewObjects[i].GetComponent<Animator>();

				if(useColorFromParentMaterial)
				{
					Material parentMaterial = GetComponentInParent<MeshRenderer>().material;
					if (parentMaterial != null)
					{
						colorToApplyToSprites = parentMaterial.color;
					}
				}
				//Apply color to each sprite renderer if set
				if (!colorToApplyToSprites.Equals(Color.white))
				{
					respectiveViewObjects[i].GetComponent<SpriteRenderer>().color = colorToApplyToSprites;
				}
			}
		}
		directionEachPlayerSees = new Direction[allPlayers.Length];
	}

    //Update is called once per frame
	//Change the textures of other players in this player's view layer
    void Update()
    {
		//Use the direction this player is facing as north in a lil coordinate system
		float thisYRotation = transform.rotation.eulerAngles.y;
		//Go through all other players
		for (int i = 0; i < allPlayers.Length; i++)
		{
			if (i != playerNumber && allPlayers[i] != null)
			{
				//Take the direction the other player is facing
				float thatYRotation = allPlayers[i].transform.rotation.eulerAngles.y;

				//And rotate it by this player's direction to convert to this lil coordinate system
				float convertedYRotation = NormalizeAngle(thatYRotation - thisYRotation);

				//Rotate and change the sprite to match that direction
				playerRenderers[i].ChangeDirectionalSprite(playerNumber, convertedYRotation);
			}
		}
	}

	//Returns an angle between 0 and 360 degrees
	float NormalizeAngle(float angleInDegrees)
	{
		if(angleInDegrees < 0)
		{
			//ex. -20 = 360 + (-20) = 340
			//ex. -500 = 360 + (-140) = 220
			return 360 + (angleInDegrees % 360);
		}
		else
		{
			//ex. 20 = 20
			//ex. 500 = 140
			return angleInDegrees % 360;
		}
	}

	//Called by other PlayerRenderers
	//Changes the animation in the viewObject corresponding to that player
	void ChangeDirectionalSprite(int otherPlayerNum, float rotation)
	{
		if (viewObjectsDisabled)
		{
			return;
		}
		//This is the direction relative to this player
		Direction relativeDirection = DegreesToDirection(rotation);
		//Then rotate the corresponding sprite renderer/animator to face that player
		respectiveViewObjects[otherPlayerNum].transform.LookAt(allPlayers[otherPlayerNum].transform.position, Vector3.up);

		if (directionEachPlayerSees[otherPlayerNum] == relativeDirection)
			return; //an attempt to be slightly more efficient
		directionEachPlayerSees[otherPlayerNum] = relativeDirection;
		
		//dirNum is used by the animator controller
		int dirNum = 0;
		switch (relativeDirection)
		{
			case Direction.NORTH:
				dirNum = 0;
				break;
			case Direction.NORTHEAST:
				dirNum = 1;
				break;
			case Direction.EAST:
				dirNum = 2;
				break;
			case Direction.SOUTHEAST:
				dirNum = 3;
				break;
			case Direction.SOUTH:
				dirNum = 4;
				break;
			case Direction.SOUTHWEST:
				dirNum = 5;
				break;
			case Direction.WEST:
				dirNum = 6;
				break;
			case Direction.NORTHWEST:
				dirNum = 7;
				break;
		}
		//Change to the animation (which can be a single frame, if no animation yet) for that direction
		animators[otherPlayerNum].SetInteger("Direction", dirNum);
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

	public void StartFlashPlayer()
	{
		StartCoroutine(FlashPlayer(Color.red));
	}

	//Based on function from Player script
	IEnumerator FlashPlayer(Color colorToChange)
	{
		float deltaT = 0.1f;
		float t = 0f;
		while(t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(colorToApplyToSprites, colorToChange, t);
			for (int i = 0; i < allPlayers.Length; i++)
			{
				if (i != playerNumber)
				{
					respectiveViewObjects[i].GetComponent<SpriteRenderer>().color = lerpedColor;
				}
			}
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}

		//yield return new WaitForSecondsRealtime(0.1f); //Wait between colors
		t = 0f;
		while (t <= 1.0f)
		{
			Color lerpedColor = Color.Lerp(colorToChange, colorToApplyToSprites, t);
			for (int i = 0; i < allPlayers.Length; i++)
			{
				if (i != playerNumber)
				{
					respectiveViewObjects[i].GetComponent<SpriteRenderer>().color = lerpedColor;
				}
			}
			t += deltaT;
			yield return new WaitForFixedUpdate();
		}
	}

	public void DisableViewObjects()
	{
		viewObjectsDisabled = true;
		for (int i = 0; i < allPlayers.Length; i++)
		{
			if (i != playerNumber)
			{
				respectiveViewObjects[i].SetActive(false);
			}
		}
	}
	public void EnableViewObjects()
	{
		viewObjectsDisabled = false;
		for (int i = 0; i < allPlayers.Length; i++)
		{
			if (i != playerNumber)
			{
				respectiveViewObjects[i].SetActive(true);
			}
		}
	}
}

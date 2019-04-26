using System.Collections;
using UnityEngine;

public class SpaceGimbal : MonoBehaviour
{
    //-- Attach this script to an OUTER SPACE to manipulate the its rotations.

    public bool isCrankingS { get; private set; }               // Denotes whether or not Stars are being cranked
    public bool isCrankingG { get; private set; }               // Denotes whether or not Gimbal is being cranked
    public float crankForce { get; private set; }               // Force multiplier for Rotation of Stars

    public GameObject starsOG;                                  // Inner-most orbit of Stars
    public GameObject starsMG;                                  // Mid-range orbit of Stars
    public GameObject starsBG;                                  // Furthest orbit of Stars

    private float gimbalX;                                      // X Rotation Force for Gimbal
    private float gimbalY;                                      // Y Rotation Force for Gimbal
    private float gimbalZ;                                      // Z Rotation Force for Gimbal

    private IEnumerator crank_S;                                // Coroutine for cranking Stars
    private IEnumerator crank_G;                                // Coroutine for cranking Gimbal

    private void Awake()
    {
        isCrankingS = false;
        isCrankingG = false;

        crankForce = 0.0f;

        gimbalX = 0.0f;
        gimbalY = 0.0f;
        gimbalZ = 0.0f;
    }

    private void Start()
    {
        TurnStarCrank(1, 4);
	}

    private void LateUpdate()
    {
        transform.Rotate(gimbalX * Time.deltaTime, gimbalY * Time.deltaTime, gimbalZ * Time.deltaTime);
    }

    /// <summary>Winds the Star Crank up/down to [force] over [crankTime] in seconds.</summary>
    public void TurnStarCrank(float force, float crankTime)
    {
        if(isCrankingS)
        {
            StopCoroutine(crank_S);
        }

        crank_S = CrankStars(force, crankTime);
        StartCoroutine(crank_S);
    }

    /// <summary>Winds the Gimbal up/down to specified Rotation Angles over [crankTime] in seconds.</summary>
    public void TurnGimbalCrank(float xForce, float yForce, float zForce, float gimbalTime)
    {
        if (isCrankingG)
        {
            StopCoroutine(crank_G);
        }

        crank_G = CrankGimbal(xForce, yForce, zForce, gimbalTime);
        StartCoroutine(crank_G);
    }

    /// <summary>Stops Star movement over [crankTime] in seconds.</summary>
    public void StarBreak(float crankTime)
    {
        TurnStarCrank(0.0f, crankTime);
    }

    /// <summary>Stops Gimbal movement over [crankTime] in seconds.</summary>
    public void GimbalBreak(float gimbalTime)
    {
        TurnGimbalCrank(0.0f, 0.0f, 0.0f, gimbalTime);
    }

    private IEnumerator CrankStars(float force, float crankTime)
    {
        isCrankingS = true;
        
        float diffSlice = (force - crankForce) / 50;

        for(int i = 50; i > 0; i--)
        {
            crankForce += diffSlice;

            starsOG.GetComponent<StarRotate>().ForceGear(crankForce);
            starsMG.GetComponent<StarRotate>().ForceGear(crankForce);
            starsBG.GetComponent<StarRotate>().ForceGear(crankForce);

            yield return new WaitForSeconds(crankTime / 50);
        }

        crankForce = force;

        starsOG.GetComponent<StarRotate>().ForceGear(crankForce);
        starsMG.GetComponent<StarRotate>().ForceGear(crankForce);
        starsBG.GetComponent<StarRotate>().ForceGear(crankForce);

        isCrankingS = false;
        
        yield return null;
	}

    private IEnumerator CrankGimbal(float xForce, float yForce, float zForce, float gimbalTime)
    {
        isCrankingG = true;

        float xDiffSlice = (gimbalX - xForce) / 50;
        float yDiffSlice = (gimbalX - yForce) / 50;
        float zDiffSlice = (gimbalX - zForce) / 50;

        for (int i = 50; i > 0; i--)
        {
            gimbalX += xDiffSlice;
            gimbalY += yDiffSlice;
            gimbalZ += zDiffSlice;

            yield return new WaitForSeconds(gimbalTime / 50);
        }

        gimbalX = xForce;
        gimbalY = yForce;
        gimbalZ = zForce;

        isCrankingG = false;

        yield return null;
    }
}
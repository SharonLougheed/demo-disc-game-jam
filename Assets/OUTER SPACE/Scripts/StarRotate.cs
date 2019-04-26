using System.Collections;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    //-- Attach this script to a Point Of Origin (POO) for Stars you wish to rotate around a spherical grid.
    
    public float gearForce { get; private set; }                // Force multiplier for Rotation
    public bool isShifting { get; private set; }                // Denotes whether or not this orbit is shifting Rotation Angle(s)

    public float xRot = 0.01f;                                  // X Rotation Angle
    public float yRot = 0.01f;                                  // Y Rotation Angle
    public float zRot = 0.01f;                                  // Z Rotation Angle

    private IEnumerator shift_G;                                // Coroutine for shifting Rotation Angle(s)

    private void Awake()
    {
        gearForce = 0.0f;
        isShifting = false;
    }

    private void LateUpdate()
    {
        transform.Rotate(xRot * gearForce * Time.deltaTime, yRot * gearForce * Time.deltaTime, zRot * gearForce * Time.deltaTime);
    }

    /// <summary>This is abrupt—consider using TurnStarCrank() from the SpaceGimbal script on OUTER SPACE.</summary>
    public void ForceGear(float force)
    {
        gearForce = force;
    }

    /// <summary>Shifts Rotation Angles to [x], [y], and [z] over 5 seconds.</summary>
    public void Shift(float x, float y, float z)
    {
        if(isShifting)
        {
            StopCoroutine(shift_G);
        }

        shift_G = ShiftGears(x, y, z);
        StartCoroutine(shift_G);
    }

    private IEnumerator ShiftGears(float x, float y, float z)
    {
        isShifting = true;

        float xDiffSlice = (x - xRot) / 50;
        float yDiffSlice = (x - yRot) / 50;
        float zDiffSlice = (x - zRot) / 50;

        float rotTime = 5.0f;

        for(int i = 50; i > 0; i--)
        {
            xRot += xDiffSlice;
            yRot += yDiffSlice;
            zRot += zDiffSlice;

            yield return new WaitForSeconds(rotTime / 50);
        }

        xRot = x;
        yRot = y;
        zRot = z;

        isShifting = false;

        yield return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ControllerNumber = 1;
    public float RotationSpeed = 100f;
    public float MovementSpeed = 8f;
    public float SpeedUpPercent = 0f;


    private void Update()
    {
        if (Input.GetAxis("Strafe" + ControllerNumber) > 0)
        {
            float strafeMovementAmount = Input.GetAxis("Horizontal" + ControllerNumber) * MovementSpeed * Time.deltaTime * (1 + (SpeedUpPercent / 100));
            gameObject.transform.Translate(new Vector3(strafeMovementAmount, 0f, 0f));
        }
        else
        {
            float rotationAmount = Input.GetAxis("Horizontal" + ControllerNumber) * RotationSpeed * Time.deltaTime * (1 + (SpeedUpPercent / 100));
            gameObject.transform.Rotate(new Vector3(0f, rotationAmount));
        }

        float movementAmount = Input.GetAxis("Vertical" + ControllerNumber) * MovementSpeed * Time.deltaTime * (1 + (SpeedUpPercent / 100));
        gameObject.transform.Translate(new Vector3(0f, 0f, movementAmount));
    }
}

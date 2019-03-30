using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber = 1;
    public float RotationSpeed = 100f;
    public float MovementSpeed = 8f;


    private void Update()
    {
        if (Input.GetAxis("Strafe" + PlayerNumber) > 0)
        {
            float strafeMovementAmount = Input.GetAxis("Horizontal" + PlayerNumber) * MovementSpeed * Time.deltaTime;
            gameObject.transform.Translate(new Vector3(strafeMovementAmount, 0f, 0f));
        }
        else
        {
            float rotationAmount = Input.GetAxis("Horizontal" + PlayerNumber) * RotationSpeed * Time.deltaTime;
            gameObject.transform.Rotate(new Vector3(0f, rotationAmount));
        }

        float movementAmount = Input.GetAxis("Vertical" + PlayerNumber) * MovementSpeed * Time.deltaTime;
        gameObject.transform.Translate(new Vector3(0f, 0f, movementAmount));
    }
}

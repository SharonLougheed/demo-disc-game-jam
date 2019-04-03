using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ControllerNumber = 1;

    // Rotation Speed at 100f was the original setting
    // Movement Speed at 8f was the original setting
    public ControlSettings Settings;



    public float SpeedUpPercent = 0f;


    private void Update()
    {
        CheckTurnStrafe();
        CheckForwardBack();

        if (Input.GetButtonDown("Left" + ControllerNumber))
        {
            var player = GetComponent<Player>();
            player.leftHand.Punch();
        }
        if (Input.GetButtonDown("Right" + ControllerNumber))
        {
            var player = GetComponent<Player>();
            player.rightHand.Punch();
        }
    }


    private void CheckForwardBack()
    {
        float movementAmount = Input.GetAxis("Vertical" + ControllerNumber)
            * Settings.MovementSpeed
            * Time.deltaTime
            * (1 + (SpeedUpPercent / 100));

        gameObject.transform.Translate(new Vector3(0f, 0f, movementAmount));
    }

    private void CheckTurnStrafe()
    {
        if (Input.GetAxis("Strafe" + ControllerNumber) > 0)
        {
            float strafeMovementAmount = Input.GetAxis("Horizontal" + ControllerNumber)
                * Settings.MovementSpeed
                * Time.deltaTime
                * (1 + (SpeedUpPercent / 100));

            gameObject.transform.Translate(new Vector3(strafeMovementAmount, 0f, 0f));
        }
        else
        {
            float rotationAmount = Input.GetAxis("Horizontal" + ControllerNumber)
                * Settings.RotationSpeed
                * Time.deltaTime
                * (1 + (SpeedUpPercent / 100));

            gameObject.transform.Rotate(new Vector3(0f, rotationAmount));
        }
    }
}

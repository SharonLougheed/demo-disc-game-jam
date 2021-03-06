﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ControllerNumber = 1;
    public bool ControllerActive = true;
    public float SpeedUpPercent = 0f;
    public float groundLevel = 0.5f;

    // Rotation Speed at 100f was the original setting
    // Movement Speed at 8f was the original setting
    public ControlSettings Settings;

    public CharacterController CharController;

    private void Update()
    {
        CheckPause();
        if (!ControllerActive)
        {
            return;
        }

        CheckTurnStrafe();
        CheckForwardBack();
        CheckPunches();
        GroundPlayer();
    }

    private int GetControllerNumber() => ControllerNumber + (int)Settings.ControllerGroup;

    private void GroundPlayer()
    {
        // Do this differently
        if (transform.position.y != groundLevel)
        {
            transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
        }
    }

    private void CheckPunches()
    {
        int controllerNumber = GetControllerNumber();
        var player = GetComponent<Player>();
        if (Input.GetButtonDown("Left" + GetControllerNumber()))
        {
            player.leftHand.Punch();
        }
        if (Input.GetButtonDown("Right" + GetControllerNumber()))
        {
            player.rightHand.Punch();
        }
    }

    private void CheckForwardBack()
    {
        float movementAmount = Input.GetAxis("Vertical" + GetControllerNumber())
            * Settings.MovementSpeed
            * Time.deltaTime
            * (1 + (SpeedUpPercent / 100));

        Vector3 moveVector = new Vector3(0f, 0f, movementAmount);

        //gameObject.transform.Translate(new Vector3(0f, 0f, movementAmount));
        CharController.Move(transform.TransformDirection(moveVector));
    }

    private void CheckTurnStrafe()
    {
        switch (Settings.ControllerGroup)
        {
            case ControllerType.FPS:
                TurnAndStrafePlayer();
                break;
            default:
                if (Input.GetAxis("Strafe" + GetControllerNumber()) > 0)
                {
                    StrafePlayer();
                }
                else
                {
                    TurnPlayer();
                }
                break;
        }

    }

    private void TurnAndStrafePlayer()
    {
        float rotationAmount = Input.GetAxis("RightHorizontal" + GetControllerNumber())
            * Settings.RotationSpeed
            * Time.deltaTime
            * (1 + (SpeedUpPercent / 100));

        gameObject.transform.Rotate(new Vector3(0f, rotationAmount));

        float strafeMovementAmount = Input.GetAxis("Horizontal" + GetControllerNumber())
                        * Settings.MovementSpeed
                        * Time.deltaTime
                        * (1 + (SpeedUpPercent / 100));

        Vector3 moveVector = new Vector3(strafeMovementAmount, 0f, 0f);

        //gameObject.transform.Translate(new Vector3(strafeMovementAmount, 0f, 0f));
        CharController.Move(transform.TransformDirection(moveVector));
    }

    private void TurnPlayer()
    {
        float rotationAmount = Input.GetAxis("Horizontal" + GetControllerNumber())
            * Settings.RotationSpeed
            * Time.deltaTime
            * (1 + (SpeedUpPercent / 100));

        gameObject.transform.Rotate(new Vector3(0f, rotationAmount));
    }

    private void StrafePlayer()
    {
        float strafeMovementAmount = Input.GetAxis("Horizontal" + GetControllerNumber())
                        * Settings.MovementSpeed
                        * Time.deltaTime
                        * (1 + (SpeedUpPercent / 100));

        Vector3 moveVector = new Vector3(strafeMovementAmount, 0f, 0f);

        //gameObject.transform.Translate(new Vector3(strafeMovementAmount, 0f, 0f));
        CharController.Move(transform.TransformDirection(moveVector));
    }

    private bool pauseButtonDown = false;

    private void CheckPause()
    {
        if (Input.GetAxisRaw("Pause" + GetControllerNumber()) != 0)
        {
            if (!pauseButtonDown)
            {
                pauseButtonDown = true;
                Pause pauseMenu = GameObject.FindObjectOfType<Pause>();
                pauseMenu.TogglePause();
            }
        }
        if (Input.GetAxisRaw("Pause" + GetControllerNumber()) == 0)
        {
            pauseButtonDown = false;
        }
    }

    public void SetNewPositionRotation(Vector3 newPosition, Quaternion newRotation)
    {
        CharController.enabled = false;
        transform.position = newPosition;
        transform.rotation = newRotation;
        CharController.enabled = true;
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float PrimaryHorizontal()
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("LeftStickXAxis");
        fHorizontalAxis += Input.GetAxis("KeyboardXAxis");
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    public static float PrimaryVertical()
    {
        float fVerticalAxis = 0.0f;
        fVerticalAxis += Input.GetAxis("LeftStickYAxis");
        fVerticalAxis += Input.GetAxis("KeyboardYAxis");
        return Mathf.Clamp(fVerticalAxis, -1.0f, 1.0f);
    }

    public static float SecondaryHorizontal(float a_fMouseXAxis)
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("RightStickXAxis");
        fHorizontalAxis += a_fMouseXAxis;
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    public static float SecondaryVertical(float a_fMouseYAxis)
    {
        float fVertialAxis = 0.0f;
        fVertialAxis += Input.GetAxis("RightStickYAxis");
        fVertialAxis += a_fMouseYAxis;
        return Mathf.Clamp(fVertialAxis, -1.0f, 1.0f);
    }

    public static float LeftTrigger()
    {
        float fLeftTriggerAxis = 0.0f;
        fLeftTriggerAxis += Input.GetAxis("LeftTrigger");
        return Mathf.Clamp(fLeftTriggerAxis, -1.0f, 1.0f);
    }

    public static float RightTrigger()
    {
        float fRightTriggerAxis = 0.0f;
        fRightTriggerAxis += Input.GetAxis("RightTrigger");
        return Mathf.Clamp(fRightTriggerAxis, -1.0f, 1.0f);
    }

    public static bool AButton()
    {
        return Input.GetButtonDown("AButton");
    }

    public static bool BButton()
    {
        return Input.GetButtonDown("BButton");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("XButton");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("YButton");
    }

    public static Vector3 PrimaryInput()
    {
        return new Vector3(PrimaryHorizontal(), 0.0f, PrimaryVertical());
    }

    public static Vector3 SecondaryInput(Vector3 a_v3MouseInput)
    {
        return new Vector3(SecondaryHorizontal(a_v3MouseInput.x), 0.0f, SecondaryVertical(a_v3MouseInput.z));
    }
}

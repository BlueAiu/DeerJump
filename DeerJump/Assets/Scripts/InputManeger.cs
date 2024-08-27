using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManeger
{
    public static bool IsCharging()
    {
        return Keyboard.current.spaceKey.isPressed;
    }

    public static float HorizontalAxis()
    {
        float value = 0f;

        if (Keyboard.current.dKey.isPressed) value++;
        if (Keyboard.current.aKey.isPressed) value--;

        value = Mathf.Clamp(value, -1f, 1f);
        return value;
    }

    public static bool IsPauseButton()
    {
        return Keyboard.current.pKey.wasPressedThisFrame;
    }

    public static bool IsDecision()
    {
        return Keyboard.current.zKey.wasPressedThisFrame;
    }

    public static bool IsCanceled() 
    {
        return Keyboard.current.xKey.wasPressedThisFrame;
    }
}

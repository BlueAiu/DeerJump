using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManeger
{
    public static bool IsCharging()
    {
        if(Gamepad.current != null)
        {
            if (Gamepad.current.rightTrigger.isPressed) return true;
            if (Gamepad.current.rightShoulder.isPressed) return true;
        }

        return Keyboard.current.spaceKey.isPressed
            || Keyboard.current.zKey.isPressed;
    }

    public static float HorizontalAxis()
    {
        float value = 0f;

        if(Gamepad.current != null)
        {
            value += Gamepad.current.leftStick.ReadValue().x;
            value += Gamepad.current.dpad.ReadValue().x;

            if (value != 0f) return Mathf.Clamp(value, -1f, 1f);
        }

        if (Keyboard.current.dKey.isPressed) value++;
        if (Keyboard.current.aKey.isPressed) value--;
        if (Keyboard.current.rightArrowKey.isPressed) value++;
        if (Keyboard.current.leftArrowKey.isPressed) value--;

        value = Mathf.Clamp(value, -1f, 1f);
        return value;
    }

    public static bool IsPauseButton()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame) return true;
            if (Gamepad.current.selectButton.wasPressedThisFrame) return true;
        }

        return Keyboard.current.pKey.wasPressedThisFrame
            || Keyboard.current.escapeKey.wasPressedThisFrame;
    }

    public static bool IsDecision()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.rightTrigger.wasPressedThisFrame) return true;
            if (Gamepad.current.rightShoulder.wasPressedThisFrame) return true;
        }

        return Keyboard.current.zKey.wasPressedThisFrame
            || Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    public static bool IsCanceled() 
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftTrigger.wasPressedThisFrame) return true;
            if (Gamepad.current.leftShoulder.wasPressedThisFrame) return true;
        }

        return Keyboard.current.xKey.wasPressedThisFrame
            || Keyboard.current.backspaceKey.wasPressedThisFrame;
    }
}

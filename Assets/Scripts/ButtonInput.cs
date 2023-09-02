using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{

    public static int Direction { get; private set; } = 0;
    public static int Jump { get; private set; } = 0;

    public void ButtonDownLeft()
    {
        Direction = -1;
    }

    public void ButtonUpLeft()
    {
        Direction = 0;
    }

    public void ButtonDownRight()
    {
        Direction = 1;
    }

    public void ButtonUpRight()
    {
        Direction = 0;
    }

    public void ButtonDownJump()
    {
        Jump = 1;
    }

    public void ButtonUpJump()
    {
        Jump = 0;
    }

    public void ButtonInputMessage(string m)
    {
        switch (m)
        {
            case "left":
                
            case "right":
                Debug.Log("Right");
                break;
            case "jump":
                Debug.Log("Up");
                break;
            case "reset":
                Debug.Log("Up");
                break;
            case "help":
                break;
            case "pause":
                break;
        }
    }
}

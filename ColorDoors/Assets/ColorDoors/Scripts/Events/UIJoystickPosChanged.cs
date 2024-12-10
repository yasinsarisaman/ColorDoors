using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct JoystickPositionChangedEvent
{
    public JoystickPositionChangedEvent(JoystickPosition jp)
    {
        JP = jp;
    }

    public JoystickPosition JP;
}
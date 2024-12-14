using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableJoystickExtras : MonoBehaviour
{
    [SerializeField] private GameObject _leftJoystick;
    [SerializeField] private GameObject _rightJoystick;

    private void Start()
    {
        GameHelper.LoadJoystickPosition();
        ChangeJoystickPositionTo(GameHelper.GetJoystickPosition());
    }

    private void OnEnable()
    {
        EventBus<JoystickPositionChangedEvent>.AddListener(OnJoystickPositionChanged);
    }

    private void OnDisable()
    {
        EventBus<JoystickPositionChangedEvent>.RemoveListener(OnJoystickPositionChanged);
    }

    private void OnJoystickPositionChanged(object sender, JoystickPositionChangedEvent jpChangedEvent)
    {
        ChangeJoystickPositionTo(jpChangedEvent.JP);
    }

    private void ChangeJoystickPositionTo(JoystickPosition jp)
    {
        //Debug.Log("Set position to " + jp);
        switch (jp)
        {
            case JoystickPosition.JoystickPosition_LEFT:
                _leftJoystick.SetActive(true);
                _rightJoystick.SetActive(false);
                break;
            case JoystickPosition.JoystickPosition_RIGHT:
                _rightJoystick.SetActive(true);
                _leftJoystick.SetActive(false);
                break;
        }
    }
}

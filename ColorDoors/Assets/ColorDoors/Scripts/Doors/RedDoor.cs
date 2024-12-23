using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using UnityEngine;

public class RedDoor : MonoBehaviour, Interactable
{
    public int doorId;
    public bool isOpened;

    private Vector3 _initialDoorRotation;
    private float _timer;
    private void Start()
    {
        _timer = 0.0f;
        isOpened = false;
        _initialDoorRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (!isOpened) return;
        switch (_timer)
        {
            case > 0.0f:
                _timer -= Time.deltaTime;
                break;
            case 0.0f:
            case < 0.0f:
                _timer = 0.0f;
                CloseDoor();
                break;
        }
    }

    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnAnyRedDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnAnyRedDoorStatusChanged);
    }

    private void OnAnyRedDoorStatusChanged(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is RedDoorStatusChangedEvent redDoorStatusChangedEvent)
        {
            if (redDoorStatusChangedEvent.DoorId == doorId) 
            {
                if (!redDoorStatusChangedEvent.IsOpened)
                {
                    OpenDoor();
                    _timer = 1.5f;
                }
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        transform.eulerAngles = new Vector3(_initialDoorRotation.x, _initialDoorRotation.y-90, _initialDoorRotation.z);
        //Debug.Log("initial door rotation: " + _initialDoorRotation + "   euler new: " + transform.eulerAngles);
    }

    private void CloseDoor()
    {
        transform.eulerAngles = _initialDoorRotation;
    }
}

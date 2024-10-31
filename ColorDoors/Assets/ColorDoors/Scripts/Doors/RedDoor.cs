using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using UnityEngine;

public class RedDoor : MonoBehaviour
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
        EventBus<RedDoorStatusChangedEvent>.AddListener(OnAnyRedDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<RedDoorStatusChangedEvent>.RemoveListener(OnAnyRedDoorStatusChanged);
    }

    private void OnAnyRedDoorStatusChanged(object sender, RedDoorStatusChangedEvent redDoorStatusChangedEvent)
    {
        if (redDoorStatusChangedEvent.DoorId == doorId) 
        {
            if (!redDoorStatusChangedEvent.IsOpened)
            {
                OpenDoor();
                _timer = 3.0f;
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        transform.eulerAngles = new Vector3(_initialDoorRotation.x, -90, _initialDoorRotation.z);
    }

    private void CloseDoor()
    {
        transform.eulerAngles = _initialDoorRotation;
    }
}

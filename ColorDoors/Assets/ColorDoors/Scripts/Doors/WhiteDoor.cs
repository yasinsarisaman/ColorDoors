using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using UnityEngine;

public class WhiteDoor : MonoBehaviour
{
    [SerializeField] private float _doorOpenTime;
    [SerializeField] private Vector3 _doorOpenVector;
    public int doorId;

    private Vector3 _initialDoorPosition;
    private float _timer;
    private bool _isOpen;
    private void Start()
    {
        _initialDoorPosition = transform.position;
    }

    private void Update()
    {
        if (!_isOpen) return;
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
        EventBus<WhiteDoorStatusChangedEvent>.AddListener(OnAnyWhiteDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<WhiteDoorStatusChangedEvent>.RemoveListener(OnAnyWhiteDoorStatusChanged);
    }

    private void OnAnyWhiteDoorStatusChanged(object sender, WhiteDoorStatusChangedEvent whiteDoorStatusChangedEvent)
    {
        if (whiteDoorStatusChangedEvent.DoorId == doorId) 
        {
            OpenDoor();
            _timer = _doorOpenTime;
        }
    }

    private void OpenDoor()
    {
        _isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x, _initialDoorPosition.y, _initialDoorPosition.z) + _doorOpenVector;
    }

    private void CloseDoor()
    {
        _isOpen = false;
        transform.position = _initialDoorPosition;
    }
}
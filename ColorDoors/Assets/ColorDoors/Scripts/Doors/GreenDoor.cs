using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using UnityEngine;

public class GreenDoor : MonoBehaviour
{
    [SerializeField] private float timeToClose;
    public int doorId;
    public float doorAdditionalTime;
    public bool isOpen;

    private Vector3 _initialDoorPosition;
    private float _timer;
    private void Start()
    {
        _timer = 0.0f;
        isOpen = false;
        _initialDoorPosition = transform.position;
    }

    private void Update()
    {
        if (!isOpen) return;
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
        EventBus<GreenDoorStatusChangedEvent>.AddListener(OnAnyGreenDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<GreenDoorStatusChangedEvent>.RemoveListener(OnAnyGreenDoorStatusChanged);
    }

    private void OnAnyGreenDoorStatusChanged(object sender, GreenDoorStatusChangedEvent greenDoorStatusChangedEvent)
    {
        if (greenDoorStatusChangedEvent.DoorId == doorId) 
        {
            if (greenDoorStatusChangedEvent.ShouldBeOpened)
            {
                OpenDoor();
                _timer = timeToClose;
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x + .4f, _initialDoorPosition.y, _initialDoorPosition.z);
    }

    private void CloseDoor()
    {
        isOpen = false;
        transform.position = _initialDoorPosition;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;

public class SpeedDoor : MonoBehaviour, Interactable
{
    public int doorId;
    public float boostFactor;

    [SerializeField] Vector3 _doorOpenVector;
    
    private float _timer;
    private bool _isOpen;
    private Vector3 _initialDoorPosition;
    
    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnSpeedDoorStatusChangedEvent);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnSpeedDoorStatusChangedEvent);
    }

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

    private void OnSpeedDoorStatusChangedEvent(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is SpeedDoorStatusChangedEvent speedDoorStatusChangedEvent)
        {
            if (speedDoorStatusChangedEvent.DoorId == doorId) 
            {
                //Activate boost button on UI
                OpenDoor();
                _timer = 3.0f;
                //EventBus<BoostPlayerSpeed>.Emit(this,new BoostPlayerSpeed(speedDoorStatusChangedEvent.BoostFactor));
            }
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
using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using UnityEngine;

public class OrangeDoor : MonoBehaviour, Interactable
{
    public float timeToOpenMazeWalls;
    public int doorId;
    [SerializeField] private float timeToClose;
    [SerializeField] private bool isOpen;

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
        EventBus<IDoorStatusChangedEvent>.AddListener(OnAnyOrangeDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnAnyOrangeDoorStatusChanged);
    }

    private void OnAnyOrangeDoorStatusChanged(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is OrangeDoorStatusChangedEvent orangeDoorStatusChangedEvent)
        {
            if (orangeDoorStatusChangedEvent.DoorId == doorId) 
            {
                OpenDoor();
                _timer = timeToClose;
            }
        }
    }
    
    private void OpenDoor()
    {
        isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x, _initialDoorPosition.y + 1.25f, _initialDoorPosition.z);
        
        EventBus<OpenDynamicMazeWalls>.Emit(this,new OpenDynamicMazeWalls(timeToOpenMazeWalls));
    }

    private void CloseDoor()
    {
        isOpen = false;
        transform.position = _initialDoorPosition;
    }
}

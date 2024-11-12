using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using TMPro;
using UnityEngine;

public class PurpleDoor : MonoBehaviour
{
    [SerializeField] TextMeshPro additionalTimeText;
    public int doorId;
    public float doorFreezeTime;
    public bool isOpen;

    private Vector3 _initialDoorPosition;
    private float _timer;

    private void Awake()
    {
        additionalTimeText.text = "* " + doorFreezeTime;
    }

    private void Start()
    {
        isOpen = false;
        _initialDoorPosition = transform.position;
    }
    
    private void OnEnable()
    {
        EventBus<PurpleDoorStatusChangedEvent>.AddListener(OnAnyPurpleDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<PurpleDoorStatusChangedEvent>.RemoveListener(OnAnyPurpleDoorStatusChanged);
    }

    private void OnAnyPurpleDoorStatusChanged(object sender, PurpleDoorStatusChangedEvent purpleDoorStatusChangedEvent)
    {
        if (purpleDoorStatusChangedEvent.DoorId == doorId) 
        {
            if (purpleDoorStatusChangedEvent.ShouldBeOpened)
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x + .4f, _initialDoorPosition.y, _initialDoorPosition.z);
        additionalTimeText.text = "";
    }
}
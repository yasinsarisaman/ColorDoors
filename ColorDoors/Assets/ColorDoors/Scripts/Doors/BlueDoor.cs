using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;

public class BlueDoor : MonoBehaviour, Interactable
{
    public int doorId;
    [SerializeField] private Transform correlatedDoorTransform;
    [SerializeField] private float doorOffset;
    [SerializeField] private bool onlyEntrance;
    [SerializeField] private bool onlyExit;
    
    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnBlueDoorStatusChangedEvent);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnBlueDoorStatusChangedEvent);
    }

    private void OnBlueDoorStatusChangedEvent(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is BlueDoorStatusChangedEvent blueDoorStatusChangedEvent)
        {
            if (blueDoorStatusChangedEvent.DoorId == doorId && !onlyExit) 
            {
                EventBus<TeleportPlayer>.Emit(this,new TeleportPlayer(correlatedDoorTransform,doorOffset));
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    public int doorId;
    [SerializeField] private Transform correlatedDoorTransform;
    [SerializeField] private float doorOffset;
    
    private void OnEnable()
    {
        EventBus<BlueDoorStatusChangedEvent>.AddListener(OnBlueDoorStatusChangedEvent);
    }

    private void OnDisable()
    {
        EventBus<BlueDoorStatusChangedEvent>.RemoveListener(OnBlueDoorStatusChangedEvent);
    }

    private void OnBlueDoorStatusChangedEvent(object sender, BlueDoorStatusChangedEvent blueDoorStatusChangedEvent)
    {
        if (blueDoorStatusChangedEvent.DoorId == doorId) 
        {
            EventBus<TeleportPlayer>.Emit(this,new TeleportPlayer(correlatedDoorTransform,doorOffset));
        }
    }
}
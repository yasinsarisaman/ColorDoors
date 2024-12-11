using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;

public class BlueDoor : MonoBehaviour, Interactable
{
    public int doorId;
    public bool onlyEntrance;
    public bool onlyExit;
    [SerializeField] private Transform correlatedDoorTransform;
    [SerializeField] private float doorOffset;
    
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
            if (blueDoorStatusChangedEvent.DoorId == doorId && blueDoorStatusChangedEvent.OnlyExit == false) 
            {
                EventBus<TeleportPlayer>.Emit(this,new TeleportPlayer(correlatedDoorTransform,doorOffset));
            }
        }
    }
}
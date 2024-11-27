using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using UnityEngine;

public class FinishDoor : MonoBehaviour, Interactable
{
    private void OnEnable()
    {
        EventBus<FinishDoorStatusChangedEvent>.AddListener(OnLevelFinishDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<FinishDoorStatusChangedEvent>.RemoveListener(OnLevelFinishDoorStatusChanged);
    }

    private void OnLevelFinishDoorStatusChanged(object sender, FinishDoorStatusChangedEvent finishDoorStatusChangedEvent)
    {
        EventBus<LevelCompletedEvent>.Emit(this, new LevelCompletedEvent(CompletionStates.CompletionState_WIN));
    }
}
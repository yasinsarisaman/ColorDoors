using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject BoostButton;
    private float _timer;

    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnSpeedDoorOpened);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnSpeedDoorOpened);
    }

    void Update()
    {
        switch (_timer)
        {
            case > 0.0f:
                _timer -= Time.deltaTime;
                break;
            case 0.0f:
            case < 0.0f:
                _timer = 0.0f;
                OnSpeedDoorClosed();
                break;
        }
    }

    private void OnSpeedDoorOpened(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is SpeedDoorStatusChangedEvent speedDoorStatusChangedEvent)
        {
            BoostButton.SetActive(true);
            _timer = 3.0f;
        }
    }

    private void OnSpeedDoorClosed()
    {
        // TODO make it with an event?
        BoostButton.SetActive(false);
    }

    public void EmitPlayerBoostEvent()
    {
        EventBus<BoostPlayerSpeed>.Emit(this, new BoostPlayerSpeed());
    }
}

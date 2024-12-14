using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Game;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorOpenClip;
    [SerializeField] private AudioClip _blueDoorOpenClip;
    [SerializeField] private AudioClip _blueDoorOnlyExitOpenClip;
    [SerializeField] private AudioClip _finishDoorOpenClip;
    [SerializeField] private AudioClip _UIClickClip;
    [SerializeField] private AudioClip _timeCriticalClip;

    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnAnyDoorStatusChanged);
        EventBus<FinishDoorStatusChangedEvent>.AddListener(OnFinishDoorStatusChanged);
        EventBus<UIButtonElementClickedEvent>.AddListener(OnUIButtonClicked);
        EventBus<TimeCriticalEvent>.AddListener(OnGetTimeCritical);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnAnyDoorStatusChanged);
        EventBus<FinishDoorStatusChangedEvent>.RemoveListener(OnFinishDoorStatusChanged);
        EventBus<UIButtonElementClickedEvent>.RemoveListener(OnUIButtonClicked);
        EventBus<TimeCriticalEvent>.RemoveListener(OnGetTimeCritical);
    }

    private void OnAnyDoorStatusChanged(object sender, IDoorStatusChangedEvent anyDoor)
    {
        if (anyDoor is BlueDoorStatusChangedEvent blueDoorStatusChangedEvent)
        {
            if (blueDoorStatusChangedEvent.OnlyExit)
            {
                _audioSource.PlayOneShot(_blueDoorOnlyExitOpenClip);
            }
            else
            {
                _audioSource.PlayOneShot(_blueDoorOpenClip);
            }
        }
        else
        {
            /* Play generic door open sound effect */
            _audioSource.PlayOneShot(_doorOpenClip);
        }

    }

    private void OnFinishDoorStatusChanged(object sender, FinishDoorStatusChangedEvent finishDoorStatusChangedEvent)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_finishDoorOpenClip);
    }

    public void PlayOneShotUIClick()
    {
        EventBus<UIButtonElementClickedEvent>.Emit(this, new UIButtonElementClickedEvent());
    }
    
    private void OnUIButtonClicked(object sender, UIButtonElementClickedEvent uıButtonElementClickedEvent)
    {
        _audioSource.PlayOneShot(_UIClickClip);
    }

    private void OnGetTimeCritical(object sender, TimeCriticalEvent timeCriticalEvent)
    {
        if (timeCriticalEvent.IsTimeCritical)
        {
            _audioSource.PlayOneShot(_timeCriticalClip);
        }
        else
        {
            _audioSource.Stop();
        }
    }
}

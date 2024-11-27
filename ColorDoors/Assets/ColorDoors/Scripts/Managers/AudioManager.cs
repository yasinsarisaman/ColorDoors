using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using ColorDoors.Scripts.Events.Doors;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorOpenClip;
    [SerializeField] private AudioClip _finishDoorOpenClip;
    [SerializeField] private AudioClip _UIClickClip;

    private void OnEnable()
    {
        EventBus<IDoorStatusChangedEvent>.AddListener(OnAnyDoorStatusChanged);
        EventBus<FinishDoorStatusChangedEvent>.AddListener(OnFinishDoorStatusChanged);
        EventBus<UIButtonElementClickedEvent>.AddListener(OnUIButtonClicked);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnAnyDoorStatusChanged);
        EventBus<FinishDoorStatusChangedEvent>.RemoveListener(OnFinishDoorStatusChanged);
        EventBus<UIButtonElementClickedEvent>.RemoveListener(OnUIButtonClicked);
    }

    private void OnAnyDoorStatusChanged(object sender, IDoorStatusChangedEvent anyDoor)
    {
        _audioSource.PlayOneShot(_doorOpenClip);
    }

    private void OnFinishDoorStatusChanged(object sender, FinishDoorStatusChangedEvent finishDoorStatusChangedEvent)
    {
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
}

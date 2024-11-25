using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using TMPro;
using UnityEngine;

public class PurpleDoor : MonoBehaviour
{
    [SerializeField] TextMeshPro additionalTimeText;
    [SerializeField] private Vector3 _doorOpenVector;
    [SerializeField] private Animator _animatorTimerClockImg;
    [SerializeField] private Animator _animatorTimerClockText;
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
                _timer = 1.0f;
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x, _initialDoorPosition.y, _initialDoorPosition.z) + _doorOpenVector;
        _animatorTimerClockImg.SetBool("TimerScaleAnim", true);
        _animatorTimerClockText.SetBool("TimerTextAnimPurple", true);
        additionalTimeText.text = "";
    }
    
    private void CloseDoor()
    {
        isOpen = false;
        _animatorTimerClockImg.SetBool("TimerScaleAnim", false);
        _animatorTimerClockText.SetBool("TimerTextAnimPurple", false);
        transform.position = _initialDoorPosition;
    }
}
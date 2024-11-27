using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using TMPro;
using UnityEngine;

public class GreenDoor : MonoBehaviour, Interactable
{
    [SerializeField] private float timeToClose;
    [SerializeField] private TextMeshPro additionalTimeText;
    [SerializeField] private Vector3 _doorOpenVector;
    [SerializeField] private Animator _animatorTimerClockImg;
    [SerializeField] private Animator _animatorTimerClockText;
    
    public int doorId;
    public float doorAdditionalTime;
    public bool isOpen;

    private Vector3 _initialDoorPosition;
    private float _timer;

    private void Awake()
    {
        additionalTimeText.text = "+ " + doorAdditionalTime;
    }

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
        EventBus<IDoorStatusChangedEvent>.AddListener(OnAnyGreenDoorStatusChanged);
    }

    private void OnDisable()
    {
        EventBus<IDoorStatusChangedEvent>.RemoveListener(OnAnyGreenDoorStatusChanged);
    }

    private void OnAnyGreenDoorStatusChanged(object sender, IDoorStatusChangedEvent doorStatusChangedEvent)
    {
        if (doorStatusChangedEvent is GreenDoorStatusChangedEvent greenDoorStatusChangedEvent)
        {
            if (greenDoorStatusChangedEvent.DoorId == doorId) 
            {
                if (greenDoorStatusChangedEvent.ShouldBeOpened)
                {
                    OpenDoor();
                    _timer = timeToClose;
                }
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        transform.position = new Vector3(_initialDoorPosition.x, _initialDoorPosition.y, _initialDoorPosition.z) + _doorOpenVector;
        _animatorTimerClockImg.SetBool("TimerScaleAnim", true);
        _animatorTimerClockText.SetBool("TimerTextAnim", true);
        additionalTimeText.text = "";
    }

    private void CloseDoor()
    {
        _animatorTimerClockImg.SetBool("TimerScaleAnim", false);
        _animatorTimerClockText.SetBool("TimerTextAnim", false);
        isOpen = false;
        transform.position = _initialDoorPosition;
    }
}
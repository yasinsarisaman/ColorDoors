using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Doors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingTime;
    [SerializeField] private float levelTime;
    
    private float _remainingTime;
    private int _seconds;
    private int _minutes;
    private int _timerAnimationCounter = 0;
    private bool _isThereTimeFreeze = false;
    private float _freezeTime;
    private static List<int> _greenDoorIdList;
    private static List<int> _purpleDoorIdList;

    private void OnEnable()
    {
        EventBus<GreenDoorStatusChangedEvent>.AddListener(OnAnyGreenDoorOpened);
        EventBus<PurpleDoorStatusChangedEvent>.AddListener(OnAnyPurpleDoorOpened);
    }

    private void OnDisable()
    {
        EventBus<GreenDoorStatusChangedEvent>.RemoveListener(OnAnyGreenDoorOpened);
        EventBus<PurpleDoorStatusChangedEvent>.RemoveListener(OnAnyPurpleDoorOpened);
    }
    
    private void Start()
    {
        _greenDoorIdList = new List<int>();
        _purpleDoorIdList = new List<int>();
        _remainingTime = levelTime;
        _seconds = (int)_remainingTime % 60;
        _minutes = (int)_remainingTime / 60;

        UpdateUITimer();
    }

    private void Update()
    {
        if ((int)_remainingTime == 0)
        {
            EventBus<LevelCompletedEvent>.Emit(this, new LevelCompletedEvent(CompletionStates.CompletionState_LOSE_TIMEOUT));
        }
        else
        {
            if (CheckTimeFreeze()) return;
            
            EvaluateTime();
            UpdateUITimer();
        }
    }

    private void UpdateUITimer()
    {
        remainingTime.text = _minutes + ":" + _seconds.ToString("00");
        if (_seconds <= 10 && _minutes < 1)
        {
            remainingTime.color = Color.red;
            if (_timerAnimationCounter < 100)
            { 
                remainingTime.fontSize = remainingTime.fontSize * 1.001f;
                _timerAnimationCounter++;
            }
            else if (_timerAnimationCounter == 200)
            {
                _timerAnimationCounter = 0;
            }
            else
            {
                remainingTime.fontSize = remainingTime.fontSize * 0.999f;
                _timerAnimationCounter++;
            }
        }
    }

    private void EvaluateTime()
    {
        _remainingTime -= Time.deltaTime;
        _minutes = (int)_remainingTime / 60; 
        _seconds = (int)_remainingTime % 60;
    }

    private void OnAnyGreenDoorOpened(object sender, GreenDoorStatusChangedEvent greenDoorStatusChangedEvent)
    {
        if (_greenDoorIdList.Contains(greenDoorStatusChangedEvent.DoorId)) return;
        _remainingTime += greenDoorStatusChangedEvent.AdditionalTime;
        ChangeTimerColor(Color.white);
        _greenDoorIdList.Add(greenDoorStatusChangedEvent.DoorId);
    }
    
    private void OnAnyPurpleDoorOpened(object sender, PurpleDoorStatusChangedEvent purpleDoorStatusChangedEvent)
    {
        if (_purpleDoorIdList.Contains(purpleDoorStatusChangedEvent.DoorId)) return;
        FreezeTime(purpleDoorStatusChangedEvent.FreezeTime);
        ChangeTimerColor(Color.cyan);
        _purpleDoorIdList.Add(purpleDoorStatusChangedEvent.DoorId);
    }

    private bool CheckTimeFreeze()
    {
        if (_isThereTimeFreeze)
        {
            if (_freezeTime > 0)
            {
                _freezeTime -= Time.deltaTime;
            }
            return true;
        }

        if (!_isThereTimeFreeze || !(_freezeTime <= 0)) return false;
        
        _freezeTime = 0.0f;
        UnfreezeTime();
        ChangeTimerColor(Color.white);
        return false;
    }

    public void FreezeTime()
    {
        _isThereTimeFreeze = true;
    }

    private void FreezeTime(float freezeTime)
    {
        _isThereTimeFreeze = true;
        _freezeTime = freezeTime;
    }

    public void UnfreezeTime()
    {
        _isThereTimeFreeze = false;
    }

    private void ChangeTimerColor(Color color)
    {
        remainingTime.color = color;
    }
}
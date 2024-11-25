using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
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
    private int _additionalTimerColorCounter;
    private bool _isThereTimeFreeze = false;
    private bool _isThereTimeFreezeForSeconds = false;
    private bool _isFirstInputReceived = false;
    private float _freezeTime;
    private static List<int> _greenDoorIdList;
    private static List<int> _purpleDoorIdList;

    private void OnEnable()
    {
        EventBus<FirstInputReceivedEvent>.AddListener(OnFirstInputReceived);
        EventBus<GreenDoorStatusChangedEvent>.AddListener(OnAnyGreenDoorOpened);
        EventBus<PurpleDoorStatusChangedEvent>.AddListener(OnAnyPurpleDoorOpened);
        EventBus<LevelCompletedEvent>.AddListener(OnLevelCompleted);
    }

    private void OnDisable()
    {
        EventBus<FirstInputReceivedEvent>.RemoveListener(OnFirstInputReceived);
        EventBus<GreenDoorStatusChangedEvent>.RemoveListener(OnAnyGreenDoorOpened);
        EventBus<PurpleDoorStatusChangedEvent>.RemoveListener(OnAnyPurpleDoorOpened);
        EventBus<LevelCompletedEvent>.RemoveListener(OnLevelCompleted);
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
        if (!_isFirstInputReceived) { return; }
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
        ArrangeTimerColor();
        if (_seconds <= 5 && _minutes < 1)
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
        _additionalTimerColorCounter = 120;
        _greenDoorIdList.Add(greenDoorStatusChangedEvent.DoorId);
    }
    
    private void OnAnyPurpleDoorOpened(object sender, PurpleDoorStatusChangedEvent purpleDoorStatusChangedEvent)
    {
        if (_purpleDoorIdList.Contains(purpleDoorStatusChangedEvent.DoorId)) return;
        FreezeTime(purpleDoorStatusChangedEvent.FreezeTime);
        ChangeTimerColor(Color.cyan);
        _purpleDoorIdList.Add(purpleDoorStatusChangedEvent.DoorId);
    }
    
    private void OnFirstInputReceived(object sender, FirstInputReceivedEvent firstInput)
    {
        _isFirstInputReceived = true;
    }

    private void OnLevelCompleted(object sender, LevelCompletedEvent levelCompletedEvent)
    {
        FreezeTime();
    }

    private bool CheckTimeFreeze()
    {
        if (_isThereTimeFreeze) return true;

        switch (_isThereTimeFreezeForSeconds)
        {
            case true when _freezeTime > 0.0f:
                _freezeTime -= Time.deltaTime;
                return true;
            case true when _freezeTime <= 0.0f:
                _freezeTime = 0.0f;
                UnfreezeSpecificTimer();
                ChangeTimerColor(Color.white);
                return false;
        }

        return false;
    }

    public void FreezeTime()
    {
        _isThereTimeFreeze = true;
    }

    private void FreezeTime(float freezeTime)
    {
        _isThereTimeFreezeForSeconds = true;
        _freezeTime = freezeTime;
    }

    public void UnfreezeTime()
    {
        _isThereTimeFreeze = false;
    }
    
    public void UnfreezeSpecificTimer()
    {
        _isThereTimeFreezeForSeconds = false;
    }

    private void ChangeTimerColor(Color color)
    {
        remainingTime.color = color;
    }

    private void ArrangeTimerColor()
    {
        if (_additionalTimerColorCounter <= 0)
        {
            ChangeTimerColor(Color.white);
            _additionalTimerColorCounter = 0;
        }
        else
        {
            ChangeTimerColor(Color.green);
            _additionalTimerColorCounter--;
        }
    }

}
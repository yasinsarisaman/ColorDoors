using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
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
    private static List<int> _doorIdList;

    private void OnEnable()
    {
        EventBus<GreenDoorStatusChangedEvent>.AddListener(OnAnyGreenDoorOpened);
    }

    private void OnDisable()
    {
        EventBus<GreenDoorStatusChangedEvent>.RemoveListener(OnAnyGreenDoorOpened);
    }
    
    private void Start()
    {
        _doorIdList = new List<int>();
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
        if (_doorIdList.Contains(greenDoorStatusChangedEvent.DoorId)) return;
        _remainingTime += greenDoorStatusChangedEvent.AdditionalTime;
        remainingTime.color = Color.white;
        _doorIdList.Add(greenDoorStatusChangedEvent.DoorId);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMazePieces : MonoBehaviour
{
    private Vector3 _openedPosition;
    private Vector3 _closedPosition;
    private float _timer;
    private float _timeElapsedToOpen;
    private float _timeElapsedToClose;
    private float _lerpDuration;
    private bool _isOpen;
    private bool _shouldBeOpened;
    
    private void Start()
    {
        _timer = 0.0f;
        _timeElapsedToOpen = 0.0f;
        _timeElapsedToClose = 0.0f;
        _lerpDuration = 2.0f;
        _isOpen = false;
        _shouldBeOpened = false;
        _closedPosition = transform.position;
        _openedPosition = new Vector3(_closedPosition.x, _closedPosition.y - .8f, _closedPosition.z);
    }

    private void Update()
    {
        if (_isOpen && _timer >= 0.0f)
        {
            _timer -= Time.deltaTime;
        }
        
        if (_timer <= 0.0f)
        {
            _shouldBeOpened = false;
        }
        
        if (_shouldBeOpened && !_isOpen)
        {
            OpenMazeWall();
        }

        if (!_shouldBeOpened && _isOpen)
        {
            CloseMazeWall();
        }
    }

    private void OnEnable()
    {
        EventBus<OpenDynamicMazeWalls>.AddListener(OnOpenEvent);
    }

    private void OnDisable()
    {
        EventBus<OpenDynamicMazeWalls>.RemoveListener(OnOpenEvent);
    }

    private void OnOpenEvent(object sender, OpenDynamicMazeWalls openDynamicMazeWalls)
    {
        _shouldBeOpened = true;
        _isOpen = false;
        _timer = openDynamicMazeWalls.TimeToOpen;
    }

    private void OpenMazeWall()
    {
        if (_timeElapsedToOpen < _lerpDuration)
        {
            float t = _timeElapsedToOpen / _lerpDuration;
            
            transform.position = Vector3.Lerp(_closedPosition, _openedPosition, t);
            
            _timeElapsedToOpen += Time.deltaTime;
        }
        else
        {
            _isOpen = true;
            _timeElapsedToOpen = 0.0f;
        }
    }
    private void CloseMazeWall()
    {
        if (_timeElapsedToClose < _lerpDuration)
        {
            float t = _timeElapsedToClose / _lerpDuration;
            
            transform.position = Vector3.Lerp(_openedPosition, _closedPosition, t);
            
            _timeElapsedToClose += Time.deltaTime;
        }
        else
        {
            _isOpen = false;
            _timeElapsedToClose = 0.0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoorArrow : MonoBehaviour
{
    private Vector3 _initialPos;
    private Vector3 _newPos;
    private Vector3 _newRot;
    private float _maxYPos, _minYPos , _animSpeed;
    private short _animDirection = 1;
    private void Start()
    {
        _initialPos = transform.position;
        _newPos = new Vector3(_initialPos.x,_initialPos.y,_initialPos.z);
        _newRot = transform.rotation.eulerAngles;
        _animSpeed = .5f;
        _minYPos = _initialPos.y - _initialPos.y / 10;
        _maxYPos = _initialPos.y + _initialPos.y / 10;
    }

    void Update()
    {
        if (transform.position.y > _maxYPos)
        {
            _animDirection = -1;
        }
        
        if (transform.position.y < _minYPos)
        {
            _animDirection = 1;
        }
        
        _newPos.y += (_animSpeed * Time.deltaTime) * _animDirection;
        
        transform.position = _newPos; 
        // transform.Rotate(new Vector3(0,1,0), .03f);
    }
}

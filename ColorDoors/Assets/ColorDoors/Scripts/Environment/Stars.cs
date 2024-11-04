using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private float _starsSpeed;
    private float _normalizedStarSpeed;
    private Vector3 _rotateVec;

    private void Start()
    {
        _rotateVec = new Vector3(0, _starsSpeed, 0);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(_rotateVec*Time.deltaTime);
    }
}

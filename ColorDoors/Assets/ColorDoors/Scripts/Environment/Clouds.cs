using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private float _cloudsSpeed;
    [SerializeField] private float _zMovementMax;
    [SerializeField] private float _zMovementMin;
    [SerializeField] private float _xMovementMax;
    [SerializeField] private float _xMovementMin;

    private Vector3 _initialPosition;
    private Vector3 _previousPosition;
    private short _xFactor = 1, _zFactor = 1;
    
    void Start()
    {
        _initialPosition = this.transform.position;
        _previousPosition = _initialPosition;
    }

    
    void Update()
    {
        CalculatePositionFactors();
        
        Vector3 movement = new Vector3(_cloudsSpeed * _xFactor, 0, _cloudsSpeed * _zFactor) * Time.deltaTime;
        transform.SetPositionAndRotation(_previousPosition + movement, transform.rotation); 
        _previousPosition = transform.position;
    }

    private void CalculatePositionFactors()
    {
        if (transform.position.x >= _xMovementMax)
        {
            _xFactor = -1;
        }
        else if (transform.position.x <= _xMovementMin)
        {
            _xFactor = 1;
        }
        
        if (transform.position.z >= _zMovementMax)
        {
            _zFactor = -1;
        }
        else if (transform.position.z <= _zMovementMin)
        {
            _zFactor = 1;
        }
    }
}

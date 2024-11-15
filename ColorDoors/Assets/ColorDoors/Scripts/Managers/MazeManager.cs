using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private Material _mazeSharedMaterial;
    [SerializeField] private Material _mazeDynamicSharedMaterial;
    private Color _dynamicMazeColorStart;
    private Color _dynamicMazeColorEnd;
    private float _lerpDuration = 1f; 
    private float _timeElapsed = 0f;
    
    void Start()
    {
        _mazeSharedMaterial.SetColor("_Color", new Color(_mazeSharedMaterial.color.r,_mazeSharedMaterial.color.g,_mazeSharedMaterial.color.b,.5f));
        _dynamicMazeColorStart = new Color(_mazeDynamicSharedMaterial.color.r, _mazeDynamicSharedMaterial.color.g,
            _mazeDynamicSharedMaterial.color.b, .1f);
        _dynamicMazeColorEnd = new Color(_mazeDynamicSharedMaterial.color.r, _mazeDynamicSharedMaterial.color.g,
            _mazeDynamicSharedMaterial.color.b, .9f);
    }

    private void Update()
    {
        if (_timeElapsed < _lerpDuration)
        {
            float t = _timeElapsed / _lerpDuration;
            _mazeDynamicSharedMaterial.SetColor("_Color",Color.Lerp(_dynamicMazeColorStart, _dynamicMazeColorEnd, t));
            _timeElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed = 0.0f;
        }
    }

    private void OnEnable()
    {
        EventBus<MazeCollisionEvent>.AddListener(OnMazeCollision);
    }

    private void OnDisable()
    {
        EventBus<MazeCollisionEvent>.RemoveListener(OnMazeCollision);
    }


    private void OnMazeCollision(object sender, MazeCollisionEvent mazeCollisionEvent)
    {
        Color newMazeColor = mazeCollisionEvent.Collision
            ? new Color(_mazeSharedMaterial.color.r, _mazeSharedMaterial.color.g, _mazeSharedMaterial.color.b, .8f)
            : new Color(_mazeSharedMaterial.color.r, _mazeSharedMaterial.color.g, _mazeSharedMaterial.color.b,
                .5f);
        
        _mazeSharedMaterial.SetColor("_Color", newMazeColor);
    }

}

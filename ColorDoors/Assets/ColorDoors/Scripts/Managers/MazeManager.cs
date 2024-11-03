using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private Material _mazeSharedMaterial;
    
    void Start()
    {
        _mazeSharedMaterial.SetColor("_Color", new Color(_mazeSharedMaterial.color.r,_mazeSharedMaterial.color.g,_mazeSharedMaterial.color.b,.5f));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OpenDynamicMazeWalls 
{
    public OpenDynamicMazeWalls(float timeToOpen)
    {
        TimeToOpen = timeToOpen;
    }

    public float TimeToOpen { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MazeCollisionEvent
{
    public MazeCollisionEvent(bool collision)
    {
        Collision = collision;
    }

    public bool Collision { get; set; }
}
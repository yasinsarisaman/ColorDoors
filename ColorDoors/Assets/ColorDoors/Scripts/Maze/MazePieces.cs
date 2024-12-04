using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePieces : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        /* Collision with a maze piece */
        if (other.gameObject.CompareTag("Player"))
        {
            EventBus<MazeCollisionEvent>.Emit(this, new MazeCollisionEvent(true));
        }
    }

    private void OnCollisionExit(Collision other)
    {

        /* Collision exit with a maze piece */
        if (other.gameObject.CompareTag("Player"))
        {
            EventBus<MazeCollisionEvent>.Emit(this, new MazeCollisionEvent(false));
        }
    }
}

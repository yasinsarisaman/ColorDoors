using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompletionStates
{
    CompletionState_WIN,
    CompletionState_LOSE_TIMEOUT
}
public struct LevelCompletedEvent
{

    public LevelCompletedEvent(CompletionStates state)
    {
        CompletionState = state;
    }

    public CompletionStates CompletionState;
}

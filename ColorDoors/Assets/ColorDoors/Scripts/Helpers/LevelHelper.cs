using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using UnityEngine;

public class LevelHelper : MonoBehaviour
{
    public static void GoToLevel(int x)
    {
        EventBus<ChangeLevelEvent>.Emit(null, new ChangeLevelEvent(x));
    }
}

using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using UnityEngine;

public class LevelHelper : MonoBehaviour
{
    public static void GoToLevel(int levelChangeChoice)
    {
        LevelChange levelChange = LevelChange.levelChange_RestartLevel;
        switch (levelChangeChoice)
        {
            case 0:
                levelChange = LevelChange.levelChange_RestartLevel;
                break;
            case 1:
                levelChange = LevelChange.levelChange_GoNextLevel;
                break;
            case 2:
                levelChange = LevelChange.levelChange_GoBackToMainMenu;
                break;
        }
        EventBus<ChangeLevelEvent>.Emit(null, new ChangeLevelEvent(levelChange));
    }
}

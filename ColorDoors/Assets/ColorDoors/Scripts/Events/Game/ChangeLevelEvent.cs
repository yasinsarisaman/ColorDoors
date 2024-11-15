using UnityEngine;

namespace ColorDoors.Scripts.Events
{
    public enum LevelChange
    {
        levelChange_GoNextLevel,
        levelChange_RestartLevel,
        levelChange_GoBackToMainMenu
    }

    public struct ChangeLevelEvent
    {
        public ChangeLevelEvent(LevelChange levelChange)
        {
            LevelChange = levelChange;
        }
        public LevelChange LevelChange;
    }
}
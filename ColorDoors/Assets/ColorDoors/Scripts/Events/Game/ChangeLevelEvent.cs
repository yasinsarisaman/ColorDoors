using UnityEngine;

namespace ColorDoors.Scripts.Events
{
    public struct ChangeLevelEvent
    {
        public ChangeLevelEvent(int levelToLoad)
        {
            LevelToLoad = levelToLoad;
        }
        public int LevelToLoad { get; set; }
    }
}
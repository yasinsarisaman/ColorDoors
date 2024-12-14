using UnityEngine;

namespace ColorDoors.Scripts.Events.Game
{
    public struct LevelCompletedWithTime
    {
        public LevelCompletedWithTime(float remainingTime)
        {
            RemainingTime = remainingTime;
        }

        public float RemainingTime { get; set; }
    }
    
}
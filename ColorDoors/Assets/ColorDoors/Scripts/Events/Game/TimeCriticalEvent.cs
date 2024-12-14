using UnityEngine;

namespace ColorDoors.Scripts.Events.Game
{
    public struct TimeCriticalEvent 
    {
        public TimeCriticalEvent(bool isTimeCritical)
        {
            IsTimeCritical = isTimeCritical;
        }

        public bool IsTimeCritical { get; set; }
    }
}
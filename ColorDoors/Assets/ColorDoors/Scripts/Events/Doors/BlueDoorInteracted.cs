using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct BlueDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public BlueDoorStatusChangedEvent(int doorId, bool onlyExit, bool onlyEntrance)
        {
            DoorId = doorId;
            OnlyExit = onlyExit;
            OnlyEntrance = onlyEntrance;
        }

        public int DoorId { get; set; }
        public bool OnlyExit { get; set; }
        public bool OnlyEntrance { get; set; }
    }
}
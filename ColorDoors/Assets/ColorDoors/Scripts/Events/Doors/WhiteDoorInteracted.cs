using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct WhiteDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public WhiteDoorStatusChangedEvent(int doorId)
        {
            DoorId = doorId;
        }

        public int DoorId { get; set; }
    }
}
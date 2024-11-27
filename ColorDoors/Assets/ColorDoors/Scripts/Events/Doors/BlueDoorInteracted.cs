using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct BlueDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public BlueDoorStatusChangedEvent(int doorId)
        {
            DoorId = doorId;
        }

        public int DoorId { get; set; }
    }
}
using UnityEngine;

namespace ColorDoors.Scripts.Events
{
    public struct BlueDoorStatusChangedEvent
    {
        public BlueDoorStatusChangedEvent(int doorId)
        {
            DoorId = doorId;
        }

        public int DoorId { get; set; }
    }
}
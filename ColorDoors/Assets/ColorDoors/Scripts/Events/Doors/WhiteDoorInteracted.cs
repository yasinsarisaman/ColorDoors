using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct WhiteDoorStatusChangedEvent
    {
        public WhiteDoorStatusChangedEvent(int doorId)
        {
            DoorId = doorId;
        }

        public int DoorId { get; set; }
    }
}
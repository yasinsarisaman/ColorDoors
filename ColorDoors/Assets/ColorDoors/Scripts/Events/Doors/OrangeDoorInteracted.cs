using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct OrangeDoorStatusChangedEvent
    {
        public OrangeDoorStatusChangedEvent(int doorId)
        {
            DoorId = doorId;
        }

        public int DoorId { get; set; }
    }
}
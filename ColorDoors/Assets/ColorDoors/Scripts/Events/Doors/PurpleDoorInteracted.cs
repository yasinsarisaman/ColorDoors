using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct PurpleDoorStatusChangedEvent
    {
        public PurpleDoorStatusChangedEvent(int doorId, float freezeTime ,bool shouldBeOpened)
        {
            DoorId = doorId;
            FreezeTime = freezeTime;
            ShouldBeOpened = shouldBeOpened;
        }

        public int DoorId { get; set; }
        public float FreezeTime { get; set; }
        public bool ShouldBeOpened { get; set; }
    }
}
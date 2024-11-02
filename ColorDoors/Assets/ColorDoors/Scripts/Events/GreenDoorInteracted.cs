using UnityEngine;

namespace ColorDoors.Scripts.Events
{
    public struct GreenDoorStatusChangedEvent
    {
        public GreenDoorStatusChangedEvent(int doorId, float additionalTime ,bool shouldBeOpened)
        {
            DoorId = doorId;
            AdditionalTime = additionalTime;
            ShouldBeOpened = shouldBeOpened;
        }

        public int DoorId { get; set; }
        public float AdditionalTime { get; set; }
        public bool ShouldBeOpened { get; set; }
    }
}
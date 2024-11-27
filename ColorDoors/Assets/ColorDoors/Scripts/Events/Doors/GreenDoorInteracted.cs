using UnityEngine;

namespace ColorDoors.Scripts.Events.Doors
{
    public struct GreenDoorStatusChangedEvent : IDoorStatusChangedEvent
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
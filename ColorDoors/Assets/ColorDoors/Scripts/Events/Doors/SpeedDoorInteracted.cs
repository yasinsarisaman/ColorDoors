namespace ColorDoors.Scripts.Events.Doors
{
    public struct SpeedDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public SpeedDoorStatusChangedEvent(int doorId, float boostFactor)
        {
            DoorId = doorId;
            BoostFactor = boostFactor;
        }

        public int DoorId { get; set; }
        public float BoostFactor { get; set; }
    }
}
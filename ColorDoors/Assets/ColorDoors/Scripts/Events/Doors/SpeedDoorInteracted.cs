namespace ColorDoors.Scripts.Events.Doors
{
    public struct SpeedDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public SpeedDoorStatusChangedEvent(int doorId, float boostFactor, float boostTime)
        {
            DoorId = doorId;
            BoostFactor = boostFactor;
            BoostTime = boostTime;
        }

        public int DoorId { get; set; }
        public float BoostFactor { get; set; }
        public float BoostTime { get; set; }
    }
}
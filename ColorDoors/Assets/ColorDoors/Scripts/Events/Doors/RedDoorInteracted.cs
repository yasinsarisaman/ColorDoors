namespace ColorDoors.Scripts.Events.Doors
{
    public struct RedDoorStatusChangedEvent : IDoorStatusChangedEvent
    {
        public RedDoorStatusChangedEvent(int doorId, bool isOpened)
        {
            DoorId = doorId;
            IsOpened = isOpened;
        }

        public int DoorId { get; set; }
        public bool IsOpened { get; set; }
    }
}
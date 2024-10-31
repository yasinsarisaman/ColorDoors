namespace ColorDoors.Scripts.Events
{
    public struct RedDoorStatusChangedEvent
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
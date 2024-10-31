using UnityEngine;

namespace ColorDoors.Scripts.Events.Player
{
    public struct TeleportPlayer
    {
        public TeleportPlayer(Transform transformToTeleport, float doorOffset)
        {
            TransformToTeleport = transformToTeleport;
            DoorOffset = doorOffset;
        }

        public Transform TransformToTeleport { get; set; }
        public float DoorOffset { get; set; }
    }
}
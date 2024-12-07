using UnityEngine;

namespace ColorDoors.Scripts.Events.Player
{
    public struct BoostPlayerSpeed
    {
        public BoostPlayerSpeed(float boostFactor)
        {
            BoostFactor = boostFactor;
        }

        public float BoostFactor { get; set; }
    }
}
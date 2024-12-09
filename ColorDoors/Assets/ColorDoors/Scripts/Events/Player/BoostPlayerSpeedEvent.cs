using UnityEngine;

namespace ColorDoors.Scripts.Events.Player
{
    public struct BoostPlayerSpeed
    {
        public BoostPlayerSpeed(float boostFactor, float boostTime)
        {
            BoostFactor = boostFactor;
            BoostTime = boostTime;
        }

        public float BoostFactor { get; set; }
        public float BoostTime { get; set; }
    }
}
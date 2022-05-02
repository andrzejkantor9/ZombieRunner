using UnityEngine;

namespace ZombieRunner.World
{
    public class PickupBattery : Pickup
    {
        [Header("PROPERTIES")]
        [SerializeField]
        private float _restoreByAngle = 20f;
        [SerializeField]
        private float _restoreByIntensity = 30f;

        private Player.FlashLightSystem _flashLightSystem;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Awake() 
        {
            _flashLightSystem = FindObjectOfType<Player.FlashLightSystem>(); 
        }

        protected override void OnPickup()
        {
            base.OnPickup();

            _flashLightSystem.RestoreLightIntensity(_restoreByIntensity);
            _flashLightSystem.RestoreLightAngle(_restoreByAngle);
        }
    }
}

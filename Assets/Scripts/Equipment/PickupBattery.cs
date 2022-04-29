using UnityEngine;

namespace Equipment
{
    public class PickupBattery : Pickup
    {
        [Header("PROPERTIES")]
        [SerializeField]
        private float _restoreByAngle = 20f;
        [SerializeField]
        private float _restoreByIntensity = 30f;

        private FlashLightSystem _flashLightSystem;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Awake() 
        {
            _flashLightSystem = FindObjectOfType<FlashLightSystem>(); 
        }

        private void OnEnable() 
        {
            _pickupEvent.AddListener(Pickup);            
        }

        private void OnDisable() 
        {
            _pickupEvent.RemoveAllListeners();            
        }

        private void Pickup()
        {
            _flashLightSystem.RestoreLightIntensity(_restoreByIntensity);
            _flashLightSystem.RestoreLightAngle(_restoreByAngle);
        }
    }
}

using UnityEngine;

namespace Equipment
{
    public class PickupAmmo : Pickup
    {
        [Header("PROPERTIES")]
        [SerializeField]
        int _ammoIncreaseAmount = 5;
        [SerializeField]
        AmmoType _ammoType;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Awake() 
        {
            _pickupEvent.AddListener(Pickup);
        }

        private void OnDestroy() 
        {
            _pickupEvent.RemoveAllListeners();            
        }

        private void Pickup()
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(_ammoType, _ammoIncreaseAmount);
        }
    }
}

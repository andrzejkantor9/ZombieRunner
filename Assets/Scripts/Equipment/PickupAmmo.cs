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

        protected override void OnPickup()
        {
            base.OnPickup();
            
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(_ammoType, _ammoIncreaseAmount);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(StarterAssets.FirstPersonController))]
public class Ammo : MonoBehaviour
{

#region PROPERTIES

    [Header("PROPERTIES")]
    [SerializeField]
    private AmmoSlot[] _ammoSlots;

#endregion

#region CACHE

    // [Space(10f)][Header("CACHE")]

#endregion

#region STATES

    [System.Serializable]
    private class AmmoSlot
    {
        public Weapons.AmmoType _ammoType;
        public int _ammoAmount;

        public int ReduceAmmo(int amount = 1)
        {
            _ammoAmount -= amount;
            return _ammoAmount;
        }

        public int IncreaseAmmo(int amount)
        {
            _ammoAmount += amount;
            return _ammoAmount;
        }
    }

#endregion

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnValidate() 
    {
        SetupCache();    
    }

    private void Awake() 
    {
        AssertCache();        
    }

    private void OnEnable() 
    {
        BindDelegates();
    }

    private void OnDisable() 
    {
        UnbindDelegates();
    }

#region Setup

    private void SetupCache()
    {
    }

    private void AssertCache()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        // UnityEngine.Assertions.Assert.IsNotNull(_cinemachineCamera, $"Script: {GetType().ToString()} variable _cinemachineCamera is null");
#endif
    }

    private void BindDelegates()
    {
    }

    private void UnbindDelegates()
    {
    }

#endregion

#region AmmoManagement

    public int GetCurrentAmmo(Weapons.AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType)._ammoAmount;
    }

    public void ReduceCurrentAmmo(Weapons.AmmoType ammoType)
    {
        int newAmmoAmount = GetAmmoSlot(ammoType).ReduceAmmo();
        // CustomDebug.Log($"{ammoType.ToString()} ammo amount: {newAmmoAmount.ToString()}");
    }

    public void IncreaseCurrentAmmo(Weapons.AmmoType ammoType, int increaseAmount)
    {
        int newAmmoAmount = GetAmmoSlot(ammoType).IncreaseAmmo(increaseAmount);
    }

    private AmmoSlot GetAmmoSlot(Weapons.AmmoType ammoType)
    {
        foreach(AmmoSlot slot in _ammoSlots)
        {
            if(slot._ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }

#endregion
}

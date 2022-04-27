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

    [Space(10f)][Header("CACHE")]
    [SerializeField]
    private TMPro.TextMeshProUGUI _ammoAmountUI;

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

    private void Start()
    {
        StartSetup();
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
        UnityEngine.Assertions.Assert.IsNotNull(_ammoAmountUI, $"Script: {GetType().ToString()} variable _ammoAmountUI is null");
#endif
    }

    private void StartSetup()
    {
        SetAmmoUIText();
    }

    private void BindDelegates()
    {
    }

    private void UnbindDelegates()
    {
    }

#endregion

#region AmmoManagement

    public void SetAmmoUIText(Weapons.AmmoType ammoType)
    {
        //so proper weapon gets enabled before we check for weapon type
        StartCoroutine(SetAmmoUITextDelayed());
    }

    IEnumerator SetAmmoUITextDelayed()
    {
        yield return new WaitForEndOfFrame();

        Weapons.Weapon[] currentActiveWeapons = FindObjectsOfType<Weapons.Weapon>();
        if(Weapons.Weapon._currentAmmoType == currentActiveWeapons[0]._ammoTypeGetter)
        {            
            _ammoAmountUI.text = GetAmmoSlot(currentActiveWeapons[0]._ammoTypeGetter)._ammoAmount.ToString();
        }
    }

    private string SetAmmoUIText()
    {
        Weapons.Weapon[] currentActiveWeapons = FindObjectsOfType<Weapons.Weapon>();
        _ammoAmountUI.text = GetAmmoSlot(currentActiveWeapons[0]._ammoTypeGetter)._ammoAmount.ToString();

        return _ammoAmountUI.text;
    }

    public int GetCurrentAmmo(Weapons.AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType)._ammoAmount;
    }

    public void ReduceCurrentAmmo(Weapons.AmmoType ammoType)
    {
        int newAmmoAmount = GetAmmoSlot(ammoType).ReduceAmmo();
        CustomDebug.Log($"{ammoType.ToString()} ammo amount: {newAmmoAmount.ToString()}");

        SetAmmoUIText(ammoType);
    }

    public void IncreaseCurrentAmmo(Weapons.AmmoType ammoType, int increaseAmount)
    {
        int newAmmoAmount = GetAmmoSlot(ammoType).IncreaseAmmo(increaseAmount);
        CustomDebug.Log($"{ammoType.ToString()} ammo amount: {newAmmoAmount.ToString()}");

        SetAmmoUIText(ammoType);
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

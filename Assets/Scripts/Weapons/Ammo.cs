using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieRunner.Weapons
{
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
            public ZombieRunner.Weapons.AmmoType _ammoType;
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

        private void Awake() 
        {
            AssertCache();        
        }

        private void Start()
        {
            StartSetup();
        }

    #region Setup

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

    #endregion

    #region AmmoManagement

        public int GetCurrentAmmo(ZombieRunner.Weapons.AmmoType ammoType)
        {
            return GetAmmoSlot(ammoType)._ammoAmount;
        }

        public void ReduceCurrentAmmo(ZombieRunner.Weapons.AmmoType ammoType)
        {
            int newAmmoAmount = GetAmmoSlot(ammoType).ReduceAmmo();
            MyDebug.Debug.Log($"{ammoType.ToString()} ammo amount: {newAmmoAmount.ToString()}");

            SetAmmoUIText(ammoType);
        }

        public void IncreaseCurrentAmmo(ZombieRunner.Weapons.AmmoType ammoType, int increaseAmount)
        {
            int newAmmoAmount = GetAmmoSlot(ammoType).IncreaseAmmo(increaseAmount);
            MyDebug.Debug.Log($"{ammoType.ToString()} ammo amount: {newAmmoAmount.ToString()}");

            SetAmmoUIText(ammoType);
        }

        private AmmoSlot GetAmmoSlot(ZombieRunner.Weapons.AmmoType ammoType)
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

    #region UI

        public void SetAmmoUIText(ZombieRunner.Weapons.AmmoType ammoType)
        {
            //so proper weapon gets enabled before we check for weapon type
            StartCoroutine(SetAmmoUITextDelayed());
        }

        IEnumerator SetAmmoUITextDelayed()
        {
            yield return new WaitForEndOfFrame();

            ZombieRunner.Weapons.Weapon[] currentActiveWeapons = FindObjectsOfType<ZombieRunner.Weapons.Weapon>();
            if(ZombieRunner.Weapons.Weapon._currentAmmoType == currentActiveWeapons[0]._ammoTypeGetter)
            {            
                _ammoAmountUI.text = GetAmmoSlot(currentActiveWeapons[0]._ammoTypeGetter)._ammoAmount.ToString();
            }
        }

        private string SetAmmoUIText()
        {
            ZombieRunner.Weapons.Weapon[] currentActiveWeapons = FindObjectsOfType<ZombieRunner.Weapons.Weapon>();
            _ammoAmountUI.text = GetAmmoSlot(currentActiveWeapons[0]._ammoTypeGetter)._ammoAmount.ToString();

            return _ammoAmountUI.text;
        }

    #endregion
    }
}

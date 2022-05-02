using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieRunner.Weapons
{
    // [RequireComponent(typeof(StarterAssets.FirstPersonController))]
    public class WeaponSwitcher : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField]
        private int _currentWeapon = 0;

    #endregion

    #region CACHE

        // [Space(10f)][Header("CACHE")]
        private StarterAssetsInputActions _controls;

    #endregion

    #region STATES

        private int _newWeapon;
        private float mouseScrollY;

    #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Awake() 
        {          
            Initialize();    
        }

        private void Start() 
        {
            SetWeaponActive();        
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

        private void Initialize()
        {
            _newWeapon = _currentWeapon;
            _controls = new StarterAssetsInputActions();
        }

        private void BindDelegates()
        {
             StarterAssets.FirstPersonController.OnWeapon0Selected += Weapon0InputChanged;
             StarterAssets.FirstPersonController.OnWeapon1Selected += Weapon1InputChanged;
             StarterAssets.FirstPersonController.OnWeapon2Selected += Weapon2InputChanged;

             _controls.Enable();

            if(_controls != null)
            {
                _controls.Player.SwitchWeapon.performed += ProcessScrolWheelInput;
                // _controls.Player.SwitchWeapon.performed += y => mouseScrollY = y.ReadValue<float>();
            }
        }

        private void UnbindDelegates()
        {
            StarterAssets.FirstPersonController.OnWeapon0Selected -= Weapon0InputChanged;
            StarterAssets.FirstPersonController.OnWeapon1Selected -= Weapon1InputChanged;
            StarterAssets.FirstPersonController.OnWeapon2Selected -= Weapon2InputChanged;

            _controls.Disable();

            if(_controls != null)
            {
                _controls.Player.SwitchWeapon.performed -= ProcessScrolWheelInput;
            }
        }

    #endregion

    #region SwitchingWeapons

    private void Weapon0InputChanged(bool pressed)
    {
        if(pressed)
        {
            _newWeapon = 0;
            SetWeaponActive();
        }
    }

    private void Weapon1InputChanged(bool pressed)
    {
        if(pressed)
        {
            _newWeapon = 1;
            SetWeaponActive();
        }
    }

    private void Weapon2InputChanged(bool pressed)
    {
        if(pressed)
        {
            _newWeapon = 2;
            SetWeaponActive();
        }
    }

    private void ProcessScrolWheelInput(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        float yValue = callbackContext.ReadValue<float>();

        if(yValue > 0)
        {
            _newWeapon = Mathf.Clamp(_newWeapon + 1, 0, transform.childCount-1);
        }
        else if(yValue < 0)
        {
            _newWeapon = Mathf.Clamp(_newWeapon - 1, 0, transform.childCount-1);
        }
        SetWeaponActive();
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weaponTransform in transform)
        {
            if(weaponIndex == _newWeapon)
            {
                weaponTransform.gameObject.SetActive(true);
                _currentWeapon = _newWeapon;

                Weapon weapon = weaponTransform.GetComponent<Weapon>();
                Weapon._currentAmmoType = weapon._ammoTypeGetter;
                FindObjectOfType<Ammo>().SetAmmoUIText(weapon._ammoTypeGetter);
            }
            else
            {
                weaponTransform.gameObject.SetActive(false);
            }
            ++weaponIndex;
        }
    }

    #endregion
    }
}

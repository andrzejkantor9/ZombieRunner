using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    // [RequireComponent(typeof(StarterAssets.FirstPersonController))]
    public class AmmoPickup : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField]
        int _ammoIncreaseAmount = 5;
        [SerializeField]
        AmmoType _ammoType;

    #endregion

    #region CACHE

        // [Space(10f)][Header("CACHE")]

    #endregion

    #region STATES



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

        private void OnTriggerEnter(Collider other)
        {
            PickupAmmo(other);
        }

        void Update() 
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");
#endif


            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.EndSample();
#endif
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

    #region AmmoPickup

        private void PickupAmmo(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<Ammo>().IncreaseCurrentAmmo(_ammoType, _ammoIncreaseAmount);
                Destroy(gameObject);
            }
        }

    #endregion
    }
}

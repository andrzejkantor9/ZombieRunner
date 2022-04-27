using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    // [RequireComponent(typeof(StarterAssets.FirstPersonController))]
    public class BatteryPickup : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField]
        private float _restoreByAngle = 20f;
        [SerializeField]
        private float _restoreByIntensity = 30f;

    #endregion

    #region CACHE

        // [Space(10f)][Header("CACHE")]
          //[SerializeField][HideInInspector]

        private FlashLightSystem _flashLightSystem;

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
            Setup();      
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
            PickupBattery(other);            
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

        private void Setup()
        {
            _flashLightSystem = FindObjectOfType<FlashLightSystem>();
        }

        private void BindDelegates()
        {
        }

        private void UnbindDelegates()
        {
        }

    #endregion

    #region Pickup

        private void PickupBattery(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _flashLightSystem.RestoreLightIntensity(_restoreByIntensity);
                _flashLightSystem.RestoreLightAngle(_restoreByAngle);
                Destroy(gameObject);
            }
        }

    #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // [RequireComponent(typeof(StarterAssets.FirstPersonController))]
    public class FlashLightSystem : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField]
        private float _lightIntensityDecay = 1f;
        [SerializeField]
        private float _angleDecay = 1f;
        [SerializeField]
        private float _minimumAngle = 30f;

    #endregion

    #region CACHE

        // [Space(10f)][Header("CACHE")]
        [SerializeField][HideInInspector]
        private Light _light;

        private const float MAX_LIGHT_ANGLE = 179f;
        private float _startingLightInstensity;
        private float _startingLightOuterAngle;
        private float _startingLightInnerAngle;

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

        void Update() 
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");
#endif

        DecreaseLightAngle();
        DecreaseLightIntensity();
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.EndSample();
#endif
        }

    #region Setup

        private void SetupCache()
        {
            _light = GetComponent<Light>();
        }

        private void AssertCache()
        {
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(_light, $"Script: {GetType().ToString()} variable _myLight is null");
    #endif
        }

        private void Setup()
        {
            _startingLightInstensity = _light.intensity;
            _startingLightOuterAngle = _light.spotAngle;
            _startingLightInnerAngle = _light.innerSpotAngle;
        }

        private void BindDelegates()
        {
        }

        private void UnbindDelegates()
        {
        }

    #endregion

    #region DecayLight

        private void DecreaseLightAngle()
        {
            if(_light.spotAngle <= _minimumAngle) return;
            _light.spotAngle -= _angleDecay * Time.deltaTime;
            _light.innerSpotAngle -= _angleDecay * Time.deltaTime;
        }

        private void DecreaseLightIntensity()
        {
            _light.intensity -= _lightIntensityDecay * Time.deltaTime;
        }

        public void RestoreLightAngle(float restoreByAngle)
        {
            _light.spotAngle += restoreByAngle;
            _light.innerSpotAngle += restoreByAngle;

            _light.spotAngle = Mathf.Clamp(_light.spotAngle, 0f, _startingLightOuterAngle);
            _light.innerSpotAngle = Mathf.Clamp(_light.innerSpotAngle, 0f, _startingLightInnerAngle);
        }

        public void RestoreLightIntensity(float restoreByAmount)
        {
            _light.intensity += restoreByAmount;
            _light.intensity = Mathf.Clamp(_light.intensity, 0f, _startingLightInstensity);
        }

    #endregion
    }
}
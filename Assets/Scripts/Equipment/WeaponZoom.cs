using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    public class WeaponZoom : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField][Range(1f, 179f)]
        float _zoomInFOV = 20f;
        [SerializeField][Range(0f, 1000f)]
        float _zoomInSensitivity = .1f;

        // [SerializeField][Range(1f, 179f)]
        float _zoomOutFOV;
        // [SerializeField][Range(1f, 179f)]
        float _zoomOutSensitivity;

    #endregion

    #region CACHE
        [Space(10f)][Header("CACHE")]
        [SerializeField]
        StarterAssets.FirstPersonController _fpsController;

        [SerializeField][HideInInspector]
        Cinemachine.CinemachineVirtualCamera _cinemachineCamera;

    #endregion

    #region STATES

        bool _isZoomedIn;

    #endregion

        //////////////////////////////////////////////////////////////

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
            ZoomChanged(false);
        }

    #region Setup

        private void SetupCache()
        {
            _cinemachineCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

            // _fpsController = GetComponent<StarterAssets.FirstPersonController>();
        }

        private void AssertCache()
        {
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(_cinemachineCamera, $"Script: {GetType().ToString()} variable _cinemachineCamera is null");

            UnityEngine.Assertions.Assert.IsNotNull(_fpsController, $"Script: {GetType().ToString()} variable _fpsController is null");
    #endif
        }

        private void Setup()
        {
            _zoomOutFOV = _cinemachineCamera.m_Lens.FieldOfView; 
            _zoomOutSensitivity = _fpsController.RotationSpeed;       
        }

        private void BindDelegates()
        {
            StarterAssets.FirstPersonController.OnZoomChanged += ZoomChanged;
        }

        private void UnbindDelegates()
        {
            StarterAssets.FirstPersonController.OnZoomChanged -= ZoomChanged;
        }

    #endregion

    #region Zooming

        private void ZoomChanged(bool pressed)
        {
            _isZoomedIn = pressed;
            _cinemachineCamera.m_Lens.FieldOfView = _isZoomedIn ? _zoomInFOV : _zoomOutFOV;
            _fpsController.RotationSpeed = _isZoomedIn ? _zoomInSensitivity : _zoomOutSensitivity;
        }

    #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace ZombieRunner.UI
{
    [RequireComponent(typeof(Image))]
    public class DisplayDamage : MonoBehaviour
    {

    #region PROPERTIES

        [Header("PROPERTIES")]
        [SerializeField]
        float _impactTime = .3f;

    #endregion

    #region CACHE

        [Space(10f)][Header("CACHE")]
        [SerializeField][HideInInspector]
        Image _impactImage;

    #endregion

    #region STATES

        public string str { get; private set;}

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
            SetValues();
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


            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.EndSample();
#endif
        }

    #region Setup

        private void SetupCache()
        {
            _impactImage = GetComponent<Image>();
        }

        private void AssertCache()
        {
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(_impactImage, $"Script: {GetType().ToString()} variable _impactCanvas is null");
    #endif
        }

        private void Setup()
        {
            
        }

        private void SetValues()
        {
            _impactImage.enabled = false;
        }

        private void BindDelegates()
        {
        }

        private void UnbindDelegates()
        {
        }

    #endregion

    #region DisplayDamage

        public void ShowDamageImpact()
        {
            StartCoroutine(ShowImpact());
        }

        private IEnumerator ShowImpact()
        {
            _impactImage.enabled = true;
            yield return new WaitForSeconds(_impactTime);
            _impactImage.enabled = false;
        }

    #endregion
    }
}
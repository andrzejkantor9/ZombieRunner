using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo introduce delegates here
//todo check nested regions
//todo check inspector atributes

namespace InterProject
{

    [RequireComponent(typeof(BoxCollider))]
    public class ExampleScript : MonoBehaviour
    {
        //CACHE
        [Header("CACHE")]
        [SerializeField][HideInInspector]
        BoxCollider _boxCollider;

        const string FRIENDLY_TAG = "Friendly";
        
        //PROPERTIES
        [Space(10)] [Header("PROPERTIES")]
        [SerializeField] [Range(0,1)] [Tooltip("to display in inspector")]
        float _speed = 1f; 
        
        //STATES
        bool _isDead;

        ///////////////////////////////////////////////
        //only engine methods without regions
        //only methods inside engine methods
        //methods called must be below methods calling them
        //#if DEVELOPMENT_BUILD || UNITY_EDITOR
        //place one or more assembly definition

        private void OnValidate()
        {
            SetupCache();
        }

        private void Awake() 
        {
            AssertCache();
            Initialize();  
        }

        private void Update() 
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
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void AssertCache()
        {
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsNotNull(_boxCollider, $"Script: {GetType().ToString()} variable _boxCollider is null");
    #endif
        }

        /// <summary>
        /// Setup on awake
        /// </summary>
        /// <param name="other">No params here</param>
        private bool Initialize()
        {
            string exampleName;
            return true;
        }
        /// ?return? <>

    #endregion

    #region RegionsForSpecificPartOfFunctionality
    #endregion

    }

}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// namespace InterProject
// {
//     // [RequireComponent(typeof(StarterAssets.FirstPersonController))]
//     public class Ammo : MonoBehaviour
//     {

//     #region PROPERTIES

//         // [Header("PROPERTIES")]

//     #endregion

//     #region CACHE

//         // [Space(10f)][Header("CACHE")]

//     #endregion

//     #region STATES



//     #endregion

//         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//         private void OnValidate() 
//         {
//             SetupCache();    
//         }

//         private void Awake() 
//         {
//             AssertCache();        
//         }

//         private void OnEnable() 
//         {
//             BindDelegates();
//         }

//         private void OnDisable() 
//         {
//             UnbindDelegates();
//         }

//         void Update() 
//         {
// #if DEVELOPMENT_BUILD || UNITY_EDITOR
//         UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");
// #endif


            
// #if DEVELOPMENT_BUILD || UNITY_EDITOR
//         UnityEngine.Profiling.Profiler.EndSample();
// #endif
//         }

//     #region Setup

//         private void SetupCache()
//         {
//         }

//         private void AssertCache()
//         {
//     #if DEVELOPMENT_BUILD || UNITY_EDITOR
//             // UnityEngine.Assertions.Assert.IsNotNull(_cinemachineCamera, $"Script: {GetType().ToString()} variable _cinemachineCamera is null");
//     #endif
//         }

//         private void BindDelegates()
//         {
//         }

//         private void UnbindDelegates()
//         {
//         }

//     #endregion

//     #region Functionality
//     #endregion
//     }
// }
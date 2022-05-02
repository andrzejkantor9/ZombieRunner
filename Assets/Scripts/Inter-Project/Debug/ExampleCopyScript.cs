// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace ProjectName.NamespaceName
// {
//     // [RequireComponent(typeof(BoxCollider))]
//     public class ClassName : MonoBehaviour
//     {

//     #region Properties
//         // [Header("Properties")]
//     #endregion

//     #region Cache
//         // [Space(10f)][Header("Cache")]
//         //[SerializeField][HideInInspector]
//     #endregion

//     #region States
//         //public string str { get; private set; }
//     #endregion

//     #region Events
//     #endregion

//         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//         private void OnValidate() 
//         {
//             SetupCache();    
//         }

//         private void Awake() 
//         {
//             AssertCache(); 
//             Setup();       
//         }

//         private void Start()
//         {
//             StartSetup();
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
//             MyDebug.Debug.BeginSample($"{GetType().ToString()}: Update");
//             MyDebug.Debug.EndSample();
//         }

//     #region Setup

//         private void SetupCache()
//         {
//         }

//         private void AssertCache()
//         {
//     #if DEVELOPMENT_BUILD || UNITY_EDITOR
//             // UnityEngine.Assertions.Assert.IsNotNull(_cinemachineCamera, $"_cinemachineCamera is null, Object: {gameObject.name}, Script: {GetType().ToString()} ");
//     #endif
//         }

//         private void Setup()
//         {   
//         }

//         private void StartSetup()
//         {
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
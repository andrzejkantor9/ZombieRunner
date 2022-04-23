using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo introduce delegates here
//todo check nested regions
//todo check inspector atributes
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
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");

        //update code here

        UnityEngine.Profiling.Profiler.EndSample();
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
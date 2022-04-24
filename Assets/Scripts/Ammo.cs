using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(StarterAssets.FirstPersonController))]
public class Ammo : MonoBehaviour
{

#region PROPERTIES

    [Header("PROPERTIES")]
    [SerializeField]
    private int _ammoAmount = 10;

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

#region AmmoManagement

    public int GetCurrentAmmo()
    {
        return _ammoAmount;
    }

    public void ReduceCurrentAmmo()
    {
        --_ammoAmount;
    }

#endregion
}

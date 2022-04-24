using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make never change position to camera
//TODO fix camera lag, get rid of wobbly camera
//TODO check health commetns
//TODO add weapon cd
public class Weapon : MonoBehaviour
{
#region CACHE
    [Header("CACHE")]
    [SerializeField] 
    Camera m_FPCamera;
    [SerializeField]
    ParticleSystem m_muzzleFlash;
    [SerializeField]
    GameObject m_hitEffect;
    [SerializeField]
    private Ammo _ammoSlot;

    GameObject m_hitEffectInstance;
    int _enemyLayer = 1 << 6;
#endregion

#region PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    float m_weaponDamage = 30f;
    [SerializeField][Range(0f, 1000f)]
    float m_range = 100f;
    [SerializeField][Range(0f, 20f)]
    private float _shootingCooldown = 1f;
#endregion

#region STATES
    private bool _isShootingInput;
    private float _lastShootTime;
#endregion

    ////////////////////////////////////////

    void Awake()
    {
        
        AssertCache();
        
        StarterAssets.FirstPersonController.OnFireChanged += FireInputChanged;
    }

    void Start() 
    {
        m_hitEffectInstance = Instantiate(m_hitEffect);
        // m_hitEffectInstance.SetActive(false);
    }

    void OnDestroy() 
    {
        StarterAssets.FirstPersonController.OnFireChanged -= FireInputChanged;
    }

    void Update() 
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");
#endif
        if(_isShootingInput && Time.time > _shootingCooldown + _lastShootTime)          
            Shoot();
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Profiling.Profiler.EndSample();
#endif
    }

    void FireInputChanged(bool isFiring)
    {
        // CustomDebug.Log("fire: " + isFiring);
        if(isFiring)
        {
            _isShootingInput = true;
        }
        else
        {
            _isShootingInput = false;
        }
    }

    void Shoot()
    {
        if(_ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            _ammoSlot.ReduceCurrentAmmo(); 
            _lastShootTime = Time.time;           
        }
    }

    void PlayMuzzleFlash()
    {
        m_muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit raycastHitInfo;

        if (Physics.Raycast(m_FPCamera.transform.position, m_FPCamera.transform.forward, out raycastHitInfo, m_range, _enemyLayer))
        {
            Health target = raycastHitInfo.transform.GetComponent<Health>();

            CreateHitImpact(raycastHitInfo);
            CustomDebug.Log($"hit target: {raycastHitInfo.transform.gameObject.name}");
            if (target)
            {
                target.TakeDamage(m_weaponDamage);
            }
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        m_hitEffectInstance.transform.position = hit.point;
        m_hitEffectInstance.transform.rotation = Quaternion.LookRotation(hit.normal);

        m_hitEffectInstance.GetComponent<UnityStandardAssets.Effects.ParticleSystemMultiplier>()?.PlayParticle();

    }

    void AssertCache()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Assertions.Assert.IsNotNull(m_FPCamera, $"Script: {GetType().ToString()} variable m_FPCamera is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_muzzleFlash, $"Script: {GetType().ToString()} variable m_muzzleFlash is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_hitEffect, $"Script: {GetType().ToString()} variable m_hitEffect is null");
        UnityEngine.Assertions.Assert.IsNotNull(_ammoSlot, $"Script: {GetType().ToString()} variable _ammoSlot is null");
#endif
    }
}

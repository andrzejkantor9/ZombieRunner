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

    GameObject m_hitEffectInstance;
#endregion

#region PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    float m_weaponDamage = 30f;
    [SerializeField]
    bool m_isOneShotWeapon = true;
    [SerializeField][Range(0f, 1000f)]
    float m_range = 100f;
#endregion

#region STATES
    bool m_isShooting;
#endregion

    ////////////////////////////////////////

    void Awake()
    {
        
        AssertCache();
        
        StarterAssets.FirstPersonController.OnFireChanged += FireChanged;
    }

    void Start() 
    {
        m_hitEffectInstance = Instantiate(m_hitEffect);
        // m_hitEffectInstance.SetActive(false);
    }

    void OnDestroy() 
    {
        StarterAssets.FirstPersonController.OnFireChanged -= FireChanged;
    }

    void Update() 
    {
        if(m_isShooting)          
            Shoot();
    }

    void FireChanged(bool isFiring)
    {
        // CustomDebug.Log("fire: " + isFiring);
        if(isFiring)
        {
            m_isShooting = true;
        }
        else
        {
            m_isShooting = false;
        }
    }

    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();

        if (m_isOneShotWeapon)
            m_isShooting = false;

    }

    void PlayMuzzleFlash()
    {
        m_muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit raycastHitInfo;


        if (Physics.Raycast(m_FPCamera.transform.position, m_FPCamera.transform.forward, out raycastHitInfo, m_range))
        {
            Health target = raycastHitInfo.transform.GetComponent<Health>();

            CreateHitImpact(raycastHitInfo);
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
#endif
    }
}

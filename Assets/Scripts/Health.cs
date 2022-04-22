using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO why summary on take damage does not work
//TODO fix on death delegates -they are ugly
public class Health : MonoBehaviour
{
#region CACHE

    bool m_isPlayer;

#endregion

#region properties

    [Header("Properties")]
    [SerializeField]
    float m_maxHitPoints = 100f;

#endregion

#region states

    float m_currentHitPoints;
    //is player / is enemy

#endregion

#region delegates

    //will it get called for everyone on whatever death?
    public delegate void Death(GameObject gameObject);
    public static event Death OnNonPlayerDeath  = delegate{};
    public static event Death OnPlayerDeath  = delegate{};

#endregion

    /////////////////////////////////////////////////////////

    void Awake() 
    {
        m_currentHitPoints = m_maxHitPoints;
        m_isPlayer = GetComponent<StarterAssets.FirstPersonController>() != null;        
    }

#region ProcessDamage

    /// <summary>
    /// returning resulting hp, calling OnDeath delegate in case of hp <= 0
    /// </summary>
    public float TakeDamage(float damage)
    {
        m_currentHitPoints -= damage;
        if(m_currentHitPoints <= 0f)
        {
            if(m_isPlayer)
                OnPlayerDeath(gameObject);
            else
                OnNonPlayerDeath(gameObject);

            Destroy(gameObject, .1f);
        }            

        return  m_currentHitPoints;
    }

#endregion
}
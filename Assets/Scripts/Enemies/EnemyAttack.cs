using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
#region CACHE

    [SerializeField]
    float m_damageAmount = 40f;

    StarterAssets.FirstPersonController m_target;

#endregion

#region STATES

    bool m_isPlayerDead;

#endregion

    ///////////////////////////////////////////////////////////////////

    void Awake() 
    {
        m_target = FindObjectOfType<StarterAssets.FirstPersonController>();       
    }

    void Start() 
    {        
    }

#region Damage

    public void AttackHitEvent()
    {
        if(m_target && !m_isPlayerDead)
        {
            float currentPlayerHp = m_target.GetComponent<Health>().TakeDamage(m_damageAmount);
            if(currentPlayerHp <= 0f)
            {
                m_isPlayerDead = true;   
            }
        }
    }

#endregion
}

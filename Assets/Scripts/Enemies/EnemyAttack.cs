using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
    #region CACHE

        [SerializeField]
        float m_damageAmount = 40f;

        UI.DisplayDamage _damageUIScript;
        StarterAssets.FirstPersonController m_target;

    #endregion

    #region STATES

        bool m_isPlayerDead;

    #endregion

        ///////////////////////////////////////////////////////////////////

        void Awake() 
        {
            m_target = FindObjectOfType<StarterAssets.FirstPersonController>();    
            _damageUIScript = FindObjectOfType<UI.DisplayDamage>();   
        }

        void Start() 
        {        
        }

    #region Damage

        public void AttackHitEvent()
        {
            if(m_target && !m_isPlayerDead)
            {
                float currentPlayerHp = m_target.GetComponent<Combat.Health>().TakeDamage(m_damageAmount);
                _damageUIScript.ShowDamageImpact(); 
                if(currentPlayerHp <= 0f)
                {
                    m_isPlayerDead = true;  
                }
            }
        }

    #endregion
    }
}

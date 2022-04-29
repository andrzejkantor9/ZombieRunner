using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
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
        // public delegate void Death(GameObject gameObject);
        // public static event Death OnNonPlayerDeath  = delegate{};
        // public static event Death OnPlayerDeath  = delegate{};
        
        public delegate void Death();
        public event Death OnDeath = delegate {};

        public delegate void TakeDamageDelegate(float currentHitPoints);
        public event TakeDamageDelegate OnTakeDamage = delegate {};

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
            OnTakeDamage(m_currentHitPoints);

            if(m_currentHitPoints <= 0f)
            {
                Die();
                return m_currentHitPoints;
            }            

            MyDebug.Debug.Log($"current HP: {m_currentHitPoints.ToString()}, object: {gameObject.name}");
            return  m_currentHitPoints;
        }

        private void Die()
        {
            // if(m_isPlayer)
            //     OnPlayerDeath(gameObject);
            // else
            //     OnNonPlayerDeath(gameObject);

            OnDeath();

        }

    #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
#region CACHE

    [SerializeField]
    Transform m_target;
    [SerializeField]
    float m_damageAmount = 40f;

#endregion

    ///////////////////////////////////////////////////////////////////

    void Start() 
    {        
    }

#region Damage

    public void AttackHitEvent()
    {
        if(m_target)
        {
            CustomDebug.Log("bang bang");
        }
    }

#endregion
}

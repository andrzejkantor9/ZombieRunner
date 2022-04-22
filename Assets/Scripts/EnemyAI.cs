using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
#region CACHE
    [Header("CACHE")]
    [SerializeField]
    Transform m_target;

    NavMeshAgent m_navMeshAgent;
    Animator m_animator;

    //Animations Id
    int m_IdleAnimId;
    int m_MoveAnimId;
    int m_AttackAnimId;
#endregion

#region PROPERTIES
    [Space(10)][Header("PROPERTIES")]
    [SerializeField]
    float m_chaseRange = 5f;
#endregion

#region STATES
    bool m_isProvoked;
    float m_distanceToTarget = Mathf.Infinity;
#endregion

    ///////////////////////////////////////////////////////////

    void Awake() 
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();        
        m_animator = GetComponent<Animator>();

        AssertCache();
    }

    void AssertCache()
    {
        //asserts
    }

    void Start()
    {
        m_IdleAnimId = Animator.StringToHash("Idle");
        m_MoveAnimId = Animator.StringToHash("Move");
        m_AttackAnimId = Animator.StringToHash("Attack");
    }

    void Update() 
    {
        m_distanceToTarget = Vector3.Distance(m_target.position, transform.position);

        if(m_isProvoked)
        {
            EngageTarget();
        }
        else if(m_distanceToTarget <= m_chaseRange)
        {
            m_isProvoked = true;
            // m_navMeshAgent.SetDestination(m_target.position);  
        }           
    }

    void OnDrawGizmosSelected()
    {
        var color = Color.red;
        // color.a = .8f;
        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, m_chaseRange);        
    }

    void EngageTarget()
    {
        if(m_distanceToTarget > m_navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        m_animator.SetBool(m_AttackAnimId, false);
        m_animator.SetTrigger(m_MoveAnimId);
        m_navMeshAgent.SetDestination(m_target.position);
    }

    void AttackTarget()
    {
        // CustomDebug.Log($"{name} has seeked and is destroying: {m_target.name}");
        m_animator.SetBool(m_AttackAnimId, true);
    }
}

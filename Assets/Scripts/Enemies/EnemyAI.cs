using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo - clean animations id's list / dictionary
//todo visualise range component independent, *with option to reference float variable from other script
//*todo - add patrol ai script
//todo - redo input with my own input actions assets
//todo - fix camera stutter
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
#region CACHE
    [Header("CACHE")]
    [SerializeField]
    Transform _target;

    [SerializeField][HideInInspector]
    NavMeshAgent _navMeshAgent;
    [SerializeField][HideInInspector]
    Animator _animator;
    [SerializeField][HideInInspector]
    Health _health;

    //Animations Id
    int m_IdleAnimId;
    int m_MoveAnimId;
    int m_AttackAnimId;
    int _DeathAnimId;
#endregion

#region PROPERTIES
    [Space(10)][Header("PROPERTIES")]
    [SerializeField]
    float m_chaseRange = 5f;
    [SerializeField]
    float m_turnSpeed = 5f;
#endregion

#region STATES
    bool m_isProvoked;
    float m_distanceToTarget = Mathf.Infinity;

    public bool _isDead {get; private set;}

#endregion

    ///////////////////////////////////////////////////////////

    void OnValidate() 
    {        
        SetupCache();
    }

    void Awake() 
    {
        AssertCache();

        SetupAnimationHashes();
    }

    void Start()
    {
        BindDelegates();
    }

    private void OnDestroy() 
    {
        UnbindDelegates();
    }

    void Update()
    {
        ProcessBehavior();
    }

    void OnDrawGizmosSelected()
    {
        DrawSphereChaseRange();
    }

#region Editor

    void DrawSphereChaseRange()
    {
        var color = Color.red;
        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, m_chaseRange);
    }

#endregion

#region Setup

    void SetupCache()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();        
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    void AssertCache()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        UnityEngine.Assertions.Assert.IsNotNull(_target, $"Script: {GetType().ToString()} variable _target is null");
#endif
    }

    void SetupAnimationHashes()
    {
        m_IdleAnimId = Animator.StringToHash("Idle");
        m_MoveAnimId = Animator.StringToHash("Move");
        m_AttackAnimId = Animator.StringToHash("Attack");
        _DeathAnimId = Animator.StringToHash("Die");
    }

    void BindDelegates()
    {
        _health.OnTakeDamage += ProcessDamageTaken;
        _health.OnDeath += Die;
    }

    void UnbindDelegates()
    {
        _health.OnTakeDamage -= ProcessDamageTaken;
        _health.OnDeath -= Die;
    }

    private void Die()
    {
        if(_isDead) return;
        
        _animator.SetTrigger(_DeathAnimId);
        _isDead = true;
        enabled = false;
        _navMeshAgent.enabled = false;
    }

#endregion

#region PlayerInteraction

    private void ProcessBehavior()
    {
        if(!_target) return;

        m_distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (m_isProvoked)
        {
            EngageTarget();
        }
        else if (m_distanceToTarget <= m_chaseRange)
        {
            m_isProvoked = true;
        }
    }

    void EngageTarget()
    {
        FaceTarget();
        if(m_distanceToTarget > _navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    public void ProcessDamageTaken(float currentHitPoints)
    {
        m_isProvoked = true;
    }

    void ChaseTarget()
    {
        _animator.SetBool(m_AttackAnimId, false);
        _animator.SetTrigger(m_MoveAnimId);
        _navMeshAgent.SetDestination(_target.position);
    }

    void AttackTarget()
    {
        _animator.SetBool(m_AttackAnimId, true);
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
    }

#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo - fix ais not navigating in bunker

//todo - clean animations id's list / dictionary
//TODO player death should not be in first person controller
    //TODO fix on death delegates -they are ugly
//todo - equipment namespace instead of weapon
//todo - universal pickup class instead of batery and ammo

//todo - restructure atchitecture so references are gooten in reasonable way, but components are independent
    //maybe master component on instance transform or game manager
    //maybe mvp
//todo - introduce proper namespaces
//todo - introduce pure c# class?
//actions, delegates, interfaces, patterns, addresables, jobs, burst, lambdas, *graphic jobs, shaders, object pool, 
//*uml, custom editors, clean inspector, pure c#, git branching, naming conventions, dependency injection, scriptable object, unit tests
//todo - introduce interfaces
//todo - introduce depencency injection - zenject
//todo - proper ui references
//todo - play with inspector look
//todo - check scriptable object
//todo - check soft references

//todo - add footstep component
//todo? - explosion component with sound and particles out of the box
//todo - rockets pickup
//todo - add basic UI's main menu, pause, game win
//todo - make current hp display as red vignete
//todo - remove explosion instantiation at start of game

//todo - do incredible 5 min experience
//todo - redo input with my own input actions assets
//todo visualise range component independent, *with option to reference float variable from other script

//todo - make things as project universal as possible
//todo - add last functions to example scrip
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
#region CACHE
    [Header("CACHE")]
    [SerializeField][HideInInspector]
    NavMeshAgent _navMeshAgent;
    [SerializeField][HideInInspector]
    Animator _animator;
    [SerializeField][HideInInspector]
    Health _health;

    Transform _target;

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

        Setup();
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
        // UnityEngine.Assertions.Assert.IsNotNull(_target, $"Script: {GetType().ToString()} variable _target is null");
#endif
    }

    void Setup()
    {
        _target = FindObjectOfType<StarterAssets.FirstPersonController>().transform;

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

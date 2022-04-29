using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//addresables - write down when to use actually

//namespaces everywhere
//naming conventions, write to cheat script
//check all inspector attributes
//pure c#
//actions, delegates, lambdas,
//scriptable object
//jobs, burst, 
//interfaces
    //do pickups with delegates?
//dependency injection
//unit tests
//todo - restructure atchitecture so references are gooten in reasonable way, but components are independent
    //maybe master component on instance transform or game manager
    //maybe mvp
//todo - proper ui references
//todo - check soft references

//assembly definitions, namespaces - write to notes, write cheatsheet about what to consider to use next time
//todo - add footstep component
//todo? - explosion component with sound and particles out of the box
//todo - add basic UI's main menu, pause, game win
//todo - make current hp display as red vignete
//todo - remove explosion instantiation at start of game

//todo - do incredible 5 min experience
    //zombies becoming faster and screaming when shot or after time
//todo - redo input with my own input actions assets
//todo visualise range component independent, *with option to reference float variable from other script

//todo - make things as project universal as possible
//todo - add last functions to example script
//todo - introduce 2 assembies to inter-project folder
    //?debug?
//todo - check if i need to pass meta filed of materials to new project

//in next project
//todo - get animator states from editor then create dictionary
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

    //Animations Id / hashes
    Dictionary<AnimationStates, int> _animationHashes = new Dictionary<AnimationStates, int>();
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

    private enum AnimationStates
    {
        Idle,
        Move,
        Attack,
        Die
    }

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

        _animationHashes.Add(AnimationStates.Idle, Animator.StringToHash("Idle"));
        _animationHashes.Add(AnimationStates.Move, Animator.StringToHash("Move"));
        _animationHashes.Add(AnimationStates.Attack, Animator.StringToHash("Attack"));
        _animationHashes.Add(AnimationStates.Die, Animator.StringToHash("Die"));
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
        
        _animator.SetTrigger(_animationHashes[AnimationStates.Die]);
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
        _animator.SetBool(_animationHashes[AnimationStates.Attack], false);
        _animator.SetTrigger(_animationHashes[AnimationStates.Move]);
        _navMeshAgent.SetDestination(_target.position);
    }

    void AttackTarget()
    {
        _animator.SetBool(_animationHashes[AnimationStates.Attack], true);
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
    }

#endregion
}

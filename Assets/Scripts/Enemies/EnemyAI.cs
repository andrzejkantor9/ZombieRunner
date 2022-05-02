using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptable object
//interfaces
    //do pickups with interfaces?
//jobs, burst, tasks
//naming conventions
//actions, delegates, events, func, lambdas, predicates

//check all inspector attributes
//dependency injection
//unit tests

//todo - restructure atchitecture so references are gooten in reasonable way, but components are independent
    //maybe master component on instance transform or game manager
    //maybe mvp
    //make everything as reusable as possible
    //try to refactor so as little namespace using is needed (in ohter places than given namespace)
    //architecture - like: saving system, saving wrapper - separating logic layer with access layer
//todo - proper ui references
//todo - check soft references
//pure c#
    //ammo + split ui and logic
    //weaponSwitcher + split what needs to be split
//setup CI pipeline

//todo - add footstep component
//todo? - explosion component with sound and particles out of the box and event assigment from argon assault
//todo - add basic UI's main menu, pause, game win
//todo - ui with controls display
//todo - make current hp display as red vignete
//todo - remove explosion instantiation at start of game
//todo - add hp pickup

//todo - do incredible 5 min experience
    //zombies becoming faster and screaming when shot or after time
//todo - redo input with my own input actions assets
//todo - visualise range component independent, *with option to reference float variable from other script

//todo - clean inter-project folder, introduce 2 assembies to it
    //?debug?
//todo - check if i need to pass meta filed of materials to new project
//todo - get mouse raycast from tower defense

//todo - get animator states from editor then create dictionary in editor time
//custom editors (class tooltip, disable inspector param conditionaly), editor to make ?dialogue?

//plan - co robic
    //yt
        //video o gicie
        //setup fb
        //setup tik tok, insta
    //douczanie sie unity na tym projekcie
        //tak jak spisane powyzej
    //robienie action combat gry
        //import universalnych rzeczy co mam juz, setup banana gita
        //import tps controlera
            //wylaczenie skoku
            //poprawienie inputu na event driven i project universal
        //setup przeciwnika - inny kolor, podchodzi do mnie, zatrzymuje sie, log ze jest gotowy do ataku
            //wziac z zombie runnera baze na to i poprawic w obu miejscach
        //setup kwadratowej areny z widzialnymi scianami, podstawowym oswietleniem, camera zoomem i podstawowymi teksturami
        //setup bg music, main menu, pause menu, footsetpy
            //z dobra architektura
        //setup hp
            //wziac z tego projektu
            //?1 canvas i osobne prefaby ui?
            //ja zadaje obrazenia na kliku, przeciwnik co jakis czas
            //sfxy na zadawaniu i otrzymywaniu obrazen + vfx'y
            //ui zycia mojego i przeciwnika + prawidlowa architektura tego
        //setup systemu kombosow
            //?state pattern?
            //ja i przeciwnik
            //?scriptable object?
            //lr, llrr, lllrr
            //przeciwnik losujacy jego kombosy
            //debug piszacyc kto robi co
        //refactor round
        //zrobienie placehorderowych anmiacji, import, podpiecie ich pod juz istniejace event
            //architektura
        //uniki bohatera
        //doskoki bossa
        //przerwanie naszych animacji przez bossa - stagger
        //setup CI pipeline
        //camera ?manager? - pokazywanie wazniejszych wydarzen z walki w innych ujeciach
            //?cinemachine?
        //trash moby
        //vertical slice
        //hp packi
        //whatever story
    //sprawdzenie rzeczy w ue5
        //skonczenie sekcji kursu
    //robienie / nauka robienia assetow
        //kurs blendera od gamedev.tv na postacie
using UnityEngine.AI;

namespace ZombieRunner.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ZombieRunner.Combat.Health))]
    public class EnemyAI : MonoBehaviour
    { 
    #region CACHE
        [Header("CACHE")]
        [SerializeField][HideInInspector]
        NavMeshAgent _navMeshAgent;
        [SerializeField][HideInInspector]
        Animator _animator;
        [SerializeField][HideInInspector]
        ZombieRunner.Combat.Health _health;

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
            _health = GetComponent<ZombieRunner.Combat.Health>();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_speed = 0.5f;
    public float run_speed = 4f;

    public float chase_Distance = 7f;
    public float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_for_this_Time = 15f;
    private float patrol_Timer;

    public float wait_before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_audio;

    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemy_audio = GetComponentInChildren<EnemyAudio>();
    }

    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_for_this_Time;

        attack_Timer = wait_before_Attack;

        current_Chase_Distance = chase_Distance;
    }

    void Update()
    {
        if(enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        if(enemy_State == EnemyState.CHASE)
        {
            Chase();
        }
        if(enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        // Enable the movement of navagent
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_for_this_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }
        
        // Animation
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        // tets the distance between player and enemy

        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.CHASE;

            //  play spotted audio
            enemy_audio.play_screamSound();
        }
    }

    void Chase()
    {
        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_speed;

        // set the destinaton as player's destination
        navAgent.SetDestination(target.position);

        // Animation
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        // If the distance between enemy and playre is < attack distance
        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            // stop animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;    
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            // player run away from the enemy

            // stop running
            enemy_Anim.Run(false);
            enemy_State = EnemyState.PATROL;

            // reset the patrol timer so that the function can calculate the new patrol destinationright away
            patrol_Timer = patrol_for_this_Time;

            // reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;    
            }
        }

    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_before_Attack)
        {
            enemy_Anim.Attack();

            attack_Timer = 0f;

            // play attack sound
            enemy_audio.play_attackSound();
        }

        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        // Using this code we have now made the enemy patrol over the terrain for which we previously had to use different check points make hin(the enemy) move within but now hw can mov freely in the  whole terrain
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        // If the navigational area is outside of our terrain then this code will randomly search for a new navigational area within the terrain here -1 means all layer
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Tuen_Off_AttackPoint()
    {
        if(attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }
    
    public EnemyState Enemy_State
    {
        get; set;
    }
}

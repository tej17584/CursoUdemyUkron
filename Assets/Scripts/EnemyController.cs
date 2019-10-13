using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;

    public int currentPatrolPoints;

    public NavMeshAgent agent;

    public Animator anim;

    public enum AiState
    {
        IsIdle,
        IsPatrolling,
        IsChasing,
        IsAttacking
    }

    public AiState currenState;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    public float waitAtPoint = 2f;

    private float waitCounter;

    //rango de distancia
    public float chaseRange;

    public float attackRange = 1f;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public float timeBetweenAttacks = 2f;

    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (currenState)
        {
            case AiState.IsIdle:
                anim.SetBool(id: IsMoving, false);
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currenState = AiState.IsPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoints].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currenState = AiState.IsChasing;
                    anim.SetBool(id: IsMoving, true);
                }

                break;
            case AiState.IsPatrolling:
                //agent.SetDestination(patrolPoints[currentPatrolPoints].position);
                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoints++;
                    if (currentPatrolPoints >= patrolPoints.Length)
                    {
                        currentPatrolPoints = 0;
                    }

                    //agent.SetDestination(patrolPoints[currentPatrolPoints].position);
                    currenState = AiState.IsIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currenState = AiState.IsChasing;
                }

                anim.SetBool(IsMoving, true);
                break;
            case AiState.IsChasing:
                agent.SetDestination(PlayerController.instance.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    currenState = AiState.IsAttacking;
                    anim.SetTrigger(Attack);
                    anim.SetBool(id: IsMoving, false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    attackCounter = timeBetweenAttacks;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currenState = AiState.IsIdle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }

                break;
            case AiState.IsAttacking:
                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger(id: Attack);
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currenState = AiState.IsIdle;
                        waitCounter = waitAtPoint;

                        agent.isStopped = false;
                    }
                }

                break;
        }
    }
}
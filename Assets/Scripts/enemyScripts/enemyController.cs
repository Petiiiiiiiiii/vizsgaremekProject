using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    private Transform player;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public float detectionRange = 13f;
    public Animator animator;
    private NavMeshAgent agent;
    private bool canSeePlayer;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if (distanceToPlayer <= detectionRange)
        {
            canSeePlayer = true;
            if (agent.enabled)
            {
                agent.SetDestination(player.position);
                RotateTowardsPlayer();
            }
        }
        else
        {
            if (agent.enabled)
            {
                canSeePlayer = false;
                agent.SetDestination(transform.position);
            }
        }

        animator.SetBool("playerSpotted", canSeePlayer);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackCooldown);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}

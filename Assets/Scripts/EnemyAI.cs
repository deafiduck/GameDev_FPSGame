using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    Transform target;
    public bool isDead = false;

    float turnSpeed = 5f;
    public float damage = 10f;
    public float attackCooldown = 1.0f; // Saldırılar arasındaki bekleme süresi
    public float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextAttackTime = Time.time;
    }

    public float distance;

    void Update()
    {
        if (isDead) return;

        distance = Vector3.Distance(transform.position, target.position);

        if (distance < 20 && distance > agent.stoppingDistance)
        {
            ChasePlayer();
        }
        else if (distance <= agent.stoppingDistance && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
        else if (distance > 20)
        {
            StopChase();
        }
    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        anim.SetBool("isRunning", true);
    }

    public void AttackPlayer()
    {
        if (PlayerHealth.PH != null)
        {
            PlayerHealth.PH.Damage(damage); // Her saldırıda sabit 10 hasar ver
            nextAttackTime = Time.time + attackCooldown; // Saldırılar arasında bekleme süresi
        }
        else
        {
            Debug.LogWarning("PlayerHealth.PH is null! Check initialization.");
        }

        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }
        agent.updatePosition = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", true);
        anim.SetBool("isIdle", false);
    }

    void StopChase()
    {
        agent.updatePosition = false;
        anim.SetBool("isRunning", false);
    }

    public void DeadAnim()
    {
        if (isDead) return;

        isDead = true;
        anim.SetBool("isDead", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", false);
        ScoreManager.instance.AddScore(10);
        agent.isStopped = true;

        Destroy(gameObject, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    Transform target; //ana karakterin pozisyonu
    public bool isDead = false;

    float turnSpeed = 5f; //player'a doğru dönme hızı
    public float distance;
    //public bool canAttack; //zombi, palyer'a atak yapacak durumda mı. Yani canını 25 azalatabilecek mi.
    [SerializeField]
    float attackTimer = 2f; // player'ın 2 saniyede bir canı azalsın.

    EnemyHealth enemyHealth;

    public float damage = 10f; //enemy'nin bize veridği hasar

    bool canAttack = true; // Saldırının gerçekleşip gerçekleşmeyeceğini kontrol eder.
    void Start()
    {
        //canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
          distance = Vector3.Distance(transform.position, target.position); //zombinin konumu ve ana karakterin konumu arasındaki mesafeyi distance olarak tanımladık

        if (distance < 20 && distance > agent.stoppingDistance /* && !isDead*/)
        {
            ChasePlayer();
            Debug.Log("takip ediyoruz ğuuu");
        }
        else if (distance <= agent.stoppingDistance /*&& canAttack == true && PlayerHealth.PH.isDead == false*/) //player canlı ise, zombi ona saldırabilsin
        {
             AttackPlayer();
            Debug.Log("attack etmek lazım");
        }
        else if (distance > 20) //zombir, artık takip etmesin
        {
            StopChase();
            Debug.Log("takibi biraktik ğuuu");
        }

        
    }

    //karakteri takip ettiren fonk
    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true; //pozisyonu güncellenecek
        agent.SetDestination(target.position);
        anim.SetBool("isRunning", true);
        //anim.SetBool("Attack", false); //koşarken atak yapamasın
    }

   void AttackPlayer()
{
    if (!canAttack) return;

    PlayerHealth.PH.Damage(damage);

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

    StartCoroutine(AttackCooldown());
}

    void StopChase()
    {
        agent.updatePosition = false;
        anim.SetBool("isRunning", false);
        //anim.SetBool("Attack", false);
    }

    public void DeadAnim()
    {
        
        isDead = true;
        Debug.Log("Enemy öldüü");
        anim.SetBool("isDead", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", false);

    }


    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackTimer); // Saldırı arasındaki bekleme süresi.
        canAttack = true;
    }

    /* public void Hurt()
     {
         agent.enabled = false;
         anim.SetTrigger("Hit");
         StartCoroutine(Nav());
     }

     IEnumerator Nav()
     {
         yield return new WaitForSeconds(1.5f);
         agent.enabled = true;
     }*/
}
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

    //public bool canAttack; //zombi, palyer'a atak yapacak durumda mı. Yani canını 25 azalatabilecek mi.
    [SerializeField]
    float attackTimer = 2f; // player'ın 2 saniyede bir canı azalsın.

    EnemyHealth enemyHealth;
    void Start()
    {
        //canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public float distance;

    void Update()
    {
          distance = Vector3.Distance(transform.position, target.position); //zombinin konumu ve ana karakterin konumu arasındaki mesafeyi distance olarak tanımladık

        if (distance < 15 && distance > agent.stoppingDistance /* && !isDead*/)
        {
            ChasePlayer();
            Debug.Log("takip ediyoruz ğuuu");
        }
        else if (distance <= agent.stoppingDistance /*&& canAttack == true && PlayerHealth.PH.isDead == false*/) //player canlı ise, zombi ona saldırabilsin
        {
             AttackPlayer();
            Debug.Log("attack etmek lazım");
        }
        else if (distance > 15) //zombir, artık takip etmesin
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
        // PlayerHealth.PH.Damage(damage);

        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;  //zombi atak yaparken yüzünün player'a dönük olması için rotasyonunu ayarladık
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }
        agent.updatePosition = false; //durarak atak yapacak
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", true);
        anim.SetBool("isIdle",false);
        //  StartCoroutine(AttackTime());
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
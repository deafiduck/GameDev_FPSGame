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

     float turnSpeed=5f; //player'a doğru dönme hızı
  
    public bool canAttack; //zombi, palyer'a atak yapacak durumda mı. Yani canını 25 azalatabilecek mi.
    [SerializeField]
    float attackTimer = 2f; // player'ın 2 saniyede bir canı azalsın.
    void Start()
    {
        canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position); //zombinin konumu ve ana karakterin konumu arasındaki mesafeyi distance olarak tanımladık

        if (distance < 10 && distance > agent.stoppingDistance && !isDead)
        {
            ChasePlayer();
        }
        else if (distance <= agent.stoppingDistance && canAttack == true /*&& PlayerHealth.PH.isDead == false*/) //player canlı ise, zombi ona saldırabilsin
        {
            AttackPlayer();
        }
        else if (distance > 10) //zombir, artık takip etmesin
        {
            StopChase();
        }
    }

    //karakteri takip ettiren fonk
    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true; //pozisyonu güncellenecek
        agent.SetDestination(target.position);
        anim.SetBool("isRunning", true);
        anim.SetBool("Attack", false); //koşarken atak yapamasın
    }

    void AttackPlayer()
    {
        // PlayerHealth.PH.Damage(damage);

        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;  //zombi atak yaparken yüzünün player'a dönük olması için rotasyonunu ayarladık
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false; //durarak atacak yapacak
        anim.SetBool("isRunning", false);
        anim.SetBool("Attack", true);
        //  StartCoroutine(AttackTime());
    }

    void StopChase()
    {
        agent.updatePosition = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("Attack", false);
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMonster : MonoBehaviour
{
    [SerializeField] GameObject BubbleM; // Bubble objesi
    [SerializeField] int BubbleMonsterHealth = 250; // Can
    [SerializeField] GameObject Player; // Takip edilecek oyuncu
    [SerializeField] float followSpeed = 5f; // Takip h�z�
    float distance;
    

    [SerializeField] private EnemyAI EnemyAI; // EnemyAI bile�eni

    void Start()
    {
        if (EnemyAI == null)
        {
            // Ayn� objede EnemyAI var m� kontrol et
            EnemyAI = GetComponent<EnemyAI>();
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, Player.gameObject.transform.position);
        FollowPlayer();

        if (distance <= 3f)
        {
            if (EnemyAI != null)
            {
                EnemyAI.AttackPlayer();
            }
            else
            {
                Debug.LogWarning("EnemyAI reference is missing!");
            }
        }
    }

    public void FollowPlayer()
    {
        if (Player != null && BubbleMonsterHealth > 0)
        {
            // BubbleMonster'�n pozisyonunu oyuncuya do�ru hareket ettir
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, followSpeed * Time.deltaTime);

            // Oyuncuya do�ru bakmas�n� sa�la
            Vector3 direction = Player.transform.position - transform.position;
            direction.y = 0; // Dikey ekseni sabit tut
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * followSpeed);
            }
        }
    }

    public void ReduceHealth(int reduceHealth)
    {
        BubbleMonsterHealth -= reduceHealth;
        Debug.Log("BubbleMonster Health Reduced: " + BubbleMonsterHealth);

        if (BubbleMonsterHealth <= 0)
        {
            BubbleMonsterHealth = 0;
            gameObject.SetActive(false); // BubbleMonster'� etkisiz hale getir
        }
    }
}

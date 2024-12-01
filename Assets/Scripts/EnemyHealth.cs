using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    EnemyAI enemy;

    private void Start()
    {
        enemy = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }
    }

    public void ReduceHealth(float reduceHealth)
    {
        enemyHealth -= reduceHealth;
        Debug.Log("Enemy Health Reduced: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            enemy.DeadAnim();
        }
    }
}

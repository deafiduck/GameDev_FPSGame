using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;

    private void Update()
    {
        if (enemyHealth < 0)
        {
            enemyHealth = 0; //can eksi olamasin
        }
    }

    public void ReduceHealth(float reduceHealth)  //parametre olarak canın ne kadar azalacağını alıyor
    {
        enemyHealth -= reduceHealth;
        Debug.Log("Enemy Health Reduced: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Debug.Log("Enemy Dead");
        //enemy.canAttack = false; //enemy öldüğünde player'a zarar veremesin.
        Destroy(gameObject, 10f);
    }
}
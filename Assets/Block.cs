using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int BlockHealth = 100;

    [SerializeField] GameObject GameObjectShow;

    private void Update()
    {
        if (BlockHealth < 0)
        {
            BlockHealth = 0;
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameObjectShow.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void ReduceHealth(int reduceHealth)
    {
        BlockHealth -= reduceHealth;
        Debug.Log("block Health Reduced: " + BlockHealth);

        if (BlockHealth <= 0)
        {
            BlockHealth = 0;
            gameObject.SetActive(false);

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private GameOverManager gameOverManager;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager bulunamad�! Scene'de GameOverManager objesinin oldu�undan emin olun.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter tetiklendi. �arp��an obje: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("MainChar"))
        {
            Debug.Log("Player ile �arp��ma ger�ekle�ti.");
            gameOverManager.ShowGameOverPanel();
        }
    }

}

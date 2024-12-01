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
            Debug.LogError("GameOverManager bulunamadý! Scene'de GameOverManager objesinin olduðundan emin olun.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter tetiklendi. Çarpýþan obje: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("MainChar"))
        {
            Debug.Log("Player ile çarpýþma gerçekleþti.");
            gameOverManager.ShowGameOverPanel();
        }
    }

}

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
            Debug.LogError("GameOverManager bulunamadı! Scene'de GameOverManager objesinin olduğundan emin olun.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter tetiklendi. Çarpışan obje: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("MainChar"))
        {
            Debug.Log("Player ile çarpışma gerçekleşti.");
            gameOverManager.ShowGameOverPanel();
        }
    }

}

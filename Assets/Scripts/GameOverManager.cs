using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
        gameOverPanel.SetActive(false); // Ba�lang��ta paneli gizle
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Paneli g�ster
        StartCoroutine(FadeInGameOverPanel());
        Time.timeScale = 0f; // Oyunu durdur
    }

    private IEnumerator FadeInGameOverPanel()
    {
        float duration = 1f; // Ge�i� s�resi (1 saniye)
        float currentTime = 0f;

        canvasGroup.alpha = 0f; // Ba�lang�� alfa de�eri

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime; // Zaman� art�r
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, currentTime / duration); // Alfa de�erini kademeli olarak art�r
            yield return null;
        }

        canvasGroup.alpha = 1f; // Tamamen g�r�n�r yap
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Oyunu yeniden ba�lat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden y�kle
    }

    public void Quit()
    {
        Application.Quit(); // Oyunu kapat
    }
}

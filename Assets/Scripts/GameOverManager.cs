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
        gameOverPanel.SetActive(false); // Baþlangýçta paneli gizle
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Paneli göster
        StartCoroutine(FadeInGameOverPanel());
        Time.timeScale = 0f; // Oyunu durdur
    }

    private IEnumerator FadeInGameOverPanel()
    {
        float duration = 1f; // Geçiþ süresi (1 saniye)
        float currentTime = 0f;

        canvasGroup.alpha = 0f; // Baþlangýç alfa deðeri

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime; // Zamaný artýr
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, currentTime / duration); // Alfa deðerini kademeli olarak artýr
            yield return null;
        }

        canvasGroup.alpha = 1f; // Tamamen görünür yap
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Oyunu yeniden baþlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden yükle
    }

    public void Quit()
    {
        Application.Quit(); // Oyunu kapat
    }
}

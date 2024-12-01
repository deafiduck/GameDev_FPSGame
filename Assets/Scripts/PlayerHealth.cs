using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth; //şu anki can
    public float maxHealth = 100f;
    public static PlayerHealth PH;

    public bool isDead; //player ölü mü degil mi 

    public Slider healthBarSlider;
    public Text healthText;

    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    bool isTakingDamage = false;

    float colorSpeed = 0.5f;

    public GameObject gameOverPanel; // Game Over paneli

    private void Awake() //start fonkundan önce calisiyo
    {
        PH = this;
    }

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        healthText.text = maxHealth.ToString();
        gameOverPanel.SetActive(false); // Oyun başında paneli gizle
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        if (isTakingDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSpeed * Time.deltaTime);
        }
        isTakingDamage = false;
    }

    public void Damage(float damage)
    {
        if (isDead) return;

        StartCoroutine(ReduceHealthOverTime(damage));
    }

    IEnumerator ReduceHealthOverTime(float damage)
    {
        float damagePerStep = damage / 10f; // 10 adımda hasar verecek
        int steps = 10;
        float stepDuration = 0.1f; // Her adım 0.1 saniye sürecek

        while (steps > 0 && currentHealth > 0)
        {
            currentHealth -= damagePerStep;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                Dead();
                break;
            }
            isTakingDamage = true;
            healthBarSlider.value = currentHealth;
            UpdateText();
            steps--;
            yield return new WaitForSeconds(stepDuration); // Adımlar arasında bekle
        }

        if (!isDead)
        {
            isTakingDamage = false;
        }
    }

    public void UpdateText()
    {
        healthText.text = currentHealth.ToString();
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        healthBarSlider.value = 0;
        UpdateText();
        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        gameOverPanel.SetActive(true);
    }
}

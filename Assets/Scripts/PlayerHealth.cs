using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth; //şu anki can
    public float maxHealth = 100;
    public static PlayerHealth PH;

    public bool isDead; //player ölü mü degil mi 

    public Slider healthBarSlider;
    public Text healthText;

    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    bool isTakingDamage = false;

    float colorSpeed = 0.5f;

    private GameOverManager gameOverManager;

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
        gameOverManager = FindObjectOfType<GameOverManager>(); // GameOverManager referansını al
    }

    void Update()
    {
        Debug.Log(currentHealth);
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

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            Dead();
        }
        else
        {
            isTakingDamage = true;
        }

        healthBarSlider.value = currentHealth;
        UpdateText();
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
        gameOverManager.ShowGameOverPanel(); // Game Over panelini göster
    }
}

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

    
    [Header("Damage Screen")] //Inspectro kýsmýnda Damage Screen diye bir baþlýk attý onun altýna da bu deðiþkenleri koydu
    public Color damageColor;
    public Image damageImage;
    bool isTakingDamage = false;

    float colorSpeed = 0.5f;
    private void Awake() //start fonkundan önce calisiyo
    {
        PH = this;
    }
    void Start()
    {
       isDead = false;
       currentHealth = maxHealth;
       healthBarSlider.value = maxHealth;
       healthText.text = maxHealth.ToString();
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
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSpeed * Time.deltaTime); //bir þeyin zamanla iki nokta arasýnda deðiþmesini istiyorsak Lerp veya Slerp kullaabiliriz. 
        }
        isTakingDamage = false;
    }

    public void Damage(float damage)
    {

        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                isTakingDamage = true;
                Dead();
            }
            else
            {
                isTakingDamage = true;
                currentHealth -= damage;
                healthBarSlider.value -= damage;
                UpdateText();
            }

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
        StartCoroutine(Wait());
        SceneManager.LoadScene(1);
    }

    IEnumerator Wait() //sahneler arası geçişi sağlamak için
    {
        yield return new WaitForSeconds(1f);
    }
}

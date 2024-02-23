using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar1;
    public Image healthBar2;
    public Text healthText1;
    public Text healthText2;
    private Image selectedHealthBar;
    private Text selectedHealthText;
    float health, maxHealth = 200;
    float lerpSpeed;
    public bool IsImmune { get; set; } // Sleduje, zda je hráč imunní vůči poškození

    void Start()
    {
        health = maxHealth;

        if (gameObject.tag == "Player1")
        {
            selectedHealthBar = healthBar1;
            selectedHealthText = healthText1;
        }
        else if (gameObject.tag == "Player2")
        {
            selectedHealthBar = healthBar2;
            selectedHealthText = healthText2;
        }
    
    }

    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        lerpSpeed = 3f * Time.deltaTime;
        
        HealthBarFiller();
        ColorChanger();

        // Aktualizace zdravotního textu
        if (selectedHealthText != null)
        {
            selectedHealthText.text = health.ToString("0"); // Zobrazí zdraví jako celé číslo
        }
    }

    public void AddHealth(float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(float damage)
    {
if (!IsImmune) // Odečte zdraví pouze pokud hráč není imunní
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
    }

        //health -= damage;
        //health = Mathf.Clamp(health, 0, maxHealth);

/*
if (IsImmune)
        {
            return; // Hráč je imunní, nedojde k poškození
        }

        // Zde pokračuje běžná logika pro poškození
        health -= damage;

health = Mathf.Clamp(health, 0, maxHealth);
*/


    
    
    
    
    }

    void HealthBarFiller()
    {
        if (selectedHealthBar != null)
        {
            selectedHealthBar.fillAmount = Mathf.Lerp(selectedHealthBar.fillAmount, health / maxHealth, lerpSpeed);
        }
    }

    void ColorChanger()
    {
        if (selectedHealthBar != null)
        {
            Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
            selectedHealthBar.color = healthColor;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar1;
    public Image healthBar2;
    private Image selectedHealthBar;
    float health, maxHealth = 100;
    float lerpSpeed;

    void Start()
    {
        health = maxHealth;

        if (gameObject.tag == "Player1")
        {
            selectedHealthBar = healthBar1;
        }
        else if (gameObject.tag == "Player2")
        {
            selectedHealthBar = healthBar2;
        }
    
    }

    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
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

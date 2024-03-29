using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    private GameObject potentialPicker;
    public float healthBoost = 100;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null;
        }
    }

    void Update()
    {
        CheckForPowerUpActivation();
    }

    void CheckForPowerUpActivation()
    {
        if (potentialPicker != null)
        {
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                ActivatePowerUp();
            }
        }
    }

    void ActivatePowerUp()
    {
        Health healthComponent = potentialPicker.GetComponent<Health>();
        if (healthComponent != null && healthComponent.health < 100)
        {
            healthComponent.AddHealth(healthBoost);
            Destroy(gameObject);
        }
    }
}
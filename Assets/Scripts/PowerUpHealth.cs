using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    public float healthBoost = 50;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.AddHealth(healthBoost);
                Destroy(gameObject); // Zničí power-up po použití
            }
        }
    }
}

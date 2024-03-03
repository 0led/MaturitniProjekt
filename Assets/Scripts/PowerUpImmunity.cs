using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpImmunity : MonoBehaviour
{
     public float immunityDuration = 5f;
    private GameObject potentialPicker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uloží potenciálního 'picker', který vstoupil do triggeru
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Zruší odkaz na 'picker', pokud opustil trigger
        }
    }

    private void Update()
    {
        if (potentialPicker != null)
        {
            // Zkontroluje, zda hráč stiskl příslušnou klávesu pro sebrání power-upu
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                StartCoroutine(ApplyImmunity(potentialPicker.GetComponent<Health>()));
                potentialPicker = null; // Vyčistí potenciálního 'picker', protože power-up byl sebrán
            }
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth)
    {
        if (playerHealth != null)
        {
            playerHealth.IsImmune = true;

            // Skryje vizuální a kolizní komponenty power-upu
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            yield return new WaitForSeconds(immunityDuration);

            playerHealth.IsImmune = false;
            Destroy(gameObject);
        }
    }
}

/*
    /*
    public float immunityDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                StartCoroutine(ApplyImmunity(playerHealth, collision.tag));
            }

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            playerHealth.IsImmune = true;
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth, string playerTag)
    {
        playerHealth.IsImmune = true;

        yield return new WaitForSeconds(immunityDuration);

        if (playerHealth != null)
        {
            playerHealth.IsImmune = false;
            Destroy(gameObject);
        }
}
}
*/
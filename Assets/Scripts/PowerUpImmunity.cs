using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpImmunity : MonoBehaviour
{
     public float immunityDuration = 5f; // Doba trvání imunity

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                StartCoroutine(ApplyImmunity(playerHealth, collision.tag));
                //Destroy(gameObject); // Zničí power-up objekt ihned po sebrání
            }

            // Deaktivujte vizuální reprezentaci power-upu
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
            //gameObject.SetActive(false);  // Skryje power-up místo jeho zničení
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth, string playerTag)
    {
        playerHealth.IsImmune = true;
        Debug.Log(playerTag + " immunity started, IsImmune: " + playerHealth.IsImmune);

        yield return new WaitForSeconds(immunityDuration);
        Debug.Log(playerTag + " immunity should end now");

        if (playerHealth != null)
        {
            playerHealth.IsImmune = false;
            Debug.Log(playerTag + " immunity ended, IsImmune: " + playerHealth.IsImmune);
        }
        else
    {
        Debug.Log(playerTag + " has been destroyed before immunity could end.");
    }
    
    //Destroy(gameObject);
}
}
  /*
     public float immunityDuration = 5.0f; // Doba trvání imunity
    private GameObject potentialPicker;
    private static Coroutine lastImmunityCoroutine;
    private static Health lastPlayerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    private void Update()
    {
        if (potentialPicker != null && 
            ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
             (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow))))
        {
            // Získání komponenty Health jednou, aby se předešlo opakovanému volání GetComponent<>
            Health playerHealth = potentialPicker.GetComponent<Health>();

            // Zastavíme poslední coroutine, pokud existuje a pokud se týká tohoto hráče
            if (lastImmunityCoroutine != null && lastPlayerHealth == playerHealth)
            {
                StopCoroutine(lastImmunityCoroutine);
            }

            lastImmunityCoroutine = StartCoroutine(ApplyImmunity(playerHealth));
            lastPlayerHealth = playerHealth;
            Destroy(gameObject); // Zničí power-up objekt
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth)
    {
        Debug.Log(playerHealth.gameObject.tag + " immunity started, IsImmune: " + playerHealth.IsImmune);
        playerHealth.IsImmune = true;
        yield return new WaitForSeconds(immunityDuration);

        // Počkejte, zda je playerHealth stále není null
        if (playerHealth != null)
        {
            playerHealth.IsImmune = false;
            Debug.Log(playerHealth.gameObject.tag + " immunity ended, IsImmune: " + playerHealth.IsImmune);
        }
        else
        {
            Debug.Log(playerHealth.gameObject.tag + " Health component not found after waiting.");
        }
    }
}

    /*
    public float immunityDuration = 5.0f; // Doba trvání imunity
    private GameObject potentialPicker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    private void Update()
    {
        if (potentialPicker != null && 
            ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
             (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow))))
        {
            StartCoroutine(ApplyImmunity(potentialPicker));
            Destroy(gameObject); // Zničí power-up objekt
        }
    }

private IEnumerator ApplyImmunity(GameObject player)
{
    Health playerHealth = player.GetComponent<Health>();
    if (playerHealth == null)
    {
        Debug.Log(player.tag + " Health component not found.");
        yield break;
    }

    playerHealth.IsImmune = true;
    Debug.Log(player.tag + " immunity started, IsImmune: " + playerHealth.IsImmune);
    yield return new WaitForSeconds(immunityDuration);

    // Počkejte, zda je playerHealth stále není null
    if (playerHealth != null)
    {
        playerHealth.IsImmune = false;
        Debug.Log(player.tag + " immunity ended, IsImmune: " + playerHealth.IsImmune);
    }
    else
    {
        Debug.Log(player.tag + " Health component not found after waiting.");
    }

}
}
/*
    private IEnumerator ApplyImmunity(GameObject player)
    {
        Debug.Log(player.tag + " immunity started");
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth == null)
        {
             Debug.Log(player.tag + " Health component not found.");
        yield break;
            playerHealth.IsImmune = true; // Nastaví hráče jako imunní

            yield return new WaitForSeconds(immunityDuration); // Počká na uplynutí doby trvání imunity

            if (playerHealth != null)
    {
            playerHealth.IsImmune = false;
            Debug.Log(player.tag + " immunity ended, IsImmune: " + playerHealth.IsImmune);
        }
        else
    {
        Debug.Log(player.tag + " Health component not found after waiting.");
    }
    }
    */
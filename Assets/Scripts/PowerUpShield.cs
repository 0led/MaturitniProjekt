using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    private GameObject potentialPicker;
    public float immunityDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null;
        }
    }

    private void Update()
    {
        CheckForPowerUpActivation();
    }

    private void CheckForPowerUpActivation()
    {
        if (potentialPicker != null)
        {
            PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
            if (playerMovement != null && !playerMovement.isPowerUpActive)
            {
                if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                    (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
                {
                    StartCoroutine(ApplyImmunity(potentialPicker.GetComponent<Health>(), playerMovement));
                }
            }
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth, PlayerMovement playerMovement)
    {
        if (playerHealth != null)
        {
            playerMovement.isPowerUpActive = true;
            playerHealth.IsImmune = true;
            HidePowerUp();

            yield return new WaitForSeconds(immunityDuration);

            playerHealth.IsImmune = false;
            playerMovement.isPowerUpActive = false;
            Destroy(gameObject);
        }
    }

    private void HidePowerUp()
    {
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
    }

}
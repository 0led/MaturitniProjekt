using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBoost : MonoBehaviour
{
    private GameObject potentialPicker;
    public float boostDuration = 5f;
    public float speedBoostAmount = 3f;

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

    void CheckForPowerUpActivation()
    {
        if (potentialPicker != null)
        {
            PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
            if (playerMovement != null && !playerMovement.isPowerUpActive)
            {
                if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                    (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
                {
                    StartCoroutine(ApplySpeedBoost(speedBoostAmount, boostDuration, playerMovement));
                }
            }
        }
    }

    IEnumerator ApplySpeedBoost(float amount, float duration, PlayerMovement playerMovement)
    {
        if (playerMovement != null)
        {
            playerMovement.isPowerUpActive = true;
            playerMovement.moveSpeed += amount;
            HidePowerUp();

            yield return new WaitForSeconds(duration);

            playerMovement.moveSpeed -= amount;
            playerMovement.isPowerUpActive = false;
        }
        Destroy(gameObject);
    }

    void HidePowerUp()
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
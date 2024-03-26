using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBoost : MonoBehaviour
{
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private GameObject potentialPicker;

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
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                ActivatePowerUp();
            }
        }
    }

    void ActivatePowerUp()
    {
        StartCoroutine(ApplySpeedBoost(speedBoostAmount, boostDuration, potentialPicker));
    }

    IEnumerator ApplySpeedBoost(float amount, float duration, GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.moveSpeed += amount;
            HidePowerUp();

            yield return new WaitForSeconds(duration);

            playerMovement.moveSpeed -= amount;
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
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
        if (potentialPicker != null)
        {
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
                if (playerMovement != null && !playerMovement.IsSpeedBoosted)
                {
                    playerMovement.ApplySpeedBoost(speedBoostAmount, boostDuration);
                }
                Destroy(gameObject);
            }
        }
    }
}
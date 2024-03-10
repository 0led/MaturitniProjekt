using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    public float immunityDuration = 5f;
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
                StartCoroutine(ApplyImmunity(potentialPicker.GetComponent<Health>()));
                potentialPicker = null;
            }
        }
    }

    private IEnumerator ApplyImmunity(Health playerHealth)
    {
        if (playerHealth != null)
        {
            playerHealth.IsImmune = true;

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
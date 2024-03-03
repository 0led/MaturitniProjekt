using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpImmunity : MonoBehaviour
{
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
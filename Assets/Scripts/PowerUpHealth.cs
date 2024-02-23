using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    public float healthBoost = 50;
    private GameObject potentialPicker; // Hráč, který má možnost sebrat power-up

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče, který může power-up sebrat
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    void Update()
    {
        if (potentialPicker != null) // Kontrola, zda je nějaký hráč v dosahu power-upu
        {
            // Podmínka pro sebrání power-upu příslušným tlačítkem pro každého hráče
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                Health health = potentialPicker.GetComponent<Health>();
                if (health != null)
                {
                    health.AddHealth(healthBoost); // Přidá zdraví hráči
                    Destroy(gameObject); // Zničí power-up po použití
                }
            }
        }

}
}
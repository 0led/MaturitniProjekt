using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    
    public GameObject weaponPrefab; // Prefab pro AK-47, který bude přiřazen hráči

    private GameObject potentialPicker; // Hráč, který je v dosahu zbraně

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložení hráče do proměnné
        }
    }
        // Funkce volaná, když objekt opustí trigger zónu
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Odebrání hráče z proměnné, když opustí zónu
        }
    }

void Update ()
{
     if (potentialPicker != null) // Kontrola, zda je hráč v dosahu
    {
        PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();

        // Detekce stisku klávesy pro sebrání zbraně
        if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
            (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
        {
            Transform firePoint = potentialPicker.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
            potentialPicker.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, firePoint); // Sebrání zbraně
            gameObject.SetActive(false); // Deaktivace GameObjectu zbraně, aby zmizel ze scény
        }
    }
}
}
    
    /*
    
    if (potentialPicker != null) // Kontrola, zda je hráč v dosahu
        {

            PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();

            // Detekce stisku klávesy pro sebrání zbraně
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                 Transform firePoint = potentialPicker.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
            potentialPicker.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, firePoint); // Sebrání zbraně
            potentialPicker = null; // Resetování proměnné po sebrání zbraně
                //EquipWeaponToPlayer(potentialPicker); // Sebrání zbraně
                //potentialPicker = null; // Resetování proměnné po sebrání zbraně
            }
        }
    */


 /*void EquipWeaponToPlayer(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        bool isFacingRight = player.CompareTag("Player1") ? playerMovement.GetFacingRightP1() : playerMovement.GetFacingRightP2();

        player.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, isFacingRight); // Přiřazení zbraně hráči
        gameObject.SetActive(false); // Deaktivace GameObjectu zbraně, aby zmizel ze scény
    }
    */



/*

         if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            // Získání instance skriptu PlayerMovement z kolizního objektu
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            bool isFacingRight;

            // Zjistěte, zda se jedná o Player1 nebo Player2, a získejte příslušnou hodnotu facingRight
            if (collision.gameObject.CompareTag("Player1"))
            {
                isFacingRight = playerMovement.GetFacingRightP1();
            }
            else // Player2
            {
                isFacingRight = playerMovement.GetFacingRightP2();
            }

            // Předání zbraně hráči s informací o orientaci
            collision.gameObject.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, isFacingRight);

            // Deaktivace GameObjectu zbraně, aby zmizel ze scény
            gameObject.SetActive(false);
        }
    }
    

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")) // Ujistěte se, že vaši hráči mají tag "Player"
        {
            // Předání zbraně hráči
            collision.gameObject.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab);

            // Deaktivace GameObjectu AK-47, aby zmizel ze scény
            gameObject.SetActive(false);
        }
        

   
         if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
    {
        bool isFacingRight = collision.GetComponent<PlayerMovement>().GetFacingRightP1(); // Pro Player1
        if (collision.CompareTag("Player2"))
        {
            isFacingRight = collision.GetComponent<PlayerMovement>().GetFacingRightP2(); // Pro Player2
        }

        collision.gameObject.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, isFacingRight);

        gameObject.SetActive(false); // Deaktivujte objekt zbraně
    }
    
    public GameObject weaponToEquip; // Prefab zbraně k vybavení

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            collision.GetComponent<WeaponEquip>().EquipWeapon(weaponToEquip);
            Destroy(gameObject); // Zničí objekt zbraně na zemi
        }
    }
    

*/

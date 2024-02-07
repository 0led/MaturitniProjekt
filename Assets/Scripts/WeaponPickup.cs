using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    
    public GameObject weaponPrefab; // Prefab pro AK-47, který bude přiřazen hráči

    void OnTriggerEnter2D(Collider2D collision)
    {
       /*
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")) // Ujistěte se, že vaši hráči mají tag "Player"
        {
            // Předání zbraně hráči
            collision.gameObject.GetComponent<WeaponPlayer>().EquipWeapon(weaponPrefab);

            // Deaktivace GameObjectu AK-47, aby zmizel ze scény
            gameObject.SetActive(false);
        }
        */
         if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
    {
        bool isFacingRight = collision.GetComponent<PlayerMovement>().GetFacingRightP1(); // Pro Player1
        if (collision.CompareTag("Player2"))
        {
            isFacingRight = collision.GetComponent<PlayerMovement>().GetFacingRightP2(); // Pro Player2
        }

        collision.gameObject.GetComponent<WeaponPlayer>().EquipWeapon(weaponPrefab, isFacingRight);

        gameObject.SetActive(false); // Deaktivujte objekt zbraně
    }
    }
    

    /*
    public GameObject weaponToEquip; // Prefab zbraně k vybavení

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            collision.GetComponent<WeaponPlayer>().EquipWeapon(weaponToEquip);
            Destroy(gameObject); // Zničí objekt zbraně na zemi
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{ 
    public GameObject weaponPrefab;
    private GameObject potentialPicker;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null;
        }
    }

    void Update()
    {
        if (potentialPicker != null)
        {
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                PickupWeapon();
            }
        }
    }

    public void PickupWeapon()
    {
        if (potentialPicker == null) return;

        PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
        Transform firePoint = potentialPicker.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
        WeaponConfig weaponConfigToUse = weaponPrefab.GetComponent<Weapon>().weaponConfig;
        GameObject newWeapon = potentialPicker.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, firePoint, weaponConfigToUse);
        Weapon weaponScript = newWeapon.GetComponent<Weapon>();

        if (weaponScript != null)
        {
            weaponScript.SetAmmo(weaponConfigToUse.ammo);
        }

        Destroy(gameObject);
    }
}
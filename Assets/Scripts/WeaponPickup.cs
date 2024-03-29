using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{ 
    private GameObject potentialPicker;
    public GameObject weaponPrefab;

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

    private void Update()
    {
        CheckInputAndPickupWeapon();
    }

    private void CheckInputAndPickupWeapon()
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

    private void PickupWeapon()
    {
        if (potentialPicker == null)
        {
        return;
        }

        AssignWeaponToPicker();
    }

    private void AssignWeaponToPicker()
    {
        PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
        Transform firePoint = DetermineFirePoint(playerMovement);
        GameObject newWeapon = EquipWeapon(firePoint);
        SetWeaponAmmo(newWeapon);

        Destroy(gameObject);
    }

    private Transform DetermineFirePoint(PlayerMovement playerMovement)
    {
        return potentialPicker.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
    }

    private GameObject EquipWeapon(Transform firePoint)
    {
        WeaponConfig weaponConfigToUse = weaponPrefab.GetComponent<Weapon>().weaponConfig;
        return potentialPicker.GetComponent<WeaponEquip>().EquipWeapon(weaponPrefab, firePoint, weaponConfigToUse);
    }

    private void SetWeaponAmmo(GameObject newWeapon)
    {
        Weapon weaponScript = newWeapon.GetComponent<Weapon>();
        if (weaponScript != null)
        {
            WeaponConfig weaponConfigToUse = weaponPrefab.GetComponent<Weapon>().weaponConfig;
            weaponScript.SetAmmo(weaponConfigToUse.ammo);
        }
    }

}
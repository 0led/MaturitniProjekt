using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public Transform weaponHolder;

    public GameObject EquipWeapon(GameObject weaponPrefab, Transform firePoint, WeaponConfig newWeaponConfig)
    {
        ClearExistingWeapons();
        GameObject weaponInstance = InstantiateWeapon(weaponPrefab);
        ConfigureWeapon(weaponInstance, newWeaponConfig, firePoint);
        return weaponInstance;
    }

    private void ClearExistingWeapons()
    {
        foreach (Transform child in weaponHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private GameObject InstantiateWeapon(GameObject weaponPrefab)
    {
        GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;
        return weaponInstance;
    }

    private void ConfigureWeapon(GameObject weaponInstance, WeaponConfig newWeaponConfig, Transform firePoint)
    {
        Weapon newWeaponScript = weaponInstance.GetComponent<Weapon>();
        if (newWeaponScript != null)
        {
            newWeaponScript.enabled = true;
            newWeaponScript.SetWeaponConfig(newWeaponConfig);
            newWeaponScript.currentAmmo = newWeaponConfig.ammo;
            newWeaponScript.UpdateAmmoText();

            PlayerMovement playerMovement = weaponHolder.GetComponentInParent<PlayerMovement>();
            Transform playerFirePoint = DeterminePlayerFirePoint(playerMovement);
            newWeaponScript.SetFirePoint(playerFirePoint);
            SetPlayerIdentifier(newWeaponScript, weaponHolder);
        }
    }

    private Transform DeterminePlayerFirePoint(PlayerMovement playerMovement)
    {
        return weaponHolder.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
    }

    private void SetPlayerIdentifier(Weapon weaponScript, Transform weaponHolder)
    {
        if (weaponHolder.CompareTag("Player1")) {
            weaponScript.SetPlayerIdentifier(1);
        } 
        else if (weaponHolder.CompareTag("Player2")) {
            weaponScript.SetPlayerIdentifier(2);
        }
    }
}
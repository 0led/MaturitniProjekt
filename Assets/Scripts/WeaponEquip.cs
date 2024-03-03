using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public Transform weaponHolder;

    public GameObject EquipWeapon(GameObject weaponPrefab, Transform firePoint, WeaponConfig newWeaponConfig)
    {
    foreach (Transform child in weaponHolder)
    {
        Destroy(child.gameObject);
    }

    GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
    weaponInstance.transform.localPosition = Vector3.zero;
    weaponInstance.transform.localRotation = Quaternion.identity;
    Weapon newWeaponScript = weaponInstance.GetComponent<Weapon>();
    
    if (newWeaponScript != null)
    {
        newWeaponScript.enabled = true;
        newWeaponScript.SetWeaponConfig(newWeaponConfig);
        
        PlayerMovement playerMovement = weaponHolder.GetComponentInParent<PlayerMovement>();
        Transform playerFirePoint = weaponHolder.CompareTag("Player1") ? playerMovement.FirePoint1 : playerMovement.FirePoint2;
        newWeaponScript.SetFirePoint(playerFirePoint);
        
        if (weaponHolder.CompareTag("Player1")) {
            newWeaponScript.SetPlayerIdentifier(1);
        } 
        else if (weaponHolder.CompareTag("Player2")) {
            newWeaponScript.SetPlayerIdentifier(2);
        } 
    }

    if (newWeaponScript != null)
    {
        newWeaponScript.SetWeaponConfig(newWeaponConfig);
        newWeaponScript.currentAmmo = newWeaponConfig.ammo;
        newWeaponScript.UpdateAmmoText();
    }
    
    return weaponInstance;
   
    }
}
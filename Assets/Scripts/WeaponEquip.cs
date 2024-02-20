using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public Transform weaponHolder;

    public GameObject EquipWeapon(GameObject weaponPrefab, Transform firePoint, WeaponConfig newWeaponConfig)
    {
    // Odstranění stávající zbraně a deaktivace skriptu Weapon
    foreach (Transform child in weaponHolder)
    {
      /*
       Weapon oldWeaponScript = child.GetComponent<Weapon>();
        if (oldWeaponScript != null)
        {
            oldWeaponScript.enabled = false; // Deaktivujeme skript
        }
        */
        Destroy(child.gameObject);
    }

    // Vytvoření instance nové zbraně a aktivace skriptu Weapon
    GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
    weaponInstance.transform.localEulerAngles = firePoint.localEulerAngles;

    // Aktivace skriptu Weapon na nově vybavené zbrani
    Weapon newWeaponScript = weaponInstance.GetComponent<Weapon>();
    if (newWeaponScript != null)
    {
        newWeaponScript.enabled = true; // Aktivujeme skript
        newWeaponScript.SetWeaponConfig(newWeaponConfig); // Aktualizujte WeaponConfig
    }

 return weaponInstance;
}
}
  /*
    public void EquipWeapon(GameObject weaponPrefab, Transform firePoint)
{
    foreach (Transform child in weaponHolder)
    {
        Destroy(child.gameObject);  // Odstranění stávající zbraně
    }

    GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
    weaponInstance.transform.localEulerAngles = firePoint.localEulerAngles;

    WeaponConfig weaponConfig = weaponPrefab.GetComponent<Weapon>().weaponConfig; // Načtení WeaponConfig z prefabu
    Weapon weaponScript = weaponInstance.GetComponent<Weapon>();
    if (weaponScript != null)
    {
        weaponScript.SetWeaponConfig(weaponConfig);  // Nastavení WeaponConfig pro instanci zbraně
    }
    else
    {
        Debug.LogError("Weapon script not found on the instantiated weapon object.");
    }
}
*/
    /*
    public void EquipWeapon(GameObject weaponPrefab, Transform firePoint)
    {
        foreach (Transform child in weaponHolder) {
            Destroy(child.gameObject);
        }

        GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        weaponInstance.transform.localEulerAngles = firePoint.localEulerAngles;

        Debug.Log("FirePoint LocalEulerAngles: " + firePoint.localEulerAngles);
        Debug.Log("Weapon Instance LocalEulerAngles: " + weaponInstance.transform.localEulerAngles);
    }
    */
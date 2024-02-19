using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public Transform weaponHolder;

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
}
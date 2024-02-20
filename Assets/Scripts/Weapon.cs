using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    private string fireButton1;
    private string fireButton2;
    //public WeaponConfig weaponConfigP1;
    //public WeaponConfig weaponConfigP2;
    public WeaponConfig weaponConfig;
    public bool isStartingWeapon = false;

    void Start()
    {

         if (!isStartingWeapon)
    {
        this.enabled = false; // Deaktivujte skript Weapon, pokud to není startovní zbraň
    }
        //this.enabled = false; // Deaktivuje skript Weapon, když hra začíná
    }

    public void SetWeaponConfig(WeaponConfig newConfig)
{
    //newWeaponScript.SetWeaponConfig(newWeaponConfig); // Aktualizujte WeaponConfig
    weaponConfig = newConfig;
    
    Debug.Log("New weapon config set with damage: " + weaponConfig.damage); // Mělo by ukázat správnou hodnotu poškození
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(firePoint1, weaponConfig);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot(firePoint2, weaponConfig);
        }
    }

    void Shoot(Transform firePoint, WeaponConfig weaponConfig)
    {
     //Debug.Log("Shooting with damage: " + weaponConfig.damage); // Kontrola hodnoty damage

    GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Bullet bulletScript = bulletObject.GetComponent<Bullet>();

    if (bulletScript != null)
    {
        bulletScript.Initialize(weaponConfig); // Inicializujeme Bullet s hodnotami z WeaponConfig
        
        Debug.Log("Shooting with damage: " + weaponConfig.damage); // Mělo by ukázat správnou hodnotu poškození
    }
    else
    {
        
        Debug.LogError("Bullet script not found on the instantiated bullet object.");
    }
}
}
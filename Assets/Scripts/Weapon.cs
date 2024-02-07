using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //public Transform firePoint;
    public Transform firePoint1; // FirePoint pro Player1
    public Transform firePoint2; // FirePoint pro Player2
    public GameObject bulletPrefab;
    private string fireButton1;
    private string fireButton2;
    //public WeaponConfig weaponConfig; // Reference na ScriptableObject konfiguraci
    //public GameObject player1GunSprite; // Reference na vizuální reprezentaci pistole pro Player1
    //public GameObject player2GunSprite; // Reference na vizuální reprezentaci pistole pro Player2
    public WeaponConfig weaponConfigP1; // Konfigurace zbraně pro hráče 1
    public WeaponConfig weaponConfigP2; // Konfigurace zbraně pro hráče 2

    void Start()
    {
   /*     
{
    firePoint = transform.Find("firePoint1"); // Předpokládá, že FirePoint1 je dítěm hráče
    if (firePoint == null) {
        // Zkuste najít FirePoint2, pokud FirePoint1 neexistuje
        firePoint = transform.Find("firePoint2");
    }
}
      */  
    
        //fireButton1 = "Fire1"; // Klávesa pro střelbu hráče 1, např. KeyCode.Space.ToString()
        // fireButton2 = "Fire2"; // Klávesa pro střelbu hráče 2, např. KeyCode.Return.ToString()  
    }

    void Update()
    {   
        /*
        if (Input.GetButtonDown(fireButton))
        {
            Shoot();
        }
        */
         // Kontrola, zda hráč 1 stiskl klávesu pro střelbu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(firePoint1);
        }

        // Kontrola, zda hráč 2 stiskl klávesu pro střelbu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot(firePoint2);
        }

    }

void Shoot(Transform firePoint) {
     Debug.Log("Shooting from: " + firePoint.name + " at time: " + Time.time);

    // Rozlišení, která konfigurace zbraně se má použít na základě firePoint
    WeaponConfig currentConfig = firePoint == firePoint1 ? weaponConfigP1 : weaponConfigP2;

    if (currentConfig != null && currentConfig.fireRate > 0 && currentConfig.damage > 0) {
        // Vytvoření instance střely
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Získání skriptu Bullet z nově vytvořeného objektu
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null) {
            // Nastavení vlastností střely z aktuální konfigurace zbraně
            bullet.damage = currentConfig.damage;
            bullet.speed = currentConfig.fireRate; // Možná budete chtít mít pro rychlost střely samostatnou proměnnou v WeaponConfig
            bullet.range = currentConfig.range;
        } else {
            Debug.LogError("Bullet component not found on the instantiated bullet object.");
        }
    } else {
        Debug.Log("Weapon configuration is null or has zero fireRate or damage, cannot shoot.");
    }
}
  /*  void Shoot(Transform firePoint) {
        //shooting logic
        WeaponConfig currentConfig = firePoint == firePoint1 ? weaponConfigP1 : weaponConfigP2;
    if (currentConfig.fireRate > 0 && currentConfig.damage > 0) {
        // Vytvoření a nastavení střely

    

        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Získání skriptu Bullet z nově vytvořeného objektu
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            // Nastavení vlastností střely z WeaponConfig
            bullet.damage = weaponConfig.damage;
            bullet.speed = weaponConfig.fireRate; // Ujistěte se, že skript Bullet má definovanou vlastnost speed
            bullet.range = weaponConfig.range; // Nastavení dosahu střely podle konfigurace zbraně
        }
        else
        {
            Debug.LogError("Bullet component not found on the instantiated bullet object.");
        }
    }
    }
        // Nastavení vlastností střely z WeaponConfig
       // bulletPrefab.damage = weaponConfig.damage;
        //bulletPrefab.speed = weaponConfig.fireRate; // Možná budete chtít použít jinou proměnnou pro rychlost střely

        // Další logika střelby, pokud je potřeba
        */
}

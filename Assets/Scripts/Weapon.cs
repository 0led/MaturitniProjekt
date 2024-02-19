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
    public WeaponConfig weaponConfigP1;
    public WeaponConfig weaponConfigP2;
    private WeaponConfig weaponConfig;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(firePoint1, weaponConfigP1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot(firePoint2, weaponConfigP2);
        }
    }

    void Shoot(Transform firePoint, WeaponConfig weaponConfig)
    {
    GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Bullet bulletScript = bulletObject.GetComponent<Bullet>();

    if (bulletScript != null)
    {
        bulletScript.Initialize(weaponConfig); // Inicializujeme Bullet s hodnotami z WeaponConfig
    }
    else
    {
        Debug.LogError("Bullet script not found on the instantiated bullet object.");
    }
}
}
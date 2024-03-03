using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private string fireButton1;
    private string fireButton2;
    public WeaponConfig weaponConfig;
    public bool isStartingWeapon = false;
    private int playerIdentifier;

    void Start()
    {
        if (isStartingWeapon)
        {
        Transform weaponHolder = transform.parent;

        if (weaponHolder.parent.CompareTag("Player1"))
        {
            SetPlayerIdentifier(1);
            firePoint = weaponHolder.parent.Find("FirePoint1");
        }
        else if (weaponHolder.parent.CompareTag("Player2"))
        {
            SetPlayerIdentifier(2);
            firePoint = weaponHolder.parent.Find("FirePoint2");
        }
    }
    }
       
    public void SetPlayerIdentifier(int identifier)
    {
        playerIdentifier = identifier;
    }

    public void SetWeaponConfig(WeaponConfig newConfig)
    {
        weaponConfig = newConfig;
    }

      public void SetFirePoint(Transform newFirePoint)
    {
        if (newFirePoint != null)
    {
        firePoint = newFirePoint;
    }
    }

    void Update()
    {
   
        if (playerIdentifier == 1 && Input.GetKeyDown(KeyCode.Space))
        {
        if (firePoint != null)
        {
            Shoot(weaponConfig);
        }
        }

        else if (playerIdentifier == 2 && Input.GetKeyDown(KeyCode.Return))
        {
        if (firePoint != null)
        {
            Shoot(weaponConfig);
        }
        }
    }
      
    void Shoot(WeaponConfig weaponConfig)
    {

        if (firePoint == null)
        {
            return;
        }

        if (firePoint != null){
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bulletObject.GetComponent<Bullet>();
    
        if (bulletScript != null)
        {
            bulletScript.Initialize(weaponConfig);
    
            Collider2D playerCollider = transform.parent.parent.GetComponent<Collider2D>();
            Collider2D bulletCollider = bulletObject.GetComponent<Collider2D>();
        
        if (playerCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);
        }
        }
    }
}
}
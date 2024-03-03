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
    private PlayerMovement playerMovement;

    public int currentAmmo;

    void Start()
    {
        currentAmmo = weaponConfig.ammo;

        playerMovement = GetComponentInParent<PlayerMovement>();

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

    UpdateAmmoText();

    }

    public void SetAmmo(int ammo)
    {
        currentAmmo = ammo;
        UpdateAmmoText();
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
        {
        if ((playerIdentifier == 1 && Input.GetKeyDown(KeyCode.Space)) || 
            (playerIdentifier == 2 && Input.GetKeyDown(KeyCode.Return)))
        {
            Shoot(weaponConfig);
        }
    }
    }
      
    void Shoot(WeaponConfig weaponConfig)
    {
        if (currentAmmo <= 0)
        {
            return;
        }

        currentAmmo--;
        UpdateAmmoText();

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

    public void UpdateAmmoText()
    {
        if (playerMovement != null)
        {
            playerMovement.UpdateAmmoText(currentAmmo, playerIdentifier);
        }
    }
}
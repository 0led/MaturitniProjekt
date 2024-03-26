using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public WeaponConfig weaponConfig;
    public bool isStartingWeapon = false;
    public int currentAmmo;
    private int playerIdentifier;
    private PlayerMovement playerMovement;

    void Start()
    {
        InitializeWeapon();
        UpdateAmmoText();
    }

    void Update()
    {
        HandleFiring();
    }

    void InitializeWeapon()
    {
        currentAmmo = weaponConfig.ammo;
        playerMovement = GetComponentInParent<PlayerMovement>();

        if (isStartingWeapon)
        {
            Transform weaponHolder = transform.parent;
            SetPlayerIdentifierBasedOnParentTag(weaponHolder.parent);
        }
    }

    void HandleFiring()
    {
        if (!GameStarter.GameHasStarted)
            return;

        if ((playerIdentifier == 1 && Input.GetKeyDown(KeyCode.Space)) || 
            (playerIdentifier == 2 && Input.GetKeyDown(KeyCode.Return)))
        {
            Shoot();
        }
    }

    void SetPlayerIdentifierBasedOnParentTag(Transform parent)
    {
        if (parent.CompareTag("Player1"))
        {
            SetPlayerIdentifier(1);
            firePoint = parent.Find("FirePoint1");
        }
        else if (parent.CompareTag("Player2"))
        {
            SetPlayerIdentifier(2);
            firePoint = parent.Find("FirePoint2");
        }
    }

    public void UpdateAmmoText()
    {
        if (playerMovement != null)
        {
            playerMovement.UpdateAmmoText(currentAmmo, playerIdentifier);
        }
    }

    public void SetAmmo(int ammo)
    {
        currentAmmo = ammo;
        UpdateAmmoText();
    }

    public void SetWeaponConfig(WeaponConfig newConfig)
    {
        weaponConfig = newConfig;
    }

    public void SetPlayerIdentifier(int identifier)
    {
        playerIdentifier = identifier;
    }

    public void SetFirePoint(Transform newFirePoint)
    {
        firePoint = newFirePoint ? newFirePoint : firePoint;
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;
        UpdateAmmoText();
        CreateBullet();
    }

    void CreateBullet()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
    
        if (bulletScript != null)
        {
            bulletScript.Initialize(weaponConfig);
            IgnoreCollisionWithPlayer(bulletObject);
        }
    }

    void IgnoreCollisionWithPlayer(GameObject bulletObject)
    {
        Collider2D playerCollider = transform.parent.parent.GetComponent<Collider2D>();
        Collider2D bulletCollider = bulletObject.GetComponent<Collider2D>();

        if (playerCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);
        }
    }
}
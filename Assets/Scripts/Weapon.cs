using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private string fireButton;

    void Start()
    {
        // Nastavit tlačítko střelby podle tagu hráče
        if (gameObject.tag == "Player1")
        {
            fireButton = "Fire1"; // Předpokládá, že máte vstup s názvem "Fire1" pro hráče 1
        }
        else if (gameObject.tag == "Player2")
        {
            fireButton = "Fire2"; // Předpokládá, že máte vstup s názvem "Fire2" pro hráče 2
        }
    }

    void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            Shoot();
        }
    }

    void Shoot() {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
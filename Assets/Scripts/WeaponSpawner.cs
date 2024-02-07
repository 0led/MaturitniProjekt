using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weaponPrefab; // Prefab pro AK-47
    public Transform[] spawnPoints; // Pole bodů spawnu

    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        yield return new WaitForSeconds(5); // Počkejte 5 sekund před spawnováním zbraně

        // Vyberte náhodný spawn bod
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // Instancujte zbraň na vybraném spawn bodě
        Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

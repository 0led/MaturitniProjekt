using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject ARPrefab;
    public GameObject SMGPrefab;
    private GameObject player1;
    private GameObject player2;
    public float minimumDistance = 20f;
    public float spawnDelay = 2f;
    public float destroyDelay = 5f;
    public GameObject[] spawnPoints; // Pole pro uchování odkazů na spawn body
      private bool[] isSpawnPointOccupied; // Přidáno pro sledování obsazenosti
    //public GameObject weaponPrefab; // Prefab pro AK-47
    //public Transform[] spawnPoints; // Pole bodů spawnu

    private void Start()
    {
       player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        isSpawnPointOccupied = new bool[spawnPoints.Length]; // Inicializace pole
        StartCoroutine(SpawnWeaponAfterDelay());
    }

    private IEnumerator SpawnWeaponAfterDelay()
    {
        while (true)
        {
        yield return new WaitForSeconds(spawnDelay); // Počkejte 5 sekund před spawnováním zbraně

        var availableSpawnPoints = spawnPoints
                .Select((point, index) => new { Point = point, Index = index })
                .Where(sp => !isSpawnPointOccupied[sp.Index])
                .ToList();

        if (availableSpawnPoints.Count > 0)
            {
                var spawnInfo = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                GameObject weaponToSpawn = Random.Range(0, 2) == 0 ? ARPrefab : SMGPrefab;
                Vector2 spawnPosition = spawnInfo.Point.transform.position;
                GameObject spawnedWeapon = Instantiate(weaponToSpawn, spawnPosition, Quaternion.identity);
                isSpawnPointOccupied[spawnInfo.Index] = true; // Označíme spawn point jako obsazený

                // Po 7 sekundách zničíme zbraň a uvolníme spawn point
                Destroy(spawnedWeapon, destroyDelay);
                StartCoroutine(ReleaseSpawnPoint(spawnInfo.Index, destroyDelay));
            }

        // Logika pro výběr a spawnování zbraně
        //GameObject weaponToSpawn = Random.Range(0, 2) == 0 ? ARPrefab : SMGPrefab;
        //Vector2 spawnPosition = CalculateSpawnPosition();
        //GameObject spawnedWeapon = Instantiate(weaponToSpawn, spawnPosition, Quaternion.identity);

        //Destroy(spawnedWeapon, destroyDelay);
        // Vyberte náhodný spawn bod
       // int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[spawnPointIndex];

        // Instancujte zbraň na vybraném spawn bodě
        //Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    
    private IEnumerator ReleaseSpawnPoint(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpawnPointOccupied[index] = false; // Po zničení zbraně uvolníme spawn point
    }

    private Vector2 CalculateSpawnPosition()
    {
        // Filtrujte spawn body na základě jejich vzdálenosti od hráčů
        var validSpawnPoints = spawnPoints.Where(spawnPoint => 
        (spawnPoint.transform.position - player1.transform.position).magnitude > minimumDistance &&
        (spawnPoint.transform.position - player2.transform.position).magnitude > minimumDistance).ToArray();
        
        // Vyberte náhodný spawn bod z filtru validních
    if (validSpawnPoints.Length > 0)
    {
        int randomIndex = Random.Range(0, validSpawnPoints.Length);
        return validSpawnPoints[randomIndex].transform.position;
    }
    else
    {
        // Záložní plán, pokud nejsou k dispozici žádné validní spawn body
        return Vector2.zero; // Můžete zvolit lepší záložní strategii
    }
        // Implementujte logiku pro výpočet spravedlivé spawnovací pozice zde
        // Zjednodušený příklad: výpočet středního bodu a přidání náhodného posunu
        //Vector2 midpoint = (player1.transform.position + player2.transform.position) / 2;
        //Vector2 randomOffset = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        //return midpoint + randomOffset;
    }
}

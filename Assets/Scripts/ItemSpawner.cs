using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
     public GameObject ARPrefab;
    public GameObject SMGPrefab;
    private GameObject player1;
    private GameObject player2;
    public float minimumDistance = 20f;
    public float spawnDelay = 10f;
    public float destroyDelay = 10f;
    public GameObject[] spawnPoints;
    public GameObject[] powerUpPrefabs;
    private bool[] isSpawnPointOccupied;
    private bool spawnWeaponNext = true;
    private GameObject[] spawnedObjects;

    private void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        isSpawnPointOccupied = new bool[spawnPoints.Length];
        spawnedObjects = new GameObject[spawnPoints.Length]; // Inicializace pole
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Vytvořit seznam dostupných spawnpointů
            var availableSpawnPoints = spawnPoints
                .Select((point, index) => new { Point = point, Index = index })
                .Where(sp => !isSpawnPointOccupied[sp.Index] && 
                             Vector3.Distance(sp.Point.transform.position, player1.transform.position) > minimumDistance &&
                             Vector3.Distance(sp.Point.transform.position, player2.transform.position) > minimumDistance)
                .ToList();

            if (availableSpawnPoints.Count > 0)
            {
                var spawnInfo = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                isSpawnPointOccupied[spawnInfo.Index] = true;
                GameObject itemToSpawn = spawnWeaponNext ? ChooseWeapon() : ChoosePowerUp();
                Vector2 spawnPosition = spawnInfo.Point.transform.position;
                GameObject spawnedItem = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
                spawnedObjects[spawnInfo.Index] = spawnedItem; // Uložení reference na vytvořený objekt

                // Nastavit timer pro uvolnění spawnpointu a zničení itemu
                StartCoroutine(ReleaseSpawnPoint(spawnInfo.Index, destroyDelay));

                // Přepněte na druhý typ položky pro další spawn
                spawnWeaponNext = !spawnWeaponNext;
            }
            else
            {
                // Pokud nejsou dostupné žádné spawnpointy, čekejte další cyklus
                continue;
            }
        }
    }
    
    private IEnumerator ReleaseSpawnPoint(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
          GameObject obj = spawnedObjects[index];
        if (obj != null && obj.activeInHierarchy)
    {
        // Před zničením zkontrolujte, zda objekt není skrytý immunity power-up
        if (!obj.CompareTag("ImmunityPowerUp"))  // Předpokládá, že máte nastavený specifický tag pro immunity power-up
        {
            Destroy(obj);
        }
    }
    isSpawnPointOccupied[index] = false;
        /*
        if (spawnedObjects[index] != null)
    {
        Destroy(spawnedObjects[index]); // Zničení objektu
    }
        isSpawnPointOccupied[index] = false;
        // Tady by měla být logika pro zničení objektu, pokud existuje
        */
    }

    private GameObject ChooseWeapon()
    {
        // Logika pro výběr a vrácení prefabu zbraně
        return Random.Range(0, 2) == 0 ? ARPrefab : SMGPrefab;
    }

    private GameObject ChoosePowerUp()
    {
        // Logika pro výběr a vrácení prefabu power-upu
        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        return powerUpPrefabs[powerUpIndex];
    }
}
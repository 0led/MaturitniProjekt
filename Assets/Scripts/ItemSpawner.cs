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
    public float spawnDelay = 2f;
    public float destroyDelay = 5f;
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
        spawnedObjects = new GameObject[spawnPoints.Length];
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

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
                spawnedObjects[spawnInfo.Index] = spawnedItem;

                StartCoroutine(ReleaseSpawnPoint(spawnInfo.Index, destroyDelay));

                spawnWeaponNext = !spawnWeaponNext;
            }
            else
            {
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
        if (!obj.CompareTag("ImmunityPowerUp"))
        {
            Destroy(obj);
        }
    }
        isSpawnPointOccupied[index] = false;  
    }

    private GameObject ChooseWeapon()
    {
        return Random.Range(0, 2) == 0 ? ARPrefab : SMGPrefab;
    }

    private GameObject ChoosePowerUp()
    {
        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        return powerUpPrefabs[powerUpIndex];
    }
}
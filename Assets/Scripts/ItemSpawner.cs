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
    public float minimumDistance = 7f;
    public float spawnDelay = 4f;
    public float destroyDelay = 7f;
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

        var shuffledAvailableSpawnPoints = spawnPoints
            .Select((point, index) => new { Point = point, Index = index })
            .Where(sp => !isSpawnPointOccupied[sp.Index] &&
                         Vector3.Distance(sp.Point.transform.position, player1.transform.position) > minimumDistance &&
                         Vector3.Distance(sp.Point.transform.position, player2.transform.position) > minimumDistance)
            .OrderBy(_ => Random.value)
            .ToList();

    if (shuffledAvailableSpawnPoints.Count > 0)
        {
            var spawnInfo = shuffledAvailableSpawnPoints[0];
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

    private bool IsSpawnPointValid(Vector3 spawnPointPosition)
    {   
        return Vector3.Distance(spawnPointPosition, player1.transform.position) > minimumDistance &&
               Vector3.Distance(spawnPointPosition, player2.transform.position) > minimumDistance; 
    }
  
    private IEnumerator ReleaseSpawnPoint(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
          GameObject obj = spawnedObjects[index];
        if (obj != null && obj.activeInHierarchy)
    {
        if (obj.CompareTag("ImmunityPowerUp"))
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            
            StartCoroutine(RemoveAfterDelay(obj, 7f));
        }
        else
        {
            Destroy(obj);
        }
        
    }
        isSpawnPointOccupied[index] = false;  
    }

    private IEnumerator RemoveAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
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

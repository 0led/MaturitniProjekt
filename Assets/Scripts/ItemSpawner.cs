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

    private void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        isSpawnPointOccupied = new bool[spawnPoints.Length];
        StartCoroutine(SpawnWeaponAfterDelay());
    }

    private IEnumerator SpawnWeaponAfterDelay()
    {
        while (true)
        {
        yield return new WaitForSeconds(spawnDelay);

        var availableSpawnPoints = spawnPoints
                .Select((point, index) => new { Point = point, Index = index })
                .Where(sp => !isSpawnPointOccupied[sp.Index])
                .ToList();

        if (availableSpawnPoints.Count > 0)
            {
                var spawnInfo = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                //GameObject weaponToSpawn = Random.Range(0, 2) == 0 ? ARPrefab : SMGPrefab;
                GameObject itemToSpawn = spawnWeaponNext ? ChooseWeapon() : ChoosePowerUp();
                Vector2 spawnPosition = spawnInfo.Point.transform.position;
                //GameObject spawnedWeapon = Instantiate(weaponToSpawn, spawnPosition, Quaternion.identity);
                //Debug.Log("Spawning power-up at position (before Instantiate):" + spawnInfo.Point.transform.position);
                GameObject spawnedItem = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
                //Debug.Log("Spawning power-up at position (after Instantiate):" + spawnedItem.transform.position);
                //Debug.Break();
                //spawnedItem.GetComponent<Weapon>().enabled = false;
                
                isSpawnPointOccupied[spawnInfo.Index] = true;
                Destroy(spawnedItem, destroyDelay);
                StartCoroutine(ReleaseSpawnPoint(spawnInfo.Index, destroyDelay));

                spawnWeaponNext = !spawnWeaponNext; // Přepněte na druhý typ položky pro další spawn
            }
        }
    }
    
    private IEnumerator ReleaseSpawnPoint(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpawnPointOccupied[index] = false;
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

    private Vector2 CalculateSpawnPosition()
    {
        var validSpawnPoints = spawnPoints.Where(spawnPoint => 
        (spawnPoint.transform.position - player1.transform.position).magnitude > minimumDistance &&
        (spawnPoint.transform.position - player2.transform.position).magnitude > minimumDistance).ToArray();
        
        if (validSpawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, validSpawnPoints.Length);
            return validSpawnPoints[randomIndex].transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }
}

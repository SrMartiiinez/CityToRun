using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour {

    public static PowerUpSpawnManager instance;

    public List<GameObject> spawnPowerUps = new List<GameObject>();
    public Transform playerPos;
    public List<GameObject> defaultList = new List<GameObject>();
    public float spawnDelay = 15f;
    public float spawnDelayExtra = 15f;
    [Range(0, 100)]
    public int extraDelaySpawnProbability = 50;

    [HideInInspector] public bool canSpawn = true;

    [HideInInspector] public GameObject[] spawnPoints;
    private GameObject go;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        defaultList = new List<GameObject>(spawnPowerUps);
    }

    public void SpawnPowerUpV2(Transform[] tileSpawnPoints)
    {
        if (canSpawn)
        {
            int index = Random.Range(0, spawnPowerUps.Count);
           // Debug.Log($"Count: {spawnPowerUps.Count} - {index}");
            go = Instantiate(spawnPowerUps[index], tileSpawnPoints[Random.Range(0, tileSpawnPoints.Length)]);
           // Debug.Log("Spawned " + go.name);
            CheckSpawn(index);
        }
    }

    public void CheckSpawn(int index)
    {
        spawnPowerUps.RemoveAt(index);

        if (spawnPowerUps.Count == 0)
        {
            Debug.Log("Reset list");
            spawnPowerUps = new List<GameObject>(defaultList);
        }

        StartCoroutine(SpawnerPUCooldownCo());
    }

    IEnumerator SpawnerPUCooldownCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);

        if (Random.Range(0, 101) < extraDelaySpawnProbability)
        {
            yield return new WaitForSeconds(spawnDelayExtra);
            
            canSpawn = true;
        }
        else
        {
            
            canSpawn = true;
        }
    }
}


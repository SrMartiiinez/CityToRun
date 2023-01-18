using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnPoint : MonoBehaviour {

    private GameObject go;
    private PowerUpSpawnManager spawnManager;
    private bool started;
    public Transform[] tileSpawnPoints;

    private void OnEnable()
    {
        if (started)
        {
            /*if (spawnManager.canSpawn) {
                int index = Random.Range(0, spawnManager.spawnPowerUps.Count);
                go = Instantiate(spawnManager.spawnPowerUps[index], transform);
                spawnManager.CheckSpawn(index);
            }*/
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (spawnManager == null)
        {
            spawnManager = PowerUpSpawnManager.instance;
        }
        if (!started)
        {
            /*int index = Random.Range(0, spawnManager.spawnPowerUps.Count);
            go = Instantiate(spawnManager.spawnPowerUps[index], transform);
            spawnManager.CheckSpawn(index);*/
            started = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnManager.SpawnPowerUpV2(tileSpawnPoints);

        }
    }
}

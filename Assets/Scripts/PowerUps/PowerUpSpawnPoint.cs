using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Administra todos los puntos del prefab en los que se instanciarán los powerUps.
 */

public class PowerUpSpawnPoint : MonoBehaviour {


    private PowerUpSpawnManager spawnManager;
    private bool started;
    public Transform[] tileSpawnPoints; //Todos los puntos en los que se generarán los poweUps

    // Start is called before the first frame update
    void Start()
    {
        if (spawnManager == null)
        {
            spawnManager = PowerUpSpawnManager.instance;
        }
        if (!started)
        {
            started = true;
        }
    }

    //Este trigger está colocado un tile por delante del prefab actual. Al llegar a él, se instancia un powerUp en este prefab, el cual está a un tile del jugador.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnManager.SpawnPowerUpV2(tileSpawnPoints);

        }
    }
}

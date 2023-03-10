using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Administra todos los puntos del prefab en los que se instanciar?n los powerUps.
 */

public class PowerUpSpawnPoint : MonoBehaviour {


    private PowerUpSpawnManager spawnManager;
    private bool started;
    public Transform[] tileSpawnPoints; //Todos los puntos en los que se generar?n los poweUps

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

    //Este trigger est? colocado un tile por delante del prefab actual. Al llegar a ?l, se instancia un powerUp en este prefab, el cual est? a un tile del jugador.
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos que se han asignado los puntos de spawn y que el jugador ha entrado en el trigger.
        if (other.CompareTag("Player") && 
            (tileSpawnPoints != null && tileSpawnPoints.Length != 0 && tileSpawnPoints[0] != null))
        {
            spawnManager.SpawnPowerUpV2(tileSpawnPoints);
            //Debug.Log($"SpawnPoint {tileSpawnPoints[0].name} - {tileSpawnPoints[0].position}");
        }
    }
}

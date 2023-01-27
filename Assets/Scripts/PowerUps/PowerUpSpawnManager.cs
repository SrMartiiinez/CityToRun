using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Administra el spawn de los powerUps.
/// </summary>

public class PowerUpSpawnManager : MonoBehaviour {

    public static PowerUpSpawnManager instance;

    public List<GameObject> spawnPowerUps = new List<GameObject>(); //PowerUps que se van a instanciar.
    public Transform playerPos;
    //Guarda el estado inicial de la lista SpawnPowerUps para poder restaurar la lista anterioir cuando ya no queden poweUps.
    public List<GameObject> defaultList = new List<GameObject>(); 
    public float spawnDelay = 15f;
    public float spawnDelayExtra = 15f;
    [Range(0, 100)]
    public int extraDelaySpawnProbability = 50;

    [HideInInspector] public bool canSpawn = true;

    [HideInInspector] public GameObject[] spawnPoints;
    private GameObject go; //Guarda el último pwerUps instanciado.

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

            go = Instantiate(spawnPowerUps[index], tileSpawnPoints[Random.Range(0, tileSpawnPoints.Length)]);

            CheckSpawn(index);
        }
    }

    public void CheckSpawn(int index)
    {
        spawnPowerUps.RemoveAt(index); //Elimina el powerups intanciado para que no se repita.

        if (spawnPowerUps.Count == 0) //Si la lista llega al cero, volvemos a crearla con su estado original.
        {
            //Debug.Log("Reset list");
            spawnPowerUps = new List<GameObject>(defaultList);
        }

        StartCoroutine(SpawnerPUCooldownCo());
    }

    //Maneja el tiempo entre el spawn de los powerUps.
    IEnumerator SpawnerPUCooldownCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);

        if (Random.Range(0, 101) < extraDelaySpawnProbability) //Hay una provabilidad de que el tiempo sea mayor.
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


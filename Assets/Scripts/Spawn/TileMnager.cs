using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Se encarga de Spawnear los tiles del escenario.
 *  AQUI se podría usar el object pool
 */

[AddComponentMenu("Scripts/ESI/Managers/Tile Manager")]

public class TileMnager : MonoBehaviour
{
    public GameObject[] tilePrefebs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 8;

    public Transform playerTransform;
    [SerializeField]
    private List<GameObject> activeTiles = new List<GameObject>(); 

    //Para Object Pool 
    [SerializeField]
    private List<GameObject> poolTiles = new List<GameObject>();
    private int firstActiveTileIndex = 0;
    private bool canPool;
    //GameObject go;

    // Start is called before the first frame update
    IEnumerator Start()
    { 
        // Sapwnea los primeros tiles.
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0); //Spawnea un tile especifico primero.
            else
                SpawnTile(Random.Range(0, tilePrefebs.Length));
            
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador llegar a una posicion determinada.
        if (playerTransform.position.z -120 >zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefebs.Length)); // Spawnea un tile aleatorio.
            DeleteTile(); // Elimina el tile más viejo de la lista de los tiles activos.
            //DisableTile(); // Desactiva el tile más viejo.
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = null;
        if (poolTiles != null && poolTiles.Count <= 0)
        {
            go = Instantiate(tilePrefebs[tileIndex], transform.forward * zSpawn, transform.rotation);
            poolTiles.Add(go);
        }
        else
        {
            for (int i = 0; i < poolTiles.Count; i++)
            {
                if (activeTiles.Contains(poolTiles[i]))
                {
                    continue;
                }
                else
                {
                    canPool = true;
                    go = poolTiles[i];
                    break;
                }
           
            }
            if (!canPool)
            {
                go = Instantiate(tilePrefebs[tileIndex], transform.forward * zSpawn, transform.rotation);
                poolTiles.Add(go);
            }
            else
            {
                if (go != null)
                {
                    go.transform.position = transform.forward * zSpawn;
                    go.SetActive(true);

                    for (int i = 0; i < go.transform.childCount; i++)
                    {
                        go.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                    
            }

        }

        if (go != null)
        {
            activeTiles.Add(go);
            zSpawn += tileLength; //Posiciona el siguiente Spawn al final del tile recien spawneado.
        }
        canPool = false;
        //go = null;
    }

    private void DeleteTile()
    {
        //Destroy(activeTiles[0]);
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }

    private void DisableTile()
    {
        activeTiles[firstActiveTileIndex].SetActive(false);
        if (firstActiveTileIndex < activeTiles.Count)
            firstActiveTileIndex++;
        else
            firstActiveTileIndex = 0;
    }

}

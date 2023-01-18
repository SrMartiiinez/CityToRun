using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Primer intento del Tile Map, sin uso.
 */
public class GroundSpawn : MonoBehaviour
{

    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    // Start is called before the first frame update
   private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame

}

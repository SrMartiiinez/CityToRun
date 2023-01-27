using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa el powerUp del JetPack.
 */
public class PowerUpBoost : MonoBehaviour
{

    public float duration = 5f;
    public Transform coinsSpawn;
    public GameObject[] coinsPrefabs; //Las monedas que aparecerán en la zona de arriba tras cojer el item


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character player = other.GetComponent<Character>();
            
            GameObject coinsPack = Instantiate(coinsPrefabs[Random.Range(0, coinsPrefabs.Length - 1)], 
                new Vector3(0, coinsSpawn.position.y, coinsSpawn.position.z), Quaternion.identity);
            
            player.BoostItem(duration, new GameObject[] {coinsPack});
            player.InRoll = false;
            

            Destroy(gameObject);
        }
    }

   
}

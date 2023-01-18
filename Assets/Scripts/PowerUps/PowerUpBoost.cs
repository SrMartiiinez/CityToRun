using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBoost : MonoBehaviour
{
    //public GameObject pickupEffect;

    //public float multiplier = 1.4f;
    public float duration = 5f;
    public Transform coinsSpawn;
    public GameObject[] coinsPrefabs;

    //private void OnEnable() {
    //    coinsSpawn.transform.parent = null; // detach from parent
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character player = other.GetComponent<Character>();
            
            GameObject coinsPack = Instantiate(coinsPrefabs[Random.Range(0, coinsPrefabs.Length - 1)], 
                new Vector3(0, coinsSpawn.position.y, coinsSpawn.position.z), Quaternion.identity);
            
            player.BoostItem(duration, new GameObject[] { /*coinsSpawn.gameObject,*/ coinsPack});
            player.InRoll = false;
            
            //StartCoroutine(Pickup());
            Destroy(gameObject);
        }
    }

   
}

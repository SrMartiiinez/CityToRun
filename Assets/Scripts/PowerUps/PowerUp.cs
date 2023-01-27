using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script que sería como un PowerUp genérico.
 * Sin uso.
 */

public class PowerUp : MonoBehaviour
{

    public float multiplier = 1.4f;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine( Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.health *= multiplier;


        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        stats.health /= multiplier;
 
        Destroy(gameObject);
    }
}

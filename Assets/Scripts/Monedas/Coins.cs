using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Maneja las mecanicas de las monedas.
 */

[AddComponentMenu("Scripts/ESI/Items/Coin")]

public class Coins : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(200 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Character.numberOfCoins += 1;
            //Debug.Log("Coins:" + Character.numberOfCoins);

            AnimParticleCoin.instance.TriggerCoinAnimation(); // Activa animacion de moneda recogida
            //Destroy(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Maneja las mecanicas de las monedas.
 */

[AddComponentMenu("Scripts/ESI/Items/Coin")]

public class Coins : MonoBehaviour
{
    public EventSO pickup;
    private bool collectedCoin;

    void OnEnable()
    {
        collectedCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(200 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collectedCoin)
        {
            collectedCoin = true;
            Character.numberOfCoins += 1;

            pickup.Ocurred(gameObject);

            transform.parent.gameObject.SetActive(false);
        }
    }

}

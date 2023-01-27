using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa el powerUp del Imán.
 */

public class PowerUpMagnet : MonoBehaviour
{

    public float duration = 10f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<Character>().Imman(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa el powerUp del Salto.
 */

public class PowerUpJump : MonoBehaviour
{

    public float multiplier = 2f;
    public float duration = 5f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<Character>().Salto(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
    }

}

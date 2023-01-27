using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa el powerUp del Invencibilidad.
 */
public class PowerUpInv : MonoBehaviour
{
    //public GameObject pickupEffect;

  
    public float duration = 5f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().Invencibilidad(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
       
    }

}

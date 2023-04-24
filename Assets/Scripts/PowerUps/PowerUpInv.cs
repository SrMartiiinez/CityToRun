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
            Character player = other.GetComponent<Character>();
            player.powerUpSound.PlayOneShot(player.powerUpSound.clip);
            player.Invencibilidad(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
       
    }

}

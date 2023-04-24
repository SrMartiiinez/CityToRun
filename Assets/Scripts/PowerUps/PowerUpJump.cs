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
            Character player = other.GetComponent<Character>();
            player.powerUpSound.PlayOneShot(player.powerUpSound.clip);
            
            player.Salto(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
    }

}

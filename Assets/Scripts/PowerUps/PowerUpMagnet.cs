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
            Character player = other.GetComponent<Character>();
            player.powerUpSound.PlayOneShot(player.powerUpSound.clip);

            player.Imman(duration); //Activa el efecto en el jugador.
            Destroy(gameObject);
        }
    }


}

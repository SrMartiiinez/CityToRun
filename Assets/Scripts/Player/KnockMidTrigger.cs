using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Trigger frontal de los obst�culos.
 *  - Da�o al jugador.
 *  - Desactiva el objeto.
 */
public class KnockMidTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Character player = other.gameObject.GetComponent<Character>();
            if (PoweUpManager.instance.Invencibility)
            {
                //TO DO: Aqu� intancio el efecto del objeto destrullendose

                Debug.Log("Kaboom");



               // Destroy(transform.parent.gameObject);
                transform.parent.gameObject.SetActive(false); //Desactivo el padre del objeto que contine el trigger.
            }
            else
            {
                player.Knock();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUpTrigger : MonoBehaviour 
{

    // Se especifica si este script se ejecuta en el lado derecho (true) o el izquierdo (false)
    public bool soyColliderDeLaDerecha;
 

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            Character player = other.gameObject.GetComponent<Character>();
            if (PoweUpManager.instance.Invencibility)
            {
                //TO DO: Aquí intancio el efecto del objeto destrullendose

                Debug.Log("Kaboom");

                

                Destroy(transform.parent.gameObject);
            }
            else
            {
                player.KnockUp(soyColliderDeLaDerecha);
            }
        }

    }
}

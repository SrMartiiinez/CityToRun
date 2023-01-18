using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInv : MonoBehaviour
{
    //public GameObject pickupEffect;

  
    public float duration = 5f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // StartCoroutine(Pickup(/*other*/));
            other.GetComponent<Character>().Invencibilidad(duration);
            Destroy(gameObject);
        }
       
    }
    
    IEnumerator Pickup(/*Collider player*/)
    {
        //Debug.Log("ItsWorks");
        //Character stats = player.GetComponent<Character>();
        //stats.Invecility = true;
      
        PoweUpManager.instance.Invencibility = true;

        //Aquí colo el efecto del power up
        Debug.Log(duration);
        //Instantiate(pickupEffect, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);
        Debug.Log("TimesOut");
        //Aquí quito el efecto power up

        //stats.Invecility = false;
   
        PoweUpManager.instance.Invencibility = false;

       
      



        Destroy(gameObject);



    }



}

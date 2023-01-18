using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : MonoBehaviour
{
    //public GameObject pickupEffect;

    public float duration = 10f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // StartCoroutine(Pickup());
            other.GetComponent<Character>().Imman(duration);
            Destroy(gameObject);
        }
    }

    IEnumerator Pickup()
    {
        //Debug.Log("ItsWorks");
        PoweUpManager.instance.Magnet = true;

        //Aquí colo el efecto del power up

        //Instantiate(pickupEffect, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        //Aquí quito el efecto power up

        PoweUpManager.instance.Magnet = false;
        //Debug.Log("TimesOut");



        Destroy(gameObject);



    }
}

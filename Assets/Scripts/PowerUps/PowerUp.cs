using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public GameObject pickupEffect;

    public float multiplier = 1.4f;
    public float duration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine( Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //Debug.Log("ItsWorks");
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.health *= multiplier;

        //Aquí colo el efecto del power up

        //Instantiate(pickupEffect, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        //Aquí quito el efecto power up

        stats.health /= multiplier;
        //Debug.Log("TimesOut");

        
 
        Destroy(gameObject);



    }
}

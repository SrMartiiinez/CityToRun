using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : MonoBehaviour
{
    //public GameObject pickupEffect;

    public float multiplier = 2f;
    public float duration = 5f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // StartCoroutine(Pickup());
            other.GetComponent<Character>().Salto(duration);
            Destroy(gameObject);
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //Debug.Log("ItsWorks");
        Character stats = player.GetComponent<Character>();
        stats.JumpPower *= multiplier;

        //Aquí colo el efecto del power up

        //Instantiate(pickupEffect, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        //Aquí quito el efecto power up

        stats.JumpPower /= multiplier;
        //Debug.Log("TimesOut");



        Destroy(gameObject);



    }
}

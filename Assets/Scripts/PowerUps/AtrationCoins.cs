using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtrationCoins : MonoBehaviour
{
    public float attratorSpeed;
    public GameObject pickupEffect;
    [SerializeField] private Collider attractionCollider;

    private void Update()
    {
        if (PoweUpManager.instance != null && PoweUpManager.instance.Magnet)
        {
            attractionCollider.enabled = true;
        }
        else
        {
            attractionCollider.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            pickupEffect.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, attratorSpeed * Time.deltaTime);
        }

    }

}

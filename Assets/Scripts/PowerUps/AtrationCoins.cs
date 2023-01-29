using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Maneja la mecánica de atraer monedas hacia el jugador.
 */

[AddComponentMenu("Scripts/ESI/Items/Atration Coins")]
public class AtrationCoins : MonoBehaviour
{
    public float attratorSpeed;
    public GameObject pickupEffect;
    [SerializeField] private Collider attractionCollider;
    private Vector3 defaultLocalPos;

    void Start() {
        defaultLocalPos = transform.localPosition;
    }

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
            //Mueve la moneda hacia el juegador
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, attratorSpeed * Time.deltaTime);
        }

    }

    private void OnDisable() {
        transform.localPosition = defaultLocalPos;
    }
}

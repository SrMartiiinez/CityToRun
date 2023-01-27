using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja el efecto de la sacudida de la camara.
/// </summary>


public class ShakeCamera : MonoBehaviour
{

    private bool started = false;
    public AnimationCurve curve; //Ajuste de la curva de la intensidad de duracion tra mover la cámara.
    public float duration = 1f;
    public float magnitude = 1f;




    public void Choque()
    {
       

        if (!started)
        {
            
            StartCoroutine(PlayCameraShakeAnimation());
            Debug.Log("Movimiento");
        }
    }

    IEnumerator Shaking()
    {

        started = true;

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration) // Se ajusta la fuerza del tiempo.
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        started = false;
    }


    public IEnumerator PlayCameraShakeAnimation() // Se realiza la magnitud del moiviemto de cámara de forma aleatoria.
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

}

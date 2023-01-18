using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja las animacion de las monedas recogidas.
/// </summary>


[AddComponentMenu("Scripts/ESI/Items/Anim Moneda Recogida")]
public class AnimParticleCoin : MonoBehaviour
{
    public static AnimParticleCoin instance; //Mas facil que usar el un GetComponent en CADA moneda.
    Animator anim;
    private void Awake()
    {

        if (instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    public void TriggerCoinAnimation()
    {
        anim.SetTrigger("AnimacionParticles");
    }
}

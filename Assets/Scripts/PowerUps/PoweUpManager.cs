using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpManager : MonoBehaviour
{

    public static PoweUpManager instance;

    public bool Invencibility;
    public bool Magnet;
    public bool Salti;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}

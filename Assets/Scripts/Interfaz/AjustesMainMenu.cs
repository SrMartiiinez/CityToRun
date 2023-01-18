using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Administra las funciones del menu ajustes
 */

[AddComponentMenu("Scripts/ESI/Ajustes/Ajustes MainMenu")]

public class AjustesMainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("Objeto padre de la UI del Menu Ajustes")] GameObject settingsMenu;
    
    [SerializeField, Tooltip("Objeto que contiene un sprite del boton que activa la UI del Menu Ajustes")] GameObject imagePause;




    void Start()
    {
        settingsMenu.SetActive(false);
        imagePause.SetActive(true);
    }

    public void Pause()
    {
        settingsMenu.SetActive(true);
        imagePause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void Resumen()
    {
        settingsMenu.SetActive(false);
        imagePause.SetActive(true);
    }


    //public void Home(int sceneID)
    //{
    //    Time.timeScale = 1f;
    //}



}

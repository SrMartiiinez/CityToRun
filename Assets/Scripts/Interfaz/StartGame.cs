using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Funcion del boton que inicia el juego.
 */

[AddComponentMenu("Scripts/ESI/Managers/Start Gameplay")]


public class StartGame : MonoBehaviour
{

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

}

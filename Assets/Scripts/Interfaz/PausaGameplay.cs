using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Administra el pausa y la interfaz dentro del juego.
 */

[AddComponentMenu("Scripts/ESI/Managers/Pause Gameplay")]

public class PausaGameplay : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject Numeros; // Prefab cuentra atrás.
    [SerializeField] float resumeCountdown; 

    //GameObjects de los botones del pausa.

    [SerializeField] GameObject imagePause;
    [SerializeField] GameObject imageAjustes;
    [SerializeField] GameObject imageSalida;




    private Animation anim;

    void Start()
    {
        pauseMenu.SetActive(false);
        imagePause.SetActive(true);
        imageAjustes.SetActive(false);
        imageSalida.SetActive(false);
        anim = gameObject.GetComponent<Animation>();

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        MenuPause();
    }

    //Se usa para ocultar el menu pausa cuando se va a mostrar el menú settings
    public void MenuPause()
    {

        pauseMenu.SetActive(true);
        Numeros.SetActive(false);
        imagePause.SetActive(false);
        imageAjustes.SetActive(false);
        imageSalida.SetActive(false);

    }

    public void Resumen()
    {

        pauseMenu.SetActive(false);
        Numeros.SetActive(true);
        imagePause.SetActive(false);
        imageAjustes.SetActive(false);
        imageSalida.SetActive(false);
        StartCoroutine(ResumeCountdownCo());

    }

    public void Ajustes()
    {
        imageAjustes.SetActive(true);
        imagePause.SetActive(false);
        pauseMenu.SetActive(false);
        imageSalida.SetActive(false);
    }


    public void Abandonar()
    {
        imageAjustes.SetActive(false);
        imagePause.SetActive(false);
        pauseMenu.SetActive(false);
        imageSalida.SetActive(true);
    }



    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    private void OnApplicationPause(bool pause)
    {
        Pause();
    }

    IEnumerator ResumeCountdownCo()
    {
        Debug.Log("Empieza");
        yield return new WaitForSecondsRealtime(resumeCountdown);
        Time.timeScale = 1f;
        imagePause.SetActive(true);
        anim.Play("Contador");
        Debug.Log("Termina");


    }

}

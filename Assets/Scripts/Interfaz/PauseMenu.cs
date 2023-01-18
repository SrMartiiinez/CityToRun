using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *  Primer intento del pausa que no funciona.
 *  Sin uso actual.
 */
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject Numeros;
    public float resumeCountdown;
    private GameObject imagePause;




    private Animation anim;

    void Start()
    {
        pauseMenu.SetActive(false);
        imagePause.SetActive(true);
        anim = gameObject.GetComponent<Animation>();

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Numeros.SetActive(false);
        imagePause.SetActive(false);

    }

    public void Resumen()
    {

        pauseMenu.SetActive(false);
        Numeros.SetActive(true);
        imagePause.SetActive(false);
        StartCoroutine(ResumeCountdownCo());

    }


    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    private void OnApplicationPause(bool pause)
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(true);
        Numeros.SetActive(false);
        imagePause.SetActive(false);
    }

    IEnumerator ResumeCountdownCo()
    {
        Time.timeScale = 1f;
        Debug.Log("Empieza");
        yield return new WaitForSecondsRealtime(resumeCountdown);
        imagePause.SetActive(true);
        anim.Play("Contador");
        Debug.Log("Termina");


    }

}

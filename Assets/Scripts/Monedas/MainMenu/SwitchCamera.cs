using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Administra el cambio de camara en el main menu.
 */

[AddComponentMenu("Scripts/ESI/Camera/Switch Camera")]

public class SwitchCamera : MonoBehaviour
{

    public Camera mainCamera;
    public string mainCameraName;
    public Camera secondCamera;
    public string secondCameraName;
    public Camera thirdCamera;
    public string thirdCameraName;

    public GameObject mainMenu;
    public GameObject secondMenu;
    public GameObject thirdMenu;

    void Start()
    {
        mainCamera.enabled = true;
        mainMenu.SetActive(true);

        secondCamera.enabled = false;
        secondMenu.SetActive(false);

        thirdCamera.enabled = false;
        thirdMenu.SetActive(false);
    }

    // Se usa en eun botón que indica el nombre de la cámara a la que cambiará.
    public void SwitchCurrentCamera(string cameraName)
    {
        if (cameraName.Equals(mainCameraName))
        {

            mainCamera.enabled = true;
            secondCamera.enabled = false;
            thirdCamera.enabled = false;
            
        }

        if (cameraName.Equals(secondCameraName))
        {
            mainCamera.enabled = false;
            secondCamera.enabled = true;
            thirdCamera.enabled = false;
        }

        if (cameraName.Equals(thirdCameraName))
        {
            mainCamera.enabled = false;
            secondCamera.enabled = false;
            thirdCamera.enabled = true;
        }

        mainMenu.SetActive(mainCamera.enabled);
        secondMenu.SetActive(secondCamera.enabled);
        thirdMenu.SetActive(thirdCamera.enabled);

    }

}

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

    [SerializeField] private Animator playerAnimator;

    void Start()
    {
        mainCamera.enabled = true;
        mainMenu.SetActive(true);

        secondCamera.enabled = false;
        secondMenu.SetActive(false);

        thirdCamera.enabled = false;
        thirdMenu.SetActive(false);
        SetDefaultSkin();
    }

    // Se usa en eun botón que indica el nombre de la cámara a la que cambiará.
    public void SwitchCurrentCamera(string cameraName)
    {
        if (cameraName.Equals(mainCameraName))
        {
            playerAnimator.SetBool("SelectSkin", false);
            mainCamera.enabled = true;
            //SimpleSkinUpdater.UpdateSkins();
            secondCamera.enabled = false;
            thirdCamera.enabled = false;
        }

        if (cameraName.Equals(secondCameraName))
        {
            playerAnimator.SetBool("SelectSkin", true);
            mainCamera.enabled = false;
            secondCamera.enabled = true;
            thirdCamera.enabled = false;
        }

        if (cameraName.Equals(thirdCameraName))
        {
            playerAnimator.SetBool("SelectSkin", false);
            mainCamera.enabled = false;
            secondCamera.enabled = false;
            thirdCamera.enabled = true;
        }

        mainMenu.SetActive(mainCamera.enabled);
        secondMenu.SetActive(secondCamera.enabled);
        thirdMenu.SetActive(thirdCamera.enabled);

    }

    private void SetDefaultSkin()
    {
        if (!PlayerPrefs.HasKey("SelectedSkin"))
        {
            PlayerPrefs.SetInt("SelectedSkin", 0);
        }
        
    }

}

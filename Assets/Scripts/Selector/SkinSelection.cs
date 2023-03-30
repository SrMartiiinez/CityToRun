using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour {
    [System.Serializable]
    public class Skin {
        public GameObject gameObject; // GameObject de la skin
        public int cost; // Coste de la skin
    }

    [SerializeField] private List<Skin> skins; // Lista de skins disponibles
    [SerializeField] private Button confirmButton; // Botón para confirmar la selección de skin
    [SerializeField] private Text confirmButtonText; // Texto del botón de confirmación

    [SerializeField] private string buyText = "Comprar";
    [SerializeField] private string selectionText = "Seleccionar";

    [SerializeField] private MainMenuInfo mainMenuInfo;

    [SerializeField] private int playerMoney; // Dinero del jugador
    private int selectedSkinIndex = 0; // Índice de la skin seleccionada

    private bool isStarted;

    void Start() {
        //PlayerPrefs.DeleteAll();
        //Debug.Log("Playerpref borrado.");
        // Obtener el índice de la última skin seleccionada de PlayerPrefs
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        PlayerPrefs.SetInt("Skin" + selectedSkinIndex, 1);  // Autocompramos la primera.
        confirmButtonText.text = selectionText;
        confirmButton.interactable = false;

        if (!isStarted)
            CheckPlayerMoney();

        isStarted = true;
    }

    void OnEnable() {
        // Actualizar el dinero del jugador
        if (isStarted)
            CheckPlayerMoney();

        // Mostrar la skin seleccionada cuando el objeto se activa
        ShowSkin(selectedSkinIndex);
    }

    void OnDisable() {
        // Ocultar la skin seleccionada cuando el objeto se desactiva
        //skins[selectedSkinIndex].gameObject.SetActive(false);
    }

    public void SelectNextSkin() {
        // Ocultar la skin actualmente seleccionada
        skins[selectedSkinIndex].gameObject.SetActive(false);
        Debug.Log("ArraySkinNext: " + selectedSkinIndex);

        // Seleccionar la siguiente skin en la lista
        selectedSkinIndex = (selectedSkinIndex + 1) % skins.Count;
        Debug.Log("Delante");

        // Mostrar la nueva skin seleccionada
        ShowSkin(selectedSkinIndex);
    }

    public void SelectPreviousSkin() {
        // Ocultar la skin actualmente seleccionada
        skins[selectedSkinIndex].gameObject.SetActive(false);
        Debug.Log("ArraySkinPrev: " + selectedSkinIndex);

        // Seleccionar la skin anterior en la lista
        selectedSkinIndex = (selectedSkinIndex - 1 + skins.Count) % skins.Count;
        Debug.Log("Patrs");

        // Mostrar la nueva skin seleccionada
        ShowSkin(selectedSkinIndex);
    }

    private void ShowSkin(int index) {
        // Mostrar la skin en el índice especificado
        skins[index].gameObject.SetActive(true);
        //Debug.Log("index: " + index);

        // Verificar si la skin ya ha sido comprada
        if (PlayerPrefs.GetInt("Skin" + index) == 1) {
            // Cambiar el texto del botón de confirmación a "Seleccionar"
            confirmButtonText.text = selectionText;

            // Verificar si la skin mostrada es la misma que la última skin seleccionada
            if (index == PlayerPrefs.GetInt("SelectedSkin", 0)) {
                // Desactivar el botón de selección si la skin mostrada ya está siendo utilizada
                confirmButton.interactable = false;
            }
            else {
                // Activar el botón de selección si la skin mostrada no está siendo utilizada
                confirmButton.interactable = true;
            }
        }
        else {
            // Cambiar el texto del botón de confirmación a "Comprar"
            confirmButtonText.text = buyText;

            // Verificar si el jugador tiene suficiente dinero para comprar la skin
            if (playerMoney >= skins[index].cost) {
                // Desbloquear el botón de confirmación
                confirmButton.interactable = true;
            }
            else {
                // Bloquear el botón de confirmación
                confirmButton.interactable = false;
            }
        }
    }

    public void ConfirmSelection() {
        // Verificar si la skin ya ha sido comprada
        if (PlayerPrefs.GetInt("Skin" + selectedSkinIndex) == 1) {
            // Guardar la selección del usuario en PlayerPrefs
            PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
            
            // Desactivar el botón de selección después de seleccionar una skin 
            confirmButton.interactable = false;
        }
        else {
            // Reducir el dinero del jugador en el costo de la skin seleccionada
            playerMoney -= skins[selectedSkinIndex].cost;
            PlayerPrefs.SetInt("numberOfTotalCoins", playerMoney);
            mainMenuInfo.UpdateUIText();

            // Guardar la compra de la skin en PlayerPrefs
            PlayerPrefs.SetInt("Skin" + selectedSkinIndex, 1);

            // Cambiar el texto del botón de confirmación a "Seleccionar"
            confirmButtonText.text = "Seleccionar";
        }
    }

    private void CheckPlayerMoney() {
        // Obtener el dinero del jugador de PlayerPrefs
        playerMoney = PlayerPrefs.GetInt("numberOfTotalCoins", 0);
        mainMenuInfo.UpdateUIText();
    }

    public void MenuSkins()
    {
        foreach (Skin skin in skins)
            skin.gameObject.SetActive(false);

        skins[PlayerPrefs.GetInt("SelectedSkin", 0)].gameObject.SetActive(true);
        //Debug.Log("SelectedSkin: " + PlayerPrefs.GetInt("SelectedSkin"));
    }
}
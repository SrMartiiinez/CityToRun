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
    [SerializeField] private int playerMoney; // Dinero del jugador
    private int selectedSkinIndex = 0; // Índice de la skin seleccionada

    void Start() {
        // Obtener el índice de la última skin seleccionada de PlayerPrefs
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
    }

    void OnEnable() {
        // Actualizar el dinero del jugador
        CheckPlayerMoney();

        // Mostrar la skin seleccionada cuando el objeto se activa
        ShowSkin(selectedSkinIndex);
    }

    void OnDisable() {
        // Ocultar la skin seleccionada cuando el objeto se desactiva
        skins[selectedSkinIndex].gameObject.SetActive(false);
    }

    public void SelectNextSkin() {
        // Ocultar la skin actualmente seleccionada
        skins[selectedSkinIndex].gameObject.SetActive(false);

        // Seleccionar la siguiente skin en la lista
        selectedSkinIndex = (selectedSkinIndex + 1) % skins.Count;

        // Mostrar la nueva skin seleccionada
        ShowSkin(selectedSkinIndex);
    }

    public void SelectPreviousSkin() {
        // Ocultar la skin actualmente seleccionada
        skins[selectedSkinIndex].gameObject.SetActive(false);

        // Seleccionar la skin anterior en la lista
        selectedSkinIndex = (selectedSkinIndex - 1 + skins.Count) % skins.Count;

        // Mostrar la nueva skin seleccionada
        ShowSkin(selectedSkinIndex);
    }

    private void ShowSkin(int index) {
        // Mostrar la skin en el índice especificado
        skins[index].gameObject.SetActive(true);

        // Verificar si la skin ya ha sido comprada
        if (PlayerPrefs.GetInt("Skin" + index) == 1) {
            // Cambiar el texto del botón de confirmación a "Seleccionar"
            confirmButtonText.text = "Seleccionar";

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
            confirmButtonText.text = "Comprar";

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

            // Guardar la compra de la skin en PlayerPrefs
            PlayerPrefs.SetInt("Skin" + selectedSkinIndex, 1);

            // Cambiar el texto del botón de confirmación a "Seleccionar"
            confirmButtonText.text = "Seleccionar";
        }
    }

    private void CheckPlayerMoney() {
        // Obtener el dinero del jugador de PlayerPrefs
        //playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
    }
}
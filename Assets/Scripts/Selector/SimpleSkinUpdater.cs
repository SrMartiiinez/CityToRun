using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkinUpdater : MonoBehaviour {

    private static GameObject skinsParent;                      // Objeto padre de las skins.

    private void Start() {
        skinsParent = gameObject;
    }

    public static void UpdateSkins() {
        // Buscar el objeto padre de las skins
        if (skinsParent == null)
            skinsParent = GameObject.Find("PlayerMainMenu");

        // Ocultar todas las skins
        foreach (Transform child in skinsParent.transform) {
            child.gameObject.SetActive(false);
        }

        // Mostrar la skin guardada en PlayerPrefs
        skinsParent.transform.GetChild(PlayerPrefs.GetInt("SelectedSkin", 3)).gameObject.SetActive(true);
    }
}

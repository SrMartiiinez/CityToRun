using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Administra la UI del GameOver. 
/// </summary>

[AddComponentMenu("Scripts/ESI/Managers/GameOver Manager")]

public class GameOverManager : MonoBehaviour {

    public static GameOverManager instance; //Singleton

    public GameObject gameOverUI; 
    public GameObject hudGO; //Interfaz del juego
    public Character player;
    public float invulnerabilityTimeAfterTryAgain = 1f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        gameOverUI.SetActive(false);
        player = FindObjectOfType<Character>();

    }

    public void GameOver() {
        gameOverUI.SetActive(true);
    }

    public void DisableUI()
    {
        hudGO.SetActive(false);
    }

    public void TryAgain() {
        gameOverUI.SetActive(false);
        player.ResetPlayer(invulnerabilityTimeAfterTryAgain);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ExampleClass.instance.time = PlayerPrefs.GetFloat("MusicaFondo"); //Mantine la ultima posicion de la música.
        ExampleClass.instance.PlaySound();
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    // Para el juego en el editor.
#else
        Application.Quit(); // Quit the game.
#endif
    }
}

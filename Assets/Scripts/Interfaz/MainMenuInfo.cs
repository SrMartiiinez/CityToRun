using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Maneja la informacion de monedas que tienes en total y monedas que has obtenido en la última partida.
 */

[AddComponentMenu("Scripts/ESI/Info/Main Menu Info")]

public class MainMenuInfo : MonoBehaviour
{
    public Text coins; //Texto que mostrará el numero de monedas en total.
    public Text coins2; //Texto que mostrará el numero de monedas en la última partida.
    private int coinsAmount = 0;

    public Text Distance; //Texto que mostrará el numero de monedas en la última partida.

    public GameObject skins;

    void Start()
    {
        UpdateUIText();


        skins.transform.GetChild(1).gameObject.SetActive(false);
        // Mostrar la skin guardada en PlayerPrefs
        skins.transform.GetChild(PlayerPrefs.GetInt("SelectedSkin", 0) + 1).gameObject.SetActive(true);
        Debug.Log("Skin: " + skins.transform.GetChild(PlayerPrefs.GetInt("SelectedSkin", 0) + 1).name);
    }

    public void UpdateUIText()
    {
        if (PlayerPrefs.HasKey("numberOfTotalCoins"))
        {
            coinsAmount = PlayerPrefs.GetInt("numberOfTotalCoins");
            //Debug.Log(coinsAmount);
            coins.text = coinsAmount.ToString();
        }

        if (PlayerPrefs.HasKey("numberOfCoins"))
        {
            coinsAmount = PlayerPrefs.GetInt("numberOfCoins");
            //Debug.Log(coinsAmount);
            coins2.text = coinsAmount.ToString();
        }

        if (PlayerPrefs.HasKey("RecordPersonal"))
        {
            Distance.text = PlayerPrefs.GetInt("RecordPersonal", 0).ToString();
        }
    }
}

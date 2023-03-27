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
    void Start()
    {
        UpdateUIText();

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
    }
}

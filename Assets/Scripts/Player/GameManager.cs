using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Administra cosas genrérias del juego.
 * - Muestra el texto de la distancias que has recorrido tanto en el pausa, gameover y inicio de la partida.
 */

public class GameManager : MonoBehaviour
{
    public static int distance;

    private GameObject player;

    public Text uiDistance;
    public Text uiDistancePause;
    public Text uiDistanceDeath;

    [SerializeField] private GameObject skinFather;

    [HideInInspector] public static Transform coinsRotation;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        skinFather.transform.GetChild(PlayerPrefs.GetInt("SelectedSkin", 0) + 1).gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        uiDistancePause.text = distance.ToString() + " m";
        uiDistanceDeath.text = distance.ToString() + " m";

        transform.Rotate(200 * Time.deltaTime, 0, 0);
        coinsRotation = transform;
    }
}

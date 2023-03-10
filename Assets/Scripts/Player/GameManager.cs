using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Administra cosas genr?rias del juego.
 * - Muestra el texto de la distancias que has recorrido tanto en el pausa, gameover y inicio de la partida.
 */

public class GameManager : MonoBehaviour
{
    private GameObject player;

    public Text uiDistance;
    public Text uiDistancePause;
    public Text uiDistanceDeath;

    [HideInInspector] public static Transform coinsRotation;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        uiDistancePause.text = distance.ToString() + " m";
        uiDistanceDeath.text = distance.ToString() + " m";

        transform.Rotate(200 * Time.deltaTime, 0, 0);
        coinsRotation = transform;
    }
}

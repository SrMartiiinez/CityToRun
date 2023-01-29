using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SIDE { Left, Mid, Right } //Carriles del juego.

/// <summary>
/// Maneja el funcionamiento del personaje.
/// </summary>

[System.Serializable]
public class Character : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown; // Movimiento de los toques de la pantalla.
    public float XValue;
    private CharacterController m_char;
    private Animator m_Animator;
    public float x;
    public float y;
    public float SpeedDodge;
    public float JumpPower = 7f;
    public bool InJump;
    public bool InRoll;
    float RollCounter;
    public float FwdSpeed = 7f;

    // Datos iniciales del collider del CharacterController.
    private float ColHeight; 
    private float ColCenterY;
    public float CollCenterYOnRoll;
    private bool ColRestored = true;


    public static int numberOfCoins;
    public static int numberOfTotalCoins;
    public Text coinsText;
    public Text coinsTextPause;
    public Text coinsTextDeath;

    private bool knocked; // Variable que indica si el personaje está noqueado o no.
    public float knockUpTime = 5f; // Tiempo de noqueo.
    public float jumpMultiplier = 2f;
    [SerializeField] private bool gameOver;
    public float gameOverCooldown = 2f;
    public ShakeCamera shakeCamera;
    public bool MaxJump;
    public bool Boost; //PowerUp jeypack activado o desactivado.
    public bool cancelJump;

    public GameObject invencibilidadIndicador;
    public GameObject magnetIndicador;
    public GameObject jetpackIndicador;



    public Slider powerUpDurationSlider;

    



    // Start is called before the first frame update
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        m_Animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
        numberOfCoins = 0;
        numberOfTotalCoins = 0;
        powerUpDurationSlider.gameObject.SetActive(false);


        jetpackIndicador.SetActive(false);
        magnetIndicador.SetActive(false);
        invencibilidadIndicador.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (gameOver) 
        {
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            return; // Si el juego está acabado, no hace nada más.
        }

        if (Time.timeScale == 1)
        {
            // Captura - Simula los movimientos de los toques en la pantalla. 
            SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Left;
            SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Right;
            SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Up;
            SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Down;
        }

        if (SwipeLeft && !InRoll)
        {
            if (m_Side == SIDE.Mid) //Comprueba si está en el carril central.
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
                if (!knocked) 
                {
                    m_Animator.Play("Left");
                }
                else 
                {
                    m_Animator.Play("KnockUpLeft");
                }
            }
            else if (m_Side == SIDE.Right)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                if (!knocked) 
                {
                    m_Animator.Play("Left");
                }
                else 
                {
                    m_Animator.Play("KnockUpLeft");
                }
            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None; //Resetea el estado de los toques de la pantalla.
        }
        else if (SwipeRight && !InRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
                if (!knocked) 
                {
                    m_Animator.Play("Right");
                }
                else 
                {
                    m_Animator.Play("KnockUpRight");
                }
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                if (!knocked) 
                {
                    m_Animator.Play("Right");
                }
                else 
                {
                    m_Animator.Play("KnockUpRight");
                }
            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
        }

        //Movimiento del perasonaje.
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);
       
        Jump();
        Roll();


        coinsText.text = "" + numberOfCoins;
        coinsTextPause.text = "" + numberOfCoins;
        coinsTextDeath.text = "" + numberOfCoins;

    }

    public void Roll()
    {
        if (Boost == false)
        {
            RollCounter -= Time.deltaTime;
            if (RollCounter <= 0f)
            {
                RollCounter = 0f;
                InRoll = false;
                GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            }
            if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Rolling") && SwipeDown)
            {

                RollCounter = 0.20f;

                //Ajustamos la forma del collider.
                m_char.center = new Vector3(0,CollCenterYOnRoll, 0);
                m_char.height = ColHeight / 2f;

                //Arregla el problema del jetpack por el cual el roll se quedaba pillado justo cuando cojes el jetpack.
                if (Boost == false)
                {
                    y -= 10f;
                }
                m_Animator.CrossFadeInFixedTime("Roll", 0.05f); //Cambia la animacion haciendo una transición.
                //Debug.Log("EmpiezaRoll");
                InRoll = true;
                InJump = false;
                ColRestored = false;

            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
        }
           
    }

    public void Jump()
    {
        if (m_char.isGrounded)
        {


            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {

                if (cancelJump)
                {
                    m_Animator.Play("Roll");
                    cancelJump = false;
                   
                }
                else
                {
                    m_Animator.Play("Landing");
                }
                
                InJump = false;
                
            }
            if (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.05f);
                InJump = true;
                
            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
        }
        else
        {
            if (Boost == false)
            {
                y -= JumpPower * 2 * Time.deltaTime;
            }

            if (m_char.velocity.y < -0.1f && (!m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Rolling")))
                m_Animator.Play("Falling");
            

            if (SwipeDown)
            {
                cancelJump = true;

            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Se necesita el tag "FrontCollider" atachando el objeto con un trigger. 
        if (!PoweUpManager.instance.Invencibility && !gameOver && other.gameObject.tag == "FrontCollider")
        {
            shakeCamera.Choque();
            StartCoroutine(Death());
            //Debug.Log("Game Over loser.");
        }
        if (PoweUpManager.instance.Invencibility && other.gameObject.tag == "FrontCollider")
        {
            //TO DO: Aquí intancio el efecto del objeto destrullendose

            //Debug.Log("Kaboom");


            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // Mueve el personaje a un lado.
    public void KnockUp(bool toLeft) 
    {
        if (!PoweUpManager.instance.Invencibility)
        {
            shakeCamera.Choque();

            if (knocked)
            {
                // Game Over.
                StartCoroutine(Death());
                Debug.Log("Game Over loser.");
            }

            // Si el personaje colisiona con el lado derecho del objeto, se desplaza a la izquierda.
            if (toLeft)
            {
                GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.Left;
                //Debug.Log("Izquierda");
            }
            else
            {
                GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.Right;
            }


            StartCoroutine(KnockTime());
        }

    }


    public void Knock()
    {
        if (!PoweUpManager.instance.Invencibility)
        {
            shakeCamera.Choque();

            if (knocked) //Si ya te habias golpeado...
            {
                // Game Over.
                StartCoroutine(Death());
                //Debug.Log("Game Over loser.");
            }

            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            

            StartCoroutine(KnockTime());
        }

    }


    // Corutina que gestiona el tiempo en el que se está noqueado.
    IEnumerator KnockTime() 
    {
        knocked = true;
        m_Animator.SetBool("Knocked", true);
        yield return new WaitForSeconds(knockUpTime);
        knocked = false;
        m_Animator.SetBool("Knocked", false);
    }
    
    //Gestiona la muerte.
    IEnumerator Death() 
    {
        gameOver = true;
        GameOverManager.instance.DisableUI();
        m_Animator.Play("Death");
        yield return new WaitForSeconds(gameOverCooldown);
        GameOverManager.instance.GameOver();
        PlayerPrefs.SetInt("numberOfCoins", numberOfCoins);
        if (PlayerPrefs.HasKey("numberOfCoins"))
        {
            numberOfTotalCoins = PlayerPrefs.GetInt("numberOfTotalCoins");
            numberOfTotalCoins += numberOfCoins;
            PlayerPrefs.SetInt("numberOfTotalCoins", numberOfTotalCoins);
        }
        

        PlayerPrefs.SetFloat("MusicaFondo", ((float)ExampleClass.instance.time));
    }


    
    public void BoostItem(float duration, GameObject[] objectsToDelete)
    {
        if (!Boost)
            StartCoroutine(BoostCorroutine(duration, objectsToDelete));

    }


    //Gestiona la mecánca del powerup del jetpack.
    IEnumerator BoostCorroutine(float duration, GameObject[] objectsToDelete)
    {
        FwdSpeed = 0;
        Boost = true;
        InJump = true;
        y = 9;
        jetpackIndicador.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        y = 0;
        FwdSpeed = 30;
        
        powerUpDurationSlider.gameObject.SetActive(true);
        powerUpDurationSlider.maxValue = duration;
        // Iteramos mientras aún quede tiempo en la cuenta atrás
        while (duration > 0) {
            // Esperamos 0,01 segundo antes de continuar
            yield return new WaitForSeconds(0.01f);
            // Restamos 0,01 segundo al tiempo restante
            duration -= 0.01f;
            // Actualizamos el slider
            powerUpDurationSlider.value = duration;
            
        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);
        FwdSpeed = 7;
        Boost = false;
        InJump = false;
        jetpackIndicador.SetActive(false);
        foreach (GameObject obj in objectsToDelete) {
            Destroy(obj);
            yield return null; // Espera al siguiente frame para continuar, reduciendo la carga
        }
    } 
    
    
    public void Invencibilidad(float duration)
    {
     
            StartCoroutine(InvencibilidadCo(duration));

    }


    //Gestiona la mecánca del powerup del Invencibilidad.
    IEnumerator InvencibilidadCo(float duration)
    {
        PoweUpManager.instance.Invencibility = true;
        invencibilidadIndicador.SetActive(true);
        powerUpDurationSlider.gameObject.SetActive(true);
        powerUpDurationSlider.maxValue = duration;
        // Iteramos mientras aún quede tiempo en la cuenta atrás
        while (duration > 0) {
            // Esperamos 0,01 segundo antes de continuar
            yield return new WaitForSeconds(0.01f);
            // Restamos 0,01 segundo al tiempo restante
            duration -= 0.01f;
            // Actualizamos el slider
            powerUpDurationSlider.value = duration;
            
        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);
        
        PoweUpManager.instance.Invencibility = false;
        invencibilidadIndicador.SetActive(false);
    }
    
    public void Imman(float duration)
    {
     
            StartCoroutine(ImmanCo(duration));

    }


    //Gestiona la mecánca del powerup del Imán.
    IEnumerator ImmanCo(float duration)
    {
        PoweUpManager.instance.Magnet = true;
        magnetIndicador.SetActive(true);
        powerUpDurationSlider.gameObject.SetActive(true);
        powerUpDurationSlider.maxValue = duration;
        // Iteramos mientras aún quede tiempo en la cuenta atrás
        while (duration > 0) {
            // Esperamos 0,01 segundo antes de continuar
            yield return new WaitForSeconds(0.01f);
            // Restamos 0,01 segundo al tiempo restante
            duration -= 0.01f;
            // Actualizamos el slider
            powerUpDurationSlider.value = duration;

        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);
        
        PoweUpManager.instance.Magnet = false;
        magnetIndicador.SetActive(false);
    } 
    
    public void Salto(float duration)
    {
     
            StartCoroutine(SaltoCo(duration));

    }

    //Gestiona la mecánca del powerup del Salto.
    IEnumerator SaltoCo(float duration)
    {
        JumpPower *= jumpMultiplier;
        
        powerUpDurationSlider.gameObject.SetActive(true);
        powerUpDurationSlider.maxValue = duration;
        // Iteramos mientras aún quede tiempo en la cuenta atrás
        while (duration > 0) {
            // Esperamos 0,01 segundo antes de continuar
            yield return new WaitForSeconds(0.01f);
            // Restamos 0,01 segundo al tiempo restante
            duration -= 0.01f;
            // Actualizamos el slider
            powerUpDurationSlider.value = duration;
        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);
        
        JumpPower /= jumpMultiplier;
    }

    // Reinicia el collider del character controller a como estaba por defecto. Se llama incluso si la animación se cancela.
    void OnAnimatorMove() 
    {
        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") && !m_Animator.GetNextAnimatorStateInfo(0).IsName("Roll") 
            && !ColRestored) 
        {
            AfterRoll();
        }
    }

    //Reinicia el collider del character controller a como estaba por defecto. Se llama al final de la animación Rolling.
    public void AfterRoll()
    {
        m_char.center = new Vector3(0, ColCenterY, 0);
        m_char.height = ColHeight;
        ColRestored = true;
        Debug.Log("Collider Restored");
    }
    
}
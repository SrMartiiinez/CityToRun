using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public enum SIDE { Left, Mid, Right }
public class Character : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue;
    private CharacterController m_char;
    private Animator m_Animator;
    public float x;
    public float y;
    public float SpeedDodge;
    public float JumpPower = 7f;
    public bool InJump;
    public bool InRoll;
    public float FwdSpeed = 7f;
    private float ColHeight;
    private float ColCenterY;

    public static int numberOfCoins;
    public static int numberOfTotalCoins;
    public Text coinsText;
    public Text coinsTextPause;
    public Text coinsTextDeath;

    private bool knocked; // Variable that indicates if the character is knocked up or not.
    public float knockUpTime = 5f; // Time that the character will be knocked up.
    public float multiplier = 2f;
    [SerializeField] private bool gameOver;
    public float gameOverCooldown = 2f;
    public ShakeCamera shakeCamera;
    public bool MaxJump;
    //public bool Invencibility;
    public bool Boost;
    public bool gravitybool = true;
    public bool cancelJump;

    public GameObject invencibilidadIndicador;
    public GameObject magnetIndicador;
    public GameObject jetpackIndicador;
    //public GameObject jumpIndicador;

    public Slider powerUpDurationSlider;

    ExampleClass instance; 



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

        //jumpIndicador.SetActive(false);
        jetpackIndicador.SetActive(false);
        magnetIndicador.SetActive(false);
        invencibilidadIndicador.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ////////SECCION DE DAVID
        //if (Input.GetButtonDown("Jump") && Boost == false)
        //{
        //    StartCoroutine(BoostCorroutine(1f));
        //}
       //if(gravitybool == false)
       // {
       //     y = 0;
       // }


        ///////////////////////////
        if (gameOver) 
        {
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            return; // If the game is over, do nothing.
        }

        if (Time.timeScale == 1)
        {
            SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Left;
            SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Right;
            SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Up;
            SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Down;
            //SwipeLeft = GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Left;
            //SwipeRight = GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Right;
            //SwipeUp = GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Up;
            //SwipeDown = GridViewController.Instance.currentSwipe == GridViewController.DraggedDirection.Down;
        }

        if (SwipeLeft && !InRoll)
        {
            if (m_Side == SIDE.Mid)
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
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
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
       
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);
       
        Jump();
        Roll();


        coinsText.text = "" + numberOfCoins;
        coinsTextPause.text = "" + numberOfCoins;
        coinsTextDeath.text = "" + numberOfCoins;

    }


    internal float RollCounter;


    public void Roll()
    {
        if (Boost == false)
        {
            RollCounter -= Time.deltaTime;
            if (RollCounter <= 0f)
            {
                RollCounter = 0f;
                m_char.center = new Vector3(0, ColCenterY, 0);
                m_char.height = ColHeight;
                InRoll = false;
                GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            }
            if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Rolling") && SwipeDown)
            {

                RollCounter = 0.20f;
                m_char.center = new Vector3(0, ColCenterY / 2f, 0);
                m_char.height = ColHeight / 0.5f;
                if (Boost == false)
                {
                    y -= 10f;
                }
                m_Animator.CrossFadeInFixedTime("Roll", 0.05f);
                InRoll = true;
                InJump = false;

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

            if (/*!cancelJump && */m_char.velocity.y < -0.1f)
                m_Animator.Play("Falling");
            

            if (SwipeDown)
            {
                cancelJump = true;
                //m_Animator.Play("Roll");
            }
            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Need the tag "FrontCollider" attached to the object with the trigger.
        if (!PoweUpManager.instance.Invencibility && !gameOver && other.gameObject.tag == "FrontCollider")
        {
            shakeCamera.Choque();
            StartCoroutine(Death());
            Debug.Log("Game Over loser.");
        }
        if (PoweUpManager.instance.Invencibility && other.gameObject.tag == "FrontCollider")
        {
            //TO DO: Aquí intancio el efecto del objeto destrullendose

            Debug.Log("Kaboom");

            //Destroy(other.gameObject.transform.parent.gameObject);
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // Move the character aside.
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

            // If the collision is on the right, move the character to the left.
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

            if (knocked)
            {
                // Game Over.
                StartCoroutine(Death());
                Debug.Log("Game Over loser.");
            }

            GridViewController.Instance.currentSwipe = GridViewController.DraggedDirection.None;
            // If the collision is on the right, move the character to the left.

            StartCoroutine(KnockTime());
        }

    }


    // Coroutine to wait for the knock up time.
    IEnumerator KnockTime() 
    {
        knocked = true;
        m_Animator.SetBool("Knocked", true);
        yield return new WaitForSeconds(knockUpTime);
        knocked = false;
        m_Animator.SetBool("Knocked", false);
    }
    
    
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

    IEnumerator BoostCorroutine(float duration, GameObject[] objectsToDelete)
    {
        FwdSpeed = 0;
        Boost = true;
        InJump = true;
        y = 10;
        jetpackIndicador.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //gravitybool = false;
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
            
            //Debug.Log("Tiempo restante: " + duration);
        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);

        //gravitybool = true;
        //y = -10;
        FwdSpeed = 7;
        Boost = false;
        InJump = false;
        jetpackIndicador.SetActive(false);
        foreach (GameObject obj in objectsToDelete) {
            Destroy(obj);
            yield return null; // Espera al siguiente frame para continuar reduciendo la carga
        }
    } 
    
    
    public void Invencibilidad(float duration)
    {
     
            StartCoroutine(InvencibilidadCo(duration));

    }

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
            
            //Debug.Log("Tiempo restante: " + duration);
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

            //Debug.Log("Tiempo restante: " + duration);
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

    IEnumerator SaltoCo(float duration)
    {
        //jumpIndicador.SetActive(true);
        JumpPower *= multiplier;
        
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

            //Debug.Log("Tiempo restante: " + duration);
        }
        powerUpDurationSlider.value = 1;
        powerUpDurationSlider.gameObject.SetActive(false);
        
        JumpPower /= multiplier;
        //jumpIndicador.SetActive(false);
    }

    
}
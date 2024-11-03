using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    public int Live;                     // Vidas del jugador, asignadas desde InstancePlayer
    public TMP_Text live;                // Texto de UI para mostrar vidas
    private bool isImmune = false;       // Estado de inmunidad temporal
    public float immuneTime = 2.0f;      // Tiempo de inmunidad en segundos
    private Coroutine immunityCoroutine; // Referencia para detener la corrutina de inmunidad
    private float lastDamageTime = -10f; // Marca el último tiempo en que se recibió daño
    private Color originalColor;         // Almacena el color original del jugador
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer
    private Rigidbody2D rd;
    private Animator animator;

    private void Awake()
    {
        if (FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Obtiene la vida inicial del jugador desde IntancePlayer
        Live = IntancePlayer.instance != null ? IntancePlayer.instance.initialLives : 5;
        live.text = "Vidas: " + Live; // Mostrar la cantidad inicial de vidas
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rd.velocity = new Vector2(horizontal * speed, vertical * speed);

        if (horizontal > 0)
        {
            animator.Play("right");
        }
        else if (horizontal < 0)
        {
            animator.Play("left");
        }
        else if (vertical > 0)
        {
            animator.Play("Up");
        }
        else if (vertical < 0)
        {
            animator.Play("Down");
        }
    }

    public void ResetPlayer()
    {
        Live = 5; // Restablecer vidas
        live.text = "Vidas: " + Live;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isImmune)
        {
            TakeDamage(1);
        }
    }
    //El quiteas la vida
    private void TakeDamage(int damage)
    {
        Live -= damage;                    // Reduce vida
        live.text = "Vidas: " + Live;

        if (Live <= 0)
        {
            Debug.Log("Jugador ha muerto");
            StartCoroutine(HandleDeath());  // Ejecuta la rutina para manejar la muerte
        }
        else
        {
            lastDamageTime = Time.time;

            if (immunityCoroutine != null)
            {
                StopCoroutine(immunityCoroutine);
            }
            immunityCoroutine = StartCoroutine(ActivateImmunity());  // Inicia la inmunidad temporal
        }
    }
    //Este metodo es para hacer la animacon de recibir daño
    private IEnumerator ActivateImmunity()
    {
        isImmune = true;                        
        spriteRenderer.color = Color.red;       
        yield return new WaitForSeconds(immuneTime);  
        spriteRenderer.color = originalColor;   
        isImmune = false;                       
        immunityCoroutine = null;               
    }
    //Este metodo es para el volver a intentar
    private IEnumerator HandleDeath()
    {
        animator.Play("Deat"); 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); 
        SceneManager.LoadScene("Game-Over"); 
        IntancePlayer.CloseInstance(); 
        Destroy(gameObject); 
    }

    public void AddLife(int amount)
    {
        Live += amount;                    
        live.text = "Vidas: " + Live;      
        Debug.Log("Vida añadida. Total de vidas: " + Live);
    }
}

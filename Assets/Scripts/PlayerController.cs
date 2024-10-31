using System.Collections;
using TMPro;
using UnityEngine;

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

    private void Awake()
    {
        // Evita duplicar el objeto Player
        if (FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
    }

    void Start()
    {
        // Carga la vida inicial del jugador desde InstancePlayer
        live.text = "Vidas: " + Live;    // Mostrar la cantidad inicial de vidas
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 0;

        // Obtén el SpriteRenderer y almacena el color original
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Movimiento del jugador
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rd.velocity = new Vector2(horizontal * speed, vertical * speed);

        // Control de animaciones basadas en el movimiento
        if (horizontal > 0)
        {
            GetComponent<Animator>().Play("right");
        }
        else if (horizontal < 0)
        {
            GetComponent<Animator>().Play("left");
        }
        else if (vertical > 0)
        {
            GetComponent<Animator>().Play("Up");
        }
        else if (vertical < 0)
        {
            GetComponent<Animator>().Play("Down");
        }
    }

    // Método para detección de colisión
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isImmune)
        {
            TakeDamage(1);
        }
    
        
    }

    // Método para reducir vida e iniciar inmunidad
    private void TakeDamage(int damage)
    {
        Live -= damage;                    // Reduce vida
        live.text = "Vidas: " + Live;   // Actualiza el texto de la vida

        if (Live <= 0)
        {
            Debug.Log("Jugador ha muerto");
            GetComponent<Animator>().Play("Deat");  // Ejecuta la animación "Deat"
            // Aquí podrías añadir lógica de Game Over, como desactivar el control del jugador
            this.enabled = false;
        }
        else
        {
            // Actualiza el tiempo del último daño recibido
            lastDamageTime = Time.time;

            // Si hay una inmunidad en curso, detenemos la corrutina actual antes de iniciar una nueva
            if (immunityCoroutine != null)
            {
                StopCoroutine(immunityCoroutine);
            }
            immunityCoroutine = StartCoroutine(ActivateImmunity());  // Inicia la inmunidad temporal
        }
    }

    // Corrutina para activar inmunidad por un tiempo
    private IEnumerator ActivateImmunity()
    {
        isImmune = true;                        // Activa inmunidad
        spriteRenderer.color = Color.red;       // Cambia el color a rojo para indicar daño
        yield return new WaitForSeconds(immuneTime);  // Espera el tiempo de inmunidad
        spriteRenderer.color = originalColor;   // Restaura el color original
        isImmune = false;                       // Desactiva inmunidad
        immunityCoroutine = null;               // Limpia la referencia de la corrutina
    }
     public void AddLife(int amount)
    {
        Live += amount;                    // Añade vida
        live.text = "Vidas: " + Live;       // Actualiza el texto de vida
        Debug.Log("Vida añadida. Total de vidas: " + Live);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    public int Live = 5;               // Vidas del jugador
    public TMP_Text live;               // Texto de UI para mostrar vidas
    private bool isImmune = false;      // Estado de inmunidad temporal
    public float immuneTime = 2.0f;     // Tiempo de inmunidad en segundos

    Rigidbody2D rd;

    void Start()
    {
        live.text = "Vidas: " + Live;   // Mostrar la cantidad inicial de vidas
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 0;
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
        if (collision.gameObject.CompareTag("Enemy") && !isImmune)  // Verifica si colisiona con "Enemy" y no está inmune
        {
            TakeDamage(1);       // Llamamos a TakeDamage con la cantidad de vida a restar
        }
    }

    // Método para reducir vida e iniciar inmunidad
    private void TakeDamage(int damage)
    {
        Live -= damage;                    // Reduce vida
        live.text = "Vidas: " + Live;      // Actualiza el texto de la vida

        if (Live <= 0)
        {
            Debug.Log("Jugador ha muerto");
            // Aquí podrías añadir lógica de Game Over
        }
        else
        {
            StartCoroutine(ActivateImmunity());  // Inicia la inmunidad temporal
        }
    }

    // Corrutina para activar inmunidad por un tiempo
    private IEnumerator ActivateImmunity()
    {
        isImmune = true;                 // Activa inmunidad
        yield return new WaitForSeconds(immuneTime);  // Espera el tiempo de inmunidad
        isImmune = false;                // Desactiva inmunidad
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float range;
    private Transform target;
    private float minDistance = 5.0f;
    private float speed = 2.0f;

    private Rigidbody2D rb;  // Guardar referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializar el Rigidbody2D

        // Buscar automáticamente el objeto con la etiqueta "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con la etiqueta 'Player'");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            range = Vector2.Distance(transform.position, target.position);

            // Solo sigue al jugador si está dentro de la distancia mínima
            if (range < minDistance)
            {
                MoveTowardsTarget();
            }
        }
    }

    void MoveTowardsTarget()
    {
        // Calcular la dirección hacia el objetivo sin modificar la rotación
        Vector3 direction = (target.position - transform.position).normalized;

        // Mover el enemigo hacia el jugador sin rotar la imagen
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Al colisionar con el jugador, no se aplica retroceso ni se detiene el movimiento
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada, pero sin retroceso.");
        }
    }
}

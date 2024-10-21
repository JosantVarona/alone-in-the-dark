using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float range;
    public Transform target;
    private float minDistance = 5.0f;
    private bool targetCollision = false;
    private float speed = 2.0f;
    private float thrust = 2.0f;

    private Rigidbody2D rb;  // Guardar referencia al Rigidbody2D
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializar el Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        range = Vector2.Distance(transform.position, target.position);
        
        if (range < minDistance && !targetCollision)  // Solo sigue si no está en colisión
        {
            MoveTowardsTarget();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;

            // Determinar la dirección de retroceso
            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            // Aplicar fuerza de retroceso en función de la dirección de la colisión
            if (right) rb.AddForce(transform.right * thrust, ForceMode2D.Impulse);
            if (left) rb.AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            if (top) rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            if (bottom) rb.AddForce(-transform.up * thrust, ForceMode2D.Impulse);

            // Invocar la función para permitir que el enemigo vuelva a seguir al jugador
            Invoke("FalseCollision", 0.5f); // Puedes ajustar el tiempo si lo consideras necesario
        }
    }

    void FalseCollision()
    {
        targetCollision = false; // Permitir que el enemigo vuelva a seguir al jugador
        rb.velocity = Vector2.zero; // Detener cualquier movimiento residual después del retroceso
    }
}

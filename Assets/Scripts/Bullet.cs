using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // Destruye la bala después de 1 segundo de haber sido creada
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si la bala impacta con un enemigo o una pared
        if (collision.CompareTag("Enemy"))
        {
            // Destruye al enemigo
            Destroy(collision.gameObject);

            // Destruye la bala inmediatamente
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            // Destruye la bala inmediatamente al chocar con la pared
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si la bala impacta con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            // Destruye al enemigo
            Destroy(collision.gameObject);

            // Destruye la bala
            Destroy(gameObject);
        }
    }
}

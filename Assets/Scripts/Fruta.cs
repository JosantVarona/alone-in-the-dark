using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
     public int lifeBonus = 1; // Número de vidas que añade este coleccionable

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Intenta encontrar el componente `Player` para añadir una vida
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddLife(lifeBonus); // Añade una vida al jugador
            }

            // Destruye el objeto coleccionable
            Destroy(gameObject);
        }
    }
}

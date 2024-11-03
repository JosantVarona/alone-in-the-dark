using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerToNextScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cierra la instancia de IntancePlayer y destruye al jugador antes de cargar la nueva escena
            IntancePlayer.CloseInstance();  // Cierra el singleton
            Destroy(other.gameObject);      // Destruye el objeto jugador

            // Cargar la escena de Game-Win
            SceneManager.LoadScene("Game-Win");
            SceneManager.sceneLoaded += OnSceneLoaded; // Agrega el evento para reposicionar al jugador si se vuelve a crear
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encuentra el punto de aparición en la nueva escena
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            // Mueve al jugador a la posición del punto de aparición
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
        // Desuscribe el evento para evitar que se llame múltiples veces
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

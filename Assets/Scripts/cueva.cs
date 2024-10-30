using UnityEngine;
using UnityEngine.SceneManagement;

public class cueva : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.sceneLoaded += OnSceneLoaded; // Agrega el evento para reposicionar al jugador
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
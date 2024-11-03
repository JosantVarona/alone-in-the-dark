using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    private float delayTime = 5f; // Tiempo de espera en segundos
    public GameObject playerPrefab; // Prefab del jugador para crear uno nuevo si es necesario
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Si el jugador no existe en la escena, crea uno nuevo a partir del prefab
        if (player == null && playerPrefab != null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<Player>().ResetPlayer(); // Resetea el jugador
        }

        // Comienza la corrutina para esperar unos segundos y luego cambiar la escena
        StartCoroutine(WaitAndLoadMapaPrincipal());
    }

    private void Update()
    {
        // Si se hace clic, pasa inmediatamente a MapaPrincipal
        if (Input.GetMouseButtonDown(0))
        {
            LoadMapaPrincipal();
        }
    }

    private IEnumerator WaitAndLoadMapaPrincipal()
    {
        yield return new WaitForSeconds(delayTime);
        LoadMapaPrincipal();
    }

    private void LoadMapaPrincipal()
    {
        // Verifica si el jugador existe y reinicia sus vidas o carga el prefab
        if (player != null)
        {
            player.GetComponent<Player>().ResetPlayer(); // Restablece el jugador
        }
        else if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<Player>().ResetPlayer();
        }
        
        SceneManager.LoadScene("MapaPrincipal");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject player;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró el objeto del jugador. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
        }
    }

    public void PlayAgain()
    {
        Debug.Log("Intentando volver a jugar...");
        if (player != null) // Verifica si el jugador no es nulo
        {
            player.GetComponent<Player>().ResetPlayer(); // Restablece el jugador
        }
        StartCoroutine(LoadMapaPrincipal());
    }

    private IEnumerator LoadMapaPrincipal()
    {
        Debug.Log("Cargando la escena MapaPrincipal...");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MapaPrincipal");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Escena MapaPrincipal cargada.");
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu-Principal"); // Asegúrate de que el nombre coincide con tu escena de menú
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para salir del editor
        #endif
    }
}

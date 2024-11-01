using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinController : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu-Principal"); // Cargar la escena del menú principal
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para salir del editor
        #endif
    }
}

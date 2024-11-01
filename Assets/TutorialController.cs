using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    private float delayTime = 5f; // Tiempo de espera en segundos

    private void Start()
    {
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
        SceneManager.LoadScene("MapaPrincipal");
    }
}

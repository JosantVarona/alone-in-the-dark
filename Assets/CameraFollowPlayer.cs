using UnityEngine;
using Cinemachine;

public class CameraFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        // Obtén la referencia de la Cinemachine Virtual Camera
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Llama a la función para buscar y asignar el jugador
        AssignPlayerAsTarget();
    }

    void AssignPlayerAsTarget()
    {
        // Busca el objeto con la etiqueta "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            //Debug.Log("hola");
            // Asigna el transform del jugador al campo Follow de la cámara
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con la etiqueta 'Player'.");
        }
    }

    /*Esto da fallo
    void OnEnable()
    {
        // Suscribirse al evento de escena cargada para reasignar el objetivo
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desuscribirse del evento cuando el script se deshabilita
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Llama de nuevo a AssignPlayerAsTarget al cargar una nueva escena
        AssignPlayerAsTarget();
    }*/
}

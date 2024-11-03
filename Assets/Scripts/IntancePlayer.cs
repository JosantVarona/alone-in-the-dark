using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntancePlayer : MonoBehaviour
{
   public static IntancePlayer instance;

    public int initialLives = 5; // Vida inicial del jugador, asignada desde el singleton

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    // Método estático para cerrar y destruir la instancia del singleton
    public static void CloseInstance()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject); // Destruye el objeto singleton en la escena
            instance = null; // Libera la referencia del singleton
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntancePlayer : MonoBehaviour
{
   public static IntancePlayer instance;
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
}

<<<<<<< HEAD
﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PinchosAsesinos : MonoBehaviour
=======
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinchosLetales : MonoBehaviour
>>>>>>> origin/Santiago
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
<<<<<<< HEAD
            Debug.Log("¡Ay no! El jugador tocó los pinchos 😱 Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
=======
            // Aqu� puedes hacer una animaci�n de muerte, sonido, etc.
            Debug.Log("�Jugador muerto por pinchos!");

           
>>>>>>> origin/Santiago
        }
    }
}

<<<<<<< HEAD
ï»¿using UnityEngine;
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
            Debug.Log("Â¡Ay no! El jugador tocÃ³ los pinchos ðŸ˜± Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
=======
            // Aquí puedes hacer una animación de muerte, sonido, etc.
            Debug.Log("¡Jugador muerto por pinchos!");

           
>>>>>>> origin/Santiago
        }
    }
}

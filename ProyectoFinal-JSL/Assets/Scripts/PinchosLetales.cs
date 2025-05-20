using UnityEngine;
using UnityEngine.SceneManagement;

public class PinchosLetales : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes hacer una animación de muerte, sonido, etc.
            Debug.Log("¡Jugador muerto por pinchos!");

            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase <c>Llave</c> que permite cambiar de escena cuando el jugador la recoge.
/// </summary>
public class Llave : MonoBehaviour
{
    /// <summary>
    /// Se ejecuta cuando un objeto colisiona con la llave. 
    /// Si es el jugador, se carga la siguiente escena.
    /// </summary>
    /// <param name="collision">Colision detectada.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Verifica si hay una siguiente escena antes de cargarla
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("Ultima escena alcanzada.");
            }
        }
    }
}

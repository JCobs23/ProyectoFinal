using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que representa una trampa de picos que reinicia la escena al colisionar con el jugador.
/// </summary>
public class PinchosLetales : MonoBehaviour
{
    /// <summary>
    /// Metodo ejecutado cuando otro collider entra en contacto con la trampa.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con la trampa.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Te empalaste! Reiniciando escena...");

            // Reinicia la escena actual
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
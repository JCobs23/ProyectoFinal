using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que representa una llave que carga una escena siguiente al colisionar con el jugador.
/// </summary>
public class Llave : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena a cargar tras recoger la llave, asignable desde el Inspector.
    /// </summary>
    [SerializeField] private string escenaSiguiente;

    /// <summary>
    /// Metodo ejecutado cuando otro collider entra en contacto con la llave.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con la llave.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(escenaSiguiente))
            {
                SceneManager.LoadScene(escenaSiguiente);
            }
            else
            {
                Debug.LogWarning("No se ha asignado una escena siguiente.");
            }
        }
    }
}
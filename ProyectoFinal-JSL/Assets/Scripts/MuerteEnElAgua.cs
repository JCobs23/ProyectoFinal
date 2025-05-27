using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que reinicia la escena actual cuando el jugador entra en contacto con el agua.
/// </summary>
public class MuerteEnElAgua : MonoBehaviour
{
    /// <summary>
    /// Metodo ejecutado cuando otro collider entra en contacto con el agua.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con el agua.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador cayo al agua y murio");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
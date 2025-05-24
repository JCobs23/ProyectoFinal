using UnityEngine;
using UnityEngine.SceneManagement;

public class Llave : MonoBehaviour
{
    [SerializeField] private string escenaSiguiente; // Asignable desde el Inspector

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

using UnityEngine;
using UnityEngine.SceneManagement;

public class LlaveNivel : MonoBehaviour
{
    [SerializeField] private string nombreEscenaSiguiente;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}

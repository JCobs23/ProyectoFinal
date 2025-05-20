using UnityEngine;
using UnityEngine.SceneManagement;

public class PinchosAsesinos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ay no! El jugador tocó los pinchos 😱 Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

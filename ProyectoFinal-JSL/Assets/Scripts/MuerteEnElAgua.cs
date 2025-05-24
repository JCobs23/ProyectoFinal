using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteEnElAgua : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🌊 El jugador cayó al agua y murió");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

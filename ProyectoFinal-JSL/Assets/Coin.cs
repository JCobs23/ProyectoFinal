using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int points = 100; // Puntuación de la moneda
    [SerializeField] public AudioClip collectSound; // Sonido al recoger
    [SerializeField] public SceneManager sceneManager; // SceneManager asignado en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Envía los puntos al SceneManager
            if (sceneManager != null)
            {
                sceneManager.CollectCoin(points);
            }
            else
            {
                Debug.LogError("SceneManager no asignado en la moneda!");
            }

            // Reproduce el sonido
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Destruye la moneda
            Destroy(gameObject);
        }
    }
}
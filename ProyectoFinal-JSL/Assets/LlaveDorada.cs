using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenKey : MonoBehaviour
{
    [SerializeField] private int requiredScore = 50; // Puntuación requerida para activar la llave
    [SerializeField] private string sceneToLoad = "SceneGame2"; // Nombre de la escena a cargar

    // Referencias a los componentes
    private Renderer keyRenderer; // Más genérico - funciona con MeshRenderer o cualquier otro Renderer
    private Collider keyCollider;

    void Start()
    {
        // Buscar componentes en este objeto o en sus hijos
        keyRenderer = GetComponentInChildren<Renderer>();
        keyCollider = GetComponentInChildren<Collider>();

        // Verificar si se encontraron los componentes necesarios
        if (keyRenderer == null)
        {
            Debug.LogError("Error: No se encontró un componente Renderer (MeshRenderer) en la llave dorada o sus hijos. Por favor, añade un MeshRenderer al objeto.");
        }

        if (keyCollider == null)
        {
            Debug.LogError("Error: No se encontró un componente Collider en la llave dorada o sus hijos. Por favor, añade un Collider al objeto y asegúrate de marcar 'Is Trigger'.");
        }

        // Desactivar la llave al inicio solo si se encontraron los componentes
        if (keyRenderer != null)
        {
            keyRenderer.enabled = false;
        }

        if (keyCollider != null)
        {
            keyCollider.enabled = false;

            // Verificar si el collider está configurado como trigger
            if (!keyCollider.isTrigger)
            {
                Debug.LogWarning("Advertencia: El Collider de la llave dorada no está configurado como 'Is Trigger'. La detección de colisiones podría no funcionar como se espera.");
            }
        }
    }

    void Update()
    {
        // Solo continuar si tenemos los componentes necesarios
        if (keyRenderer == null || keyCollider == null)
            return;

        // Verificar si se ha alcanzado la puntuación requerida
        if (GameManager.Instance != null && GameManager.Instance.score >= requiredScore)
        {
            // Activar la llave si aún no está visible
            if (!keyRenderer.enabled)
            {
                keyRenderer.enabled = true;
                keyCollider.enabled = true;
                Debug.Log($"¡Llave dorada activada! Puntuación actual: {GameManager.Instance.score}, Requerida: {requiredScore}");
            }
        }
    }

    // Este método se llama automáticamente cuando otro collider entra en contacto con este objeto
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo tocó la llave: " + other.name);

        // Verificar si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Es el jugador");

            // Verificar si GameManager existe y la puntuación es suficiente
            if (GameManager.Instance != null && GameManager.Instance.score >= requiredScore)
            {
                Debug.Log($"Puntaje correcto ({GameManager.Instance.score}). Cargando escena: {sceneToLoad}");

                // Verificar si la escena existe en build settings
                if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    Debug.LogError($"Error: La escena '{sceneToLoad}' no está en Build Settings. Asegúrate de añadirla en File > Build Settings.");
                }
            }
            else if (GameManager.Instance != null)
            {
                Debug.Log("Puntaje incorrecto: " + GameManager.Instance.score);
            }
            else
            {
                Debug.LogError("Error: No se encontró una instancia de GameManager.");
            }
        }
    }
}
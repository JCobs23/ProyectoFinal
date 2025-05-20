using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenKey : MonoBehaviour
{
    [SerializeField] private int requiredScore = 50; // Puntuaci�n requerida para activar la llave
    [SerializeField] private string sceneToLoad = "SceneGame2"; // Nombre de la escena a cargar

    // Referencias a los componentes
    private Renderer keyRenderer; // M�s gen�rico - funciona con MeshRenderer o cualquier otro Renderer
    private Collider keyCollider;

    void Start()
    {
        // Buscar componentes en este objeto o en sus hijos
        keyRenderer = GetComponentInChildren<Renderer>();
        keyCollider = GetComponentInChildren<Collider>();

        // Verificar si se encontraron los componentes necesarios
        if (keyRenderer == null)
        {
            Debug.LogError("Error: No se encontr� un componente Renderer (MeshRenderer) en la llave dorada o sus hijos. Por favor, a�ade un MeshRenderer al objeto.");
        }

        if (keyCollider == null)
        {
            Debug.LogError("Error: No se encontr� un componente Collider en la llave dorada o sus hijos. Por favor, a�ade un Collider al objeto y aseg�rate de marcar 'Is Trigger'.");
        }

        // Desactivar la llave al inicio solo si se encontraron los componentes
        if (keyRenderer != null)
        {
            keyRenderer.enabled = false;
        }

        if (keyCollider != null)
        {
            keyCollider.enabled = false;

            // Verificar si el collider est� configurado como trigger
            if (!keyCollider.isTrigger)
            {
                Debug.LogWarning("Advertencia: El Collider de la llave dorada no est� configurado como 'Is Trigger'. La detecci�n de colisiones podr�a no funcionar como se espera.");
            }
        }
    }

    void Update()
    {
        // Solo continuar si tenemos los componentes necesarios
        if (keyRenderer == null || keyCollider == null)
            return;

        // Verificar si se ha alcanzado la puntuaci�n requerida
        if (GameManager.Instance != null && GameManager.Instance.score >= requiredScore)
        {
            // Activar la llave si a�n no est� visible
            if (!keyRenderer.enabled)
            {
                keyRenderer.enabled = true;
                keyCollider.enabled = true;
                Debug.Log($"�Llave dorada activada! Puntuaci�n actual: {GameManager.Instance.score}, Requerida: {requiredScore}");
            }
        }
    }

    // Este m�todo se llama autom�ticamente cuando otro collider entra en contacto con este objeto
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo toc� la llave: " + other.name);

        // Verificar si el objeto que colision� es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Es el jugador");

            // Verificar si GameManager existe y la puntuaci�n es suficiente
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
                    Debug.LogError($"Error: La escena '{sceneToLoad}' no est� en Build Settings. Aseg�rate de a�adirla en File > Build Settings.");
                }
            }
            else if (GameManager.Instance != null)
            {
                Debug.Log("Puntaje incorrecto: " + GameManager.Instance.score);
            }
            else
            {
                Debug.LogError("Error: No se encontr� una instancia de GameManager.");
            }
        }
    }
}
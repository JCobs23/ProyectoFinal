using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldKey : MonoBehaviour
{
    [SerializeField] private int requiredScore = 50; // Puntuación requerida para activar la llave
    [SerializeField] private string sceneToLoad = "SceneGame2"; // Nombre de la escena a cargar

    private MeshRenderer meshRenderer;
    private Collider keyCollider;

    void Start()
    {
        // Obtener componentes
        meshRenderer = GetComponent<MeshRenderer>();
        keyCollider = GetComponent<Collider>(); // Funciona con cualquier tipo de collider 3D

        // Desactivar la llave al inicio
        if (meshRenderer != null && keyCollider != null)
        {
            meshRenderer.enabled = false;
            keyCollider.enabled = false;
        }
        else
        {
            Debug.LogError("No se encontró MeshRenderer o Collider en la llave dorada");
        }
    }

    void Update()
    {
        // Verificar si se ha alcanzado la puntuación requerida
        if (GameManager.Instance.score >= requiredScore)
        {
            // Activar la llave si aún no está visible
            if (meshRenderer != null && !meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
                keyCollider.enabled = true;
                Debug.Log($"¡Llave dorada activada! Puntuación actual: {GameManager.Instance.score}, Requerida: {requiredScore}");
            }
        }
    }

    // Este método se llama automáticamente cuando otro collider entra en contacto con este objeto
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Algo tocó la llave: " + collision.name);

        // Verificar si el objeto que colisionó es el jugador
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Es el jugador");

            // Verificar de nuevo la puntuación por seguridad
            if (GameManager.Instance.score >= requiredScore)
            {
                Debug.Log("Puntaje correcto. Cargando escena...");
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Puntaje incorrecto: " + GameManager.Instance.score);
            }
        }
    }
}
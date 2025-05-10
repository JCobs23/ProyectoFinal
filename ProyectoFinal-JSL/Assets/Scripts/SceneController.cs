using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldKey : MonoBehaviour
{
    [SerializeField] private int requiredScore = 50; // Puntuaci�n requerida para activar la llave
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
            Debug.LogError("No se encontr� MeshRenderer o Collider en la llave dorada");
        }
    }

    void Update()
    {
        // Verificar si se ha alcanzado la puntuaci�n requerida
        if (GameManager.Instance.score >= requiredScore)
        {
            // Activar la llave si a�n no est� visible
            if (meshRenderer != null && !meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
                keyCollider.enabled = true;
                Debug.Log($"�Llave dorada activada! Puntuaci�n actual: {GameManager.Instance.score}, Requerida: {requiredScore}");
            }
        }
    }

    // Este m�todo se llama autom�ticamente cuando otro collider entra en contacto con este objeto
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Algo toc� la llave: " + collision.name);

        // Verificar si el objeto que colision� es el jugador
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Es el jugador");

            // Verificar de nuevo la puntuaci�n por seguridad
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
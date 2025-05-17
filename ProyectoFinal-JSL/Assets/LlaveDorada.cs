using UnityEngine;
using UnityEngine.SceneManagement;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class LlaveDorada : MonoBehaviour
{
    public string sceneToLoad = "NombreDeLaEscena";

    private Renderer objectRenderer;
    private Collider collider3D;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        collider3D = GetComponent<Collider>();

        if (objectRenderer != null && collider3D != null)
        {
            objectRenderer.enabled = false;
            collider3D.enabled = false;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.Score >= 50)
        {
            if (!objectRenderer.enabled)
            {
                objectRenderer.enabled = true;
                collider3D.enabled = true;
                Debug.Log("Llave visible y activada");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.Score >= 50)
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.Log("Llave recogida, cargando escena: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("¡El nombre de la escena está vacío! Verifica en el Inspector.");
            }
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

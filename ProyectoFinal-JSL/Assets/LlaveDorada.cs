using UnityEngine;
using UnityEngine.SceneManagement;

public class LlaveDorada : MonoBehaviour
{
    [Header("Nombre exacto de la escena a cargar")]
    public string sceneToLoad = "NombreDeLaEscena";

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer != null && boxCollider != null)
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

    void Update()
    {
        if (GameManager.Instance.Score >= 50)
        {
            if (!spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
                boxCollider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.Score >= 50)
            {
                if (!string.IsNullOrEmpty(sceneToLoad))
                {
                    Debug.Log("Cargando escena: " + sceneToLoad);
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    Debug.LogWarning("¡El nombre de la escena está vacío! Verifica en el Inspector.");
                }
            }
        }
    }
}

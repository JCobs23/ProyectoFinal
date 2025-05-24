using UnityEngine;

/// <summary>
/// Clase <c>SceneController</c> que gestiona eventos y actualizaciones en la escena.
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Referencia al <c>GameManager</c> para gestionar la puntuacion y el daño del jugador.
    /// </summary>
    private GameManager gameManager;

    /// <summary>
    /// Inicializa la referencia al <c>GameManager</c>.
    /// </summary>
    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado!");
        }
    }

    /// <summary>
    /// Añade puntos a la puntuacion del jugador al recolectar una moneda.
    /// </summary>
    /// <param name="points">Cantidad de puntos a añadir.</param>
    public void CollectCoin(int points)
    {
        gameManager.AddScore(points);
    }

    /// <summary>
    /// Aplica daño al jugador reduciendo su vida.
    /// </summary>
    public void ApplyDamage()
    {
        gameManager.TakeDamage();
    }
}

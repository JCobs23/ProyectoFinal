using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase <c>GameManager</c> que gestiona la logica del juego, incluyendo la puntuacion, la vida y los eventos.
/// Implementa un patron singleton para mantener su instancia unica.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia unica de <c>GameManager</c>.
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Puntuacion persistente del jugador.
    /// </summary>
    public int score = 0;

    /// <summary>
    /// Vida actual del jugador.
    /// </summary>
    public int playerHealth = 2;

    /// <summary>
    /// Vida maxima del jugador.
    /// </summary>
    public int maxHealth = 3;

    /// <summary>
    /// Contadores de distintos objetos recolectables.
    /// </summary>
    public int spiralCount = 0;
    public int diamondoCount = 0;
    public int cubieCount = 0;
    public int hexagonCount = 0;
    public int sphereGemCount = 0;
    public int heartCount = 0;
    public int thunderCount = 0;
    public int jumpboostCount = 0;

    /// <summary>
    /// Tiempo acumulado durante la partida.
    /// </summary>
    private float tiempoAcumulado = 0f;
    public float TiempoAcumulado { get => tiempoAcumulado; set => tiempoAcumulado = value; }

    /// <summary>
    /// Puntuacion del juego.
    /// </summary>
    public int Score { get; internal set; }

    /// <summary>
    /// Evento que se activa cuando la salud del jugador cambia.
    /// </summary>
    public delegate void HealthChangedHandler(int newHealth);
    public event HealthChangedHandler OnHealthChanged;

    /// <summary>
    /// Incrementa el contador de objetos recolectados.
    /// </summary>
    public void AddSpiral() => spiralCount++;
    public void AddDiamondo() => diamondoCount++;
    public void AddCubie() => cubieCount++;
    public void AddHexagon() => hexagonCount++;
    public void AddSphereGem() => sphereGemCount++;
    public void AddHeart() => heartCount++;
    public void AddThunder() => thunderCount++;
    public void AddJumpBoost() => jumpboostCount++;

    /// <summary>
    /// Inicializa el GameManager como singleton.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager inicializado como singleton");
        }
        else
        {
            Debug.Log("Ya existe una instancia de GameManager, destruyendo duplicado");
            Destroy(gameObject);
        }
        Debug.Log("GameManager inicializado. Contadores actuales:");
        Debug.Log($"Spiral: {spiralCount}, Diamondo: {diamondoCount}, Cubie: {cubieCount}, Hexagon: {hexagonCount}, SphereGem: {sphereGemCount}, Thunder: {thunderCount}, JumpBoost: {jumpboostCount}, Heart: {heartCount}");

    }

    /// <summary>
    /// Añade puntos a la puntuacion.
    /// </summary>
    /// <param name="points">Cantidad de puntos a añadir.</param>
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Puntuacion actualizada: {score}");
    }

    /// <summary>
    /// Obtiene la puntuacion actual.
    /// </summary>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Añade salud al jugador.
    /// </summary>
    /// <param name="healthPoints">Cantidad de puntos de salud a añadir.</param>
    public void AddHealth(int healthPoints)
    {
        Debug.Log($"AddHealth: Añadiendo {healthPoints} puntos de vida. Antes: {playerHealth}/{maxHealth}");

        if (healthPoints <= 0)
        {
            Debug.LogWarning($"AddHealth: Intento de añadir {healthPoints} puntos de vida, pero el valor es invalido.");
            return;
        }

        int oldHealth = playerHealth;
        playerHealth = Mathf.Min(playerHealth + healthPoints, maxHealth);

        if (oldHealth != playerHealth)
        {
            OnHealthChanged?.Invoke(playerHealth);
        }
    }

    /// <summary>
    /// Reduce la vida del jugador.
    /// </summary>
    public void TakeDamage()
    {
        int oldHealth = playerHealth;
        playerHealth--;

        if (oldHealth != playerHealth)
        {
            OnHealthChanged?.Invoke(playerHealth);
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerHealth = 2 ; // Reinicia la vida al volver a cargar la escena
            Debug.Log("Jugador ha muerto. Reiniciando escena.");

        }
    }

    /// <summary>
    /// Obtiene la salud actual del jugador.
    /// </summary>
    public int GetHealth()
    {
        return playerHealth;
    }

    /// <summary>
    /// Reinicia el tiempo acumulado.
    /// </summary>
    public void ResetTiempo()
    {
        tiempoAcumulado = 0f;
    }

    /// <summary>
    /// Devuelve la suma total de todas las gemas recolectadas.
    /// </summary>
    public int TotalGemCount()
    {
        return spiralCount + diamondoCount + cubieCount + hexagonCount + sphereGemCount +
               thunderCount + jumpboostCount + heartCount;
    }


}

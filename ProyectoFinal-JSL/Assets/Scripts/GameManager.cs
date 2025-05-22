using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score = 0; // Puntuación persistente
    public int playerHealth = 2; // Vida persistente
    public int maxHealth = 3; // Vida máxima del jugador
    public int spiralCount = 0;
    public int diamondoCount = 0;
    public int cubieCount = 0;
    public int hexagonCount = 0;
    public int sphereGemCount = 0;
    public int heartCount = 0;
    public int thunderCount = 0;
    public int jumpboostCount = 0;

    // Tiempo
    private float tiempoAcumulado = 0f;
    public float TiempoAcumulado { get => tiempoAcumulado; set => tiempoAcumulado = value; }
    public int Score { get; internal set; }

    // Evento para notificar cambios en la salud
    public delegate void HealthChangedHandler(int newHealth);
    public event HealthChangedHandler OnHealthChanged;


    public void AddSpiral() => spiralCount++;
    public void AddDiamondo() => diamondoCount++;
    public void AddCubie() => cubieCount++;
    public void AddHexagon() => hexagonCount++;
    public void AddSphereGem() => sphereGemCount++;
    public void AddHeart() => heartCount++;
    public void AddThunder() => thunderCount++;
    public void AddJumpBoost() => jumpboostCount++;






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
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Puntuación actualizada: {score}");
    }

    public int GetScore()
    {
        return score;
    }

    public void AddHealth(int healthPoints)
    {
        Debug.Log($"AddHealth: Añadiendo {healthPoints} puntos de vida. Antes: {playerHealth}/{maxHealth}");

        if (healthPoints <= 0)
        {
            Debug.LogWarning($"AddHealth: Intento de añadir {healthPoints} puntos de vida, pero el valor es inválido.");
            return;
        }

        int oldHealth = playerHealth;
        playerHealth = Mathf.Min(playerHealth + healthPoints, maxHealth);

        Debug.Log($"AddHealth: Vida actualizada: {playerHealth}/{maxHealth}");

        // Notificar cambio de salud si realmente cambió
        if (oldHealth != playerHealth)
        {
            Debug.Log($"AddHealth: Notificando cambio de salud: {oldHealth} -> {playerHealth}");
            OnHealthChanged?.Invoke(playerHealth);
        }

        Debug.Log($"AddHealth: Salud del jugador es {playerHealth}/{maxHealth} después de añadir {healthPoints} puntos de vida.");
        
        if (playerHealth >= maxHealth)
        {
            Debug.Log($"AddHealth: La vida ya está al máximo ({playerHealth}/{maxHealth}). No se puede añadir más.");
            return;
        }

        playerHealth = Mathf.Min(playerHealth + healthPoints, maxHealth);
        Debug.Log($"AddHealth: Vida actualizada: {playerHealth}/{maxHealth}");
    }

    public void TakeDamage()
    {
        Debug.Log($"TakeDamage: Quitando 1 punto de vida. Antes: {playerHealth}");

        int oldHealth = playerHealth;
        playerHealth--;

        Debug.Log($"TakeDamage: Vida del jugador actualizada: {playerHealth}");

        // Notificar cambio de salud
        if (oldHealth != playerHealth)
        {
            Debug.Log($"TakeDamage: Notificando cambio de salud: {oldHealth} -> {playerHealth}");
            OnHealthChanged?.Invoke(playerHealth);
        }

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over - El jugador ha perdido todas las vidas");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Lógica adicional para el Game Over
        }
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public void ResetTiempo()
    {
        tiempoAcumulado = 0f;
    }
}
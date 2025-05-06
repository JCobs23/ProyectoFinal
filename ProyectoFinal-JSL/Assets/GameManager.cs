using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score = 0; // Puntuación persistente
    public int playerHealth = 3; // Vida persistente

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
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

    public void TakeDamage()
    {
        playerHealth--;
        Debug.Log($"Vida del jugador: {playerHealth}");
        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
            // Logica Adicional para el Game Over
        }
    }

    public int GetHealth()
    {
        return playerHealth;
    }
}
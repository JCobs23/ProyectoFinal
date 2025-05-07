using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Texto para la puntuación
    [SerializeField] private Image[] healthImages; // Imágenes para la vida

    private GameManager gameManager;
    private int lastKnownHealth; // Para rastrear cambios en la salud
    private int lastKnownScore; // Para rastrear cambios en la puntuación

    void Start()
    {
        // Obtener la referencia al GameManager
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado!");
            return;
        }

        // Suscribirse al evento de cambio de salud
        gameManager.OnHealthChanged += OnHealthChanged;

        // Inicializar los últimos valores conocidos
        lastKnownHealth = gameManager.GetHealth();
        lastKnownScore = gameManager.GetScore();

        Debug.Log($"GameUI inicializado. Salud inicial: {lastKnownHealth}, Puntuación inicial: {lastKnownScore}");

        // Actualizar la UI inicial
        UpdateUI();
    }

    // Método llamado cuando cambia la salud
    private void OnHealthChanged(int newHealth)
    {
        Debug.Log($"GameUI: Evento de cambio de salud recibido. Nueva salud: {newHealth}");
        lastKnownHealth = newHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        // Solo verificamos cambios en la puntuación en cada frame
        // Los cambios de salud son manejados por eventos
        if (gameManager != null)
        {
            int currentScore = gameManager.GetScore();
            if (lastKnownScore != currentScore)
            {
                Debug.Log($"Cambio de puntuación detectado: {lastKnownScore} -> {currentScore}");
                lastKnownScore = currentScore;
                UpdateScoreUI();
            }
        }
    }

    // Actualiza solo la parte de puntuación de la UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = lastKnownScore.ToString();
        }
    }

    // Actualiza solo la parte de salud de la UI
    private void UpdateHealthUI()
    {
        // Actualiza las imágenes de vida
        Debug.Log($"UpdateHealthUI: Actualizando vidas. Salud actual: {lastKnownHealth}, Imágenes disponibles: {healthImages.Length}");

        // Asegurarse de que no intentemos acceder a imágenes fuera del array
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (healthImages[i] != null)
            {
                bool shouldBeEnabled = i < lastKnownHealth;
                healthImages[i].enabled = shouldBeEnabled;
                Debug.Log($"UpdateHealthUI: Imagen[{i}] - Estado: {(shouldBeEnabled ? "Activada" : "Desactivada")}");
            }
            else
            {
                Debug.LogWarning($"UpdateHealthUI: La imagen de vida en el índice {i} es null");
            }
        }
    }

    // Actualiza toda la UI
    private void UpdateUI()
    {
        UpdateScoreUI();
        UpdateHealthUI();
    }

    // Al desactivar el componente, desuscribirse de los eventos
    void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnHealthChanged -= OnHealthChanged;
            Debug.Log("GameUI: Desuscrito de eventos de GameManager");
        }
    }

    // Al destruir el componente, asegurarse de que todo esté limpio
    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnHealthChanged -= OnHealthChanged;
            Debug.Log("GameUI: Desuscrito de eventos de GameManager (OnDestroy)");
        }
    }
}
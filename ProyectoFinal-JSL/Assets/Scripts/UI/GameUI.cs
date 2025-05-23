using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase que gestiona la interfaz de usuario del juego, actualizando 
/// la puntuacion, salud y controlando la aparicion de una llave.
/// </summary>
public class GameUI : MonoBehaviour
{
    /// <summary>
    /// Referencia al componente TextMeshProUGUI para mostrar la puntuacion.
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreText;

    /// <summary>
    /// Arreglo de imagenes para representar la salud del jugador.
    /// </summary>
    [SerializeField] private Image[] healthImages;

    /// <summary>
    /// Prefab de la llave que se instanciara al alcanzar un puntaje especifico.
    /// </summary>
    [SerializeField] private GameObject llavePrefab;

    /// <summary>
    /// Indica si la llave ya ha sido instanciada.
    /// </summary>
    private bool llaveInstanciada = false;

    /// <summary>
    /// Punto de aparicion para la llave.
    /// </summary>
    private Transform spawnPoint;

    /// <summary>
    /// Referencia al GameManager para obtener datos del juego.
    /// </summary>
    private GameManager gameManager;

    /// <summary>
    /// Ultimo valor conocido de la salud del jugador.
    /// </summary>
    private int lastKnownHealth;

    /// <summary>
    /// Ultimo valor conocido de la puntuacion del jugador.
    /// </summary>
    private int lastKnownScore;

    /// <summary>
    /// Inicializa la interfaz de usuario, suscribiendose a eventos y configurando valores iniciales.
    /// </summary>
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

        // Inicializar los ultimos valores conocidos
        lastKnownHealth = gameManager.GetHealth();
        lastKnownScore = gameManager.GetScore();

        Debug.Log($"GameUI inicializado. Salud inicial: {lastKnownHealth}, Puntuacion inicial: {lastKnownScore}");

        // Actualizar la UI inicial
        UpdateUI();

        LlaveSpawner spawner = FindObjectOfType<LlaveSpawner>();
        if (spawner != null)
        {
            spawnPoint = spawner.spawnPoint;
        }
        else
        {
            Debug.LogWarning("No se encontro un LlaveSpawner en esta escena.");
        }
    }

    /// <summary>
    /// Metodo invocado cuando cambia la salud del jugador.
    /// </summary>
    /// <param name="newHealth">Nuevo valor de la salud.</param>
    private void OnHealthChanged(int newHealth)
    {
        Debug.Log($"GameUI: Evento de cambio de salud recibido. Nueva salud: {newHealth}");
        lastKnownHealth = newHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Actualiza la interfaz de usuario en cada frame, verificando cambios en la puntuacion y controlando la aparicion de la llave.
    /// </summary>
    void Update()
    {
        // Solo verificamos cambios en la puntuacion en cada frame
        // Los cambios de salud son manejados por eventos
        if (gameManager != null)
        {
            int currentScore = gameManager.GetScore();
            if (lastKnownScore != currentScore)
            {
                Debug.Log($"Cambio de puntuacion detectado: {lastKnownScore} -> {currentScore}");
                lastKnownScore = currentScore;
                UpdateScoreUI();

                // Verifica si alcanzo el puntaje para aparecer la llave
                if (!llaveInstanciada && lastKnownScore >= 100 && spawnPoint != null)
                {
                    Instantiate(llavePrefab, spawnPoint.position, Quaternion.identity);
                    llaveInstanciada = true;
                }
            }
        }
    }

    /// <summary>
    /// Actualiza el texto de la puntuacion en la interfaz de usuario.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = lastKnownScore.ToString();
        }
    }

    /// <summary>
    /// Actualiza las imagenes de salud en la interfaz de usuario segun el valor actual de salud.
    /// </summary>
    private void UpdateHealthUI()
    {
        // Actualiza las imagenes de vida
        Debug.Log($"UpdateHealthUI: Actualizando vidas. Salud actual: {lastKnownHealth}, Imagenes disponibles: {healthImages.Length}");

        // Asegurarse de que no intentemos acceder a imagenes fuera del array
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
                Debug.LogWarning($"UpdateHealthUI: La imagen de vida en el indice {i} es null");
            }
        }
    }

    /// <summary>
    /// Actualiza toda la interfaz de usuario, incluyendo puntuacion y salud.
    /// </summary>
    private void UpdateUI()
    {
        UpdateScoreUI();
        UpdateHealthUI();
    }

    /// <summary>
    /// Se desconecta el componente de los eventos del GameManager al desactivarse.
    /// </summary>
    void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnHealthChanged -= OnHealthChanged;
            Debug.Log("GameUI: Desconectado de eventos de GameManager");
        }
    }

    /// <summary>
    /// Asegura la desconexion de los eventos del GameManager al destruir el componente.
    /// </summary>
    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnHealthChanged -= OnHealthChanged;
            Debug.Log("GameUI: Desconexion de eventos de GameManager (OnDestroy)");
        }
    }
}
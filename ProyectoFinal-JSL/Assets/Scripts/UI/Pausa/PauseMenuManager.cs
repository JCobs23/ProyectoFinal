using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Clase que gestiona el menu de pausa, permitiendo pausar el juego, guardar y salir.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    /// <summary>
    /// Panel de pausa que se activa o desactiva.
    /// </summary>
    public GameObject pausePanel;

    /// <summary>
    /// Indica si el juego esta pausado.
    /// </summary>
    private bool isPaused = false;

    /// <summary>
    /// Actualiza el tiempo acumulado y detecta la tecla de pausa.
    /// </summary>
    void Update()
    {
        if (!isPaused && GameManager.Instance != null)
            GameManager.Instance.TiempoAcumulado += Time.unscaledDeltaTime;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Alterna el estado de pausa del juego, activando o desactivando el panel de pausa.
    /// </summary>
    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;

        foreach (Transform child in pausePanel.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(isPaused);
        }
    }

    /// <summary>
    /// Reanuda el juego, desactivando el panel de pausa.
    /// </summary>
    public void ContinueGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Guarda la partida y carga el menu principal.
    /// </summary>
    public void SaveAndExit()
    {
        SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-Main Menu");
    }

    /// <summary>
    /// Sale al menu principal sin guardar la partida.
    /// </summary>
    public void ExitWithoutSaving()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-Main Menu");
    }

    /// <summary>
    /// Clase que representa los datos guardados de la partida.
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        /// <summary>
        /// Nombre de la escena actual.
        /// </summary>
        public string sceneName;

        /// <summary>
        /// Posicion X del jugador.
        /// </summary>
        public float playerX;

        /// <summary>
        /// Posicion Y del jugador.
        /// </summary>
        public float playerY;

        /// <summary>
        /// Posicion Z del jugador.
        /// </summary>
        public float playerZ;

        /// <summary>
        /// Total de gemas recolectadas.
        /// </summary>
        public int totalGems;

        /// <summary>
        /// Tiempo acumulado en el juego.
        /// </summary>
        public float gameTime;
    }

    /// <summary>
    /// Guarda el estado actual del juego en un archivo JSON.
    /// </summary>
    void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || GameManager.Instance == null) return;

        Vector3 pos = player.transform.position;
        SaveData data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name,
            playerX = pos.x,
            playerY = pos.y,
            playerZ = pos.z,
            totalGems = GameManager.Instance.TotalGemCount(),
            gameTime = GameManager.Instance.TiempoAcumulado
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "savegame.json");
        File.WriteAllText(path, json);

        Debug.Log("Juego guardado en: " + path);
        Debug.Log($"Total de gemas al guardar: {GameManager.Instance.TotalGemCount()}");
    }
}

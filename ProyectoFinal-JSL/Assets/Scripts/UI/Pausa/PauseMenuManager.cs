using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Update()
    {
        if (!isPaused && GameManager.Instance != null)
            GameManager.Instance.TiempoAcumulado += Time.unscaledDeltaTime;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

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

    public void ContinueGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SaveAndExit()
    {
        SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-Main Menu");
    }

    public void ExitWithoutSaving()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-Main Menu");
    }

    [System.Serializable]
    public class SaveData
    {
        public string sceneName;
        public float playerX, playerY, playerZ;
        public int totalGems;
        public float gameTime;
    }

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

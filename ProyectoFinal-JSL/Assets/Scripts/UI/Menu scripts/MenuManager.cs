using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Botones del Menú")]
    public Button newGameButton;
    public Button continueButton;

    private void Start()
    {
        if (newGameButton != null)
            newGameButton.onClick.AddListener(StartNewGame);
        else
            Debug.LogWarning("No se asignó el botón New Game.");

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(() => StartCoroutine(LoadSavedGame()));

            // Desactivar botón si no hay archivo de guardado
            string path = Path.Combine(Application.persistentDataPath, "savegame.json");
            continueButton.interactable = File.Exists(path);
        }
        else
        {
            Debug.LogWarning("No se asignó el botón Continue.");
        }
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame ejecutado");

        // Elimina el archivo de guardado si existe
        string path = Path.Combine(Application.persistentDataPath, "savegame.json");
        if (File.Exists(path))
            File.Delete(path);

        SceneManager.LoadScene("Scenes/LVL1 - RNW");
    }

    public IEnumerator LoadSavedGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "savegame.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("No se encontró una partida guardada.");
            if (continueButton != null)
                continueButton.interactable = false;
            yield break;
        }

        string json = File.ReadAllText(path);
        PauseMenuController.SaveData data = JsonUtility.FromJson<PauseMenuController.SaveData>(json);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TiempoAcumulado = data.gameTime;
        }

        Debug.Log("Partida cargada correctamente.");
    }
}

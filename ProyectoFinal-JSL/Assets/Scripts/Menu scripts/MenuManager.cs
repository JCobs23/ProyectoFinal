using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Botones del Men�")]
    public Button newGameButton;
    public Button continueButton;

    private void Start()
    {
        if (newGameButton != null)
            newGameButton.onClick.AddListener(StartNewGame);
        else
            Debug.LogWarning("No se asign� el bot�n New Game.");

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(LoadSavedGame);

            // Desactivar bot�n si no hay partida guardada
            if (!PlayerPrefs.HasKey("SavedScene"))
                continueButton.interactable = false;
        }
        else
        {
            Debug.LogWarning("No se asign� el bot�n Continue.");
        }
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame ejecutado");

        // Borrar datos de guardado
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.Save(); // Asegura que se apliquen

        // Cargar escena inicial
        SceneManager.LoadScene("Scenes/LVL1 - RNW");
    }


    public void LoadSavedGame()
    {
        string savedScene = PlayerPrefs.GetString("SavedScene", "");

        if (!string.IsNullOrEmpty(savedScene))
        {
            Debug.Log("Cargando partida guardada: " + savedScene);
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.LogWarning("No se encontr� una partida guardada.");
            // Opcional: puedes desactivar el bot�n Continue si no hay partida guardada
            if (continueButton != null)
                continueButton.interactable = false;
        }
    }
}

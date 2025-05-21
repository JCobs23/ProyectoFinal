using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Botones del Men�")]
    public Button newGameButton;

    private void Start()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(StartNewGame);
        }
        else
        {
            Debug.LogWarning("No se asign� el bot�n New Game.");
        }
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame ejecutado");
        SceneManager.LoadScene("Scenes/LVL1 - RNW"); // Usa la ruta completa como aparece en Build Settings
    }
}
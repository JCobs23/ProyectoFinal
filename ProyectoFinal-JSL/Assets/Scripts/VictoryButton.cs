using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que maneja el boton de victoria para cargar el menu principal.
/// </summary>
public class VictoryButton : MonoBehaviour
{
    /// <summary>
    /// Carga el menu principal y restaura el estado del juego.
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Restaurar tiempo normal
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        SceneManager.LoadScene("1-Main Menu");
    }
}

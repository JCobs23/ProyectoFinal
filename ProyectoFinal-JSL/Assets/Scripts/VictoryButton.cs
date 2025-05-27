using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Restaurar tiempo normal
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        SceneManager.LoadScene("1-Main Menu"); 
    }
}

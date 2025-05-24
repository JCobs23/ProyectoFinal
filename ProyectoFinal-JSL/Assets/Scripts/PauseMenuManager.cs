using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

           
            foreach (Transform child in pausePanel.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

    void SaveGame()
    {
        // Guarda la escena actual
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);

        // Guarda la posición del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            PlayerPrefs.SetFloat("PlayerX", pos.x);
            PlayerPrefs.SetFloat("PlayerY", pos.y);
            PlayerPrefs.SetFloat("PlayerZ", pos.z);
        }

        PlayerPrefs.Save();
    }
}

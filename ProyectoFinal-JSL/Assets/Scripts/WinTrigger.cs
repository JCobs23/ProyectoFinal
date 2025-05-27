using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject victoryPanel;
    public AudioSource victoryMusic;
    public AudioSource backgroundMusic; // Música del juego que se pausará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" ¡Victoria alcanzada!");

            if (backgroundMusic != null)
                backgroundMusic.Pause();

            if (victoryPanel != null)
                victoryPanel.SetActive(true);

            if (victoryMusic != null)
                victoryMusic.Play();

            Time.timeScale = 0f;

            // Mostrar cursor del mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

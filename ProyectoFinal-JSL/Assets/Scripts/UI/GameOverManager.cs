using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public AudioClip gameOverMusic; //  Música que suena al morir
    private AudioSource audioSource;

    void Start()
    {
        gameOverPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void MostrarGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        if (gameOverMusic != null)
        {
            audioSource.Stop(); // Detiene cualquier música anterior
            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

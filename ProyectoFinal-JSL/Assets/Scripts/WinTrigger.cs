using UnityEngine;

/// <summary>
/// Clase que activa la condicion de victoria cuando el jugador entra en contacto con el trigger.
/// </summary>
public class WinTrigger : MonoBehaviour
{
    /// <summary>
    /// Panel de victoria que se activa al alcanzar la victoria.
    /// </summary>
    public GameObject victoryPanel;

    /// <summary>
    /// Fuente de audio que reproduce la musica de victoria.
    /// </summary>
    public AudioSource victoryMusic;

    /// <summary>
    /// Fuente de audio de la musica de fondo que se pausa al alcanzar la victoria.
    /// </summary>
    public AudioSource backgroundMusic;

    /// <summary>
    /// Referencia al componente Cronometro para controlar el tiempo.
    /// </summary>
    public Cronometro cronometro;

    /// <summary>
    /// Metodo ejecutado cuando otro collider entra en contacto con el trigger.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Victoria alcanzada!");

            if (backgroundMusic != null)
                backgroundMusic.Pause();

            if (victoryPanel != null)
                victoryPanel.SetActive(true);

            if (victoryMusic != null)
                victoryMusic.Play();

            if (GameManager.Instance)
            {
                GameManager.Instance.ResetearPuntuacion();
            }

            // Pausar y resetear el cronometro
            if (cronometro != null)
            {
                cronometro.DetenerTiempo();
                cronometro.ResetearTiempo();
            }
            else
            {
                Debug.LogWarning("No se asigno el Cronometro en WinTrigger.");
            }

            Time.timeScale = 0f;

            // Mostrar cursor del mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
using UnityEngine;

/// <summary>
/// Clase que permite alternar el estado de la musica (activada o silenciada) y guarda la configuracion.
/// </summary>
public class ToggleMusic : MonoBehaviour
{
    /// <summary>
    /// Componente AudioSource que controla la musica.
    /// </summary>
    public AudioSource musicSource;

    /// <summary>
    /// Indica si la musica esta silenciada.
    /// </summary>
    private bool isMuted = false;

    /// <summary>
    /// Inicializa el estado de la musica basado en la configuracion guardada.
    /// </summary>
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        ApplyState();
    }

    /// <summary>
    /// Alterna el estado de la musica entre activada y silenciada, guardando la configuracion.
    /// </summary>
    public void ToggleSound()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyState();
    }

    /// <summary>
    /// Aplica el estado actual de la musica (activada o silenciada).
    /// </summary>
    private void ApplyState()
    {
        if (musicSource != null)
        {
            if (isMuted)
                musicSource.Pause();
            else
                musicSource.Play();
        }
    }
}

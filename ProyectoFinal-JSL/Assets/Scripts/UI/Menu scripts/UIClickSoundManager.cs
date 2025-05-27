using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que agrega un sonido de clic a todos los botones activos en la escena.
/// </summary>
public class UIClickSoundManager : MonoBehaviour
{
    /// <summary>
    /// Sonido reproducido al hacer clic en un boton.
    /// </summary>
    public AudioClip clickSound;

    /// <summary>
    /// Componente AudioSource para reproducir el sonido de clic.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Configura el AudioSource y asigna el sonido de clic a todos los botones en la escena.
    /// </summary>
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Busca todos los botones activos en la escena
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayClickSound());
        }
    }

    /// <summary>
    /// Reproduce el sonido de clic.
    /// </summary>
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
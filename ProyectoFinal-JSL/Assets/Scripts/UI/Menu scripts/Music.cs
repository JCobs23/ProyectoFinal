using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Clase que gestiona el sonido de clic para botones especificados en la interfaz de usuario.
/// </summary>
public class Music : MonoBehaviour
{
    /// <summary>
    /// Sonido reproducido al hacer clic en los botones.
    /// </summary>
    public AudioClip clickSound;

    /// <summary>
    /// Componente AudioSource para reproducir sonidos.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Lista de botones que reproduciran el sonido de clic.
    /// </summary>
    [Header("Botones con sonido de clic")]
    public List<Button> botonesConSonido;

    /// <summary>
    /// Configura el AudioSource y asigna el evento de clic a los botones especificados.
    /// </summary>
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Asignar sonido solo a los botones especificados
        foreach (Button btn in botonesConSonido)
        {
            if (btn != null)
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

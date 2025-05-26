using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    [Header("Botones con sonido de clic")]
    public List<Button> botonesConSonido;

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

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}

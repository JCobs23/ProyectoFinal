using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    public AudioSource musicSource; // Arrastra aqu� tu AudioSource (m�sica de fondo)
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Image buttonImage; // Para cambiar el �cono del bot�n si quieres

    private bool isMuted = false;

    void Start()
    {
        // Restaurar estado previo si quieres usar PlayerPrefs
        if (PlayerPrefs.HasKey("MusicMuted"))
        {
            isMuted = PlayerPrefs.GetInt("MusicMuted") == 1;
            ApplyMute();
        }
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        ApplyMute();

        // Guardar estado
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyMute()
    {
        if (musicSource != null)
            musicSource.mute = isMuted;

        // Cambiar �cono del bot�n si se desea
        if (buttonImage != null && soundOnIcon != null && soundOffIcon != null)
        {
            buttonImage.sprite = isMuted ? soundOffIcon : soundOnIcon;
        }
    }
}

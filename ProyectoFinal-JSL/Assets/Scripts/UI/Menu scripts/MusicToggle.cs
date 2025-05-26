using UnityEngine;

public class ToggleMusic : MonoBehaviour
{
    public AudioSource musicSource;
    private bool isMuted = false;

    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        ApplyState();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyState();
    }

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

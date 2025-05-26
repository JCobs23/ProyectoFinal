using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;

    private const string VolumePrefKey = "MusicVolume";

    void Awake()
    {
       
        if (volumeSlider == null)
        {
            GameObject sliderObj = GameObject.Find("musicslider");
            if (sliderObj != null)
            {
                volumeSlider = sliderObj.GetComponent<Slider>();
            }
            else
            {
                Debug.LogWarning("No se encontró el slider llamado 'musicslider'");
            }
        }
    }

    void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("Slider no asignado y no se pudo encontrar automáticamente.");
            return;
        }

        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        volumeSlider.value = savedVolume;
        ApplyVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(ApplyVolume);
    }

    public void ApplyVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }

        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }
}

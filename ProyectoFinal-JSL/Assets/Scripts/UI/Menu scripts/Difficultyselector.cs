using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySelector : MonoBehaviour
{
    public Slider difficultySlider;
    public TextMeshProUGUI difficultyLabel;

    private void Start()
    {
        int savedDifficulty = PlayerPrefs.GetInt("GameDifficulty", 1);
        difficultySlider.value = savedDifficulty;
        UpdateDifficulty(savedDifficulty);
    }

    public void OnSliderChanged(float value)
    {
        int intValue = Mathf.RoundToInt(value);
        UpdateDifficulty(intValue);
        DifficultyManager.Instance.SetDifficulty(intValue); 
        PlayerPrefs.SetInt("GameDifficulty", intValue);
    }


    private void UpdateDifficulty(int index)
    {
        string difficultyName = "Normal";

        switch (index)
        {
            case 0: difficultyName = "Easy"; break;
            case 1: difficultyName = "Normal"; break;
            case 2: difficultyName = "Hard"; break;
        }

        if (difficultyLabel != null)
            difficultyLabel.text = difficultyName;
    }
}

public enum Dificultad
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

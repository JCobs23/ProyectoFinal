using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText; // Texto para la puntuación
    [SerializeField] public Image[] healthImages; // Imágenes para la vida
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado!");
        }
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (gameManager != null)
        {
            // Actualiza la puntuación
            if (scoreText != null)
            {
                scoreText.text = "" + gameManager.GetScore();
            }

            // Actualiza las imágenes de vida
            int currentHealth = gameManager.GetHealth();
            for (int i = 0; i < healthImages.Length; i++)
            {
                if (healthImages[i] != null)
                {
                    healthImages[i].enabled = i < currentHealth;
                }
            }
        }
    }
}
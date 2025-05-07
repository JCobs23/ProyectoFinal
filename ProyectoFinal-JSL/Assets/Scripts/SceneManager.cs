using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado!");
        }
    }

    public void CollectCoin(int points)
    {
        gameManager.AddScore(points);
    }

    public void ApplyDamage()
    {
        gameManager.TakeDamage();
    }
}
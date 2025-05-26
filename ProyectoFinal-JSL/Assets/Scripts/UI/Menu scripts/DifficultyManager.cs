using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public Dificultad dificultadActual = Dificultad.Normal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Leer dificultad guardada
            int valorGuardado = PlayerPrefs.GetInt("GameDifficulty", 1);
            dificultadActual = (Dificultad)valorGuardado;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(int valor)
    {
        dificultadActual = (Dificultad)valor;
        PlayerPrefs.SetInt("GameDifficulty", valor);
        PlayerPrefs.Save();
    }

    public Dificultad GetDifficulty()
    {
        return dificultadActual;
    }
}

using UnityEngine;

/// <summary>
/// Clase que carga la posicion del jugador desde los datos guardados en PlayerPrefs.
/// </summary>
public class LoadPlayerPosition : MonoBehaviour
{
    /// <summary>
    /// Carga la posicion guardada del jugador al iniciar.
    /// </summary>
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerX") &&
            PlayerPrefs.HasKey("PlayerY") &&
            PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            transform.position = new Vector3(x, y, z);
            Debug.Log("Posicion cargada del guardado.");
        }
        else
        {
            Debug.Log("No hay datos guardados de posicion.");
        }
    }
}

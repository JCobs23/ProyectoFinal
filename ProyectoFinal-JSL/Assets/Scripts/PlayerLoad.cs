using UnityEngine;

public class LoadPlayerPosition : MonoBehaviour
{
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
            Debug.Log("Posición cargada del guardado.");
        }
        else
        {
            Debug.Log("No hay datos guardados de posición.");
        }
    }
}

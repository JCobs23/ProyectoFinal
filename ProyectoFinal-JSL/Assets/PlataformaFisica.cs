using UnityEngine;

public class PlataformaFisica : MonoBehaviour
{
    public Transform contenedor; // Referencia al PlataformaContenedor

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con: " + collision.gameObject.name + ", asignando como hijo de: " + contenedor.name);
            // Hacer que el personaje sea hijo del contenedor, preservando su transformación local
            collision.transform.SetParent(contenedor, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Salió de colisión: " + collision.gameObject.name + ", quitando como hijo");
            // Quitar al personaje como hijo
            collision.transform.SetParent(null);
        }
    }
}
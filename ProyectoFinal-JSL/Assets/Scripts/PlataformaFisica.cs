using UnityEngine;

public class PlataformaFisica : MonoBehaviour
{
    public Transform contenedor; // Referencia al PlataformaContenedor

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hacer que el personaje sea hijo del contenedor
            collision.transform.SetParent(contenedor);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Quitar al personaje como hijo
            collision.transform.SetParent(null);
        }
    }
}
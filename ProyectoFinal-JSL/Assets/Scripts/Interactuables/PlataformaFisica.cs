using UnityEngine;

/// <summary>
/// Clase <c>PlataformaFisica</c> que gestiona la interaccion entre un jugador y una plataforma en Unity.
/// </summary>
public class PlataformaFisica : MonoBehaviour
{
    /// <summary>
    /// Referencia al contenedor de la plataforma.
    /// </summary>
    public Transform contenedor;

    /// <summary>
    /// Se ejecuta cuando un objeto colisiona con la plataforma. 
    /// Si es el jugador, se establece como hijo del contenedor.
    /// </summary>
    /// <param name="collision">Colision detectada.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hacer que el personaje sea hijo del contenedor
            collision.transform.SetParent(contenedor);
        }
    }

    /// <summary>
    /// Se ejecuta cuando un objeto deja de colisionar con la plataforma. 
    /// Si es el jugador, se elimina como hijo del contenedor.
    /// </summary>
    /// <param name="collision">Colision detectada.</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Quitar al personaje como hijo
            collision.transform.SetParent(null);
        }
    }
}

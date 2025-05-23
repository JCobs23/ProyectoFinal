using UnityEngine;

/// <summary>
/// Clase <c>LlaveSpawner</c> que gestiona la creacion de una llave en un punto de inicio.
/// </summary>
public class LlaveSpawner : MonoBehaviour
{
    /// <summary>
    /// Punto donde se generara la llave.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Metodo <c>Reset</c> que inicializa el punto de spawn al transform actual.
    /// </summary>
    private void Reset()
    {
        spawnPoint = transform; // Para que se use la misma posicion del objeto
    }
}

using UnityEngine;

/// <summary>
/// Clase <c>PlataformaContainer</c> que gestiona el movimiento de una plataforma entre dos puntos.
/// </summary>
public class PlataformaContainer : MonoBehaviour
{
    /// <summary>
    /// Punto inicial del movimiento de la plataforma.
    /// </summary>
    public Transform puntoA;

    /// <summary>
    /// Punto final del movimiento de la plataforma.
    /// </summary>
    public Transform puntoB;

    /// <summary>
    /// Velocidad de desplazamiento de la plataforma.
    /// </summary>
    public float velocidad = 2f;

    /// <summary>
    /// Punto objetivo actual hacia el que se mueve la plataforma.
    /// </summary>
    private Vector3 objetivo;

    /// <summary>
    /// Inicializa la plataforma definiendo el punto objetivo como <c>puntoB</c>.
    /// </summary>
    void Start()
    {
        objetivo = puntoB.position;
    }

    /// <summary>
    /// Se ejecuta en cada fotograma para mover la plataforma.
    /// </summary>
    void Update()
    {
        MoverPlataforma();
    }

    /// <summary>
    /// Mueve la plataforma entre <c>puntoA</c> y <c>puntoB</c>.
    /// </summary>
    void MoverPlataforma()
    {
        Vector3 posicionAnterior = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

        // Si la plataforma esta lo suficientemente cerca del objetivo, cambia al otro punto.
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }

        // Opcional: imprimir en consola el movimiento de la plataforma.
        // if (posicionAnterior != transform.position)
        // {
        //     Debug.Log("PlataformaContenedor moviendose a: " + transform.position);
        // }
    }

    /// <summary>
    /// Detecta cuando un objeto entra en el area de la plataforma y lo vincula a ella.
    /// </summary>
    /// <param name="other">Objeto que entra en la plataforma.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    /// <summary>
    /// Detecta cuando un objeto sale del area de la plataforma y lo desvincula de ella.
    /// </summary>
    /// <param name="other">Objeto que sale de la plataforma.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}

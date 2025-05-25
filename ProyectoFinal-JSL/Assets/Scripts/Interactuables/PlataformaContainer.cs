using UnityEngine;

/// <summary>
/// Clase <c>PlataformaContainer</c> que mueve una plataforma entre dos puntos y arrastra al jugador sin que caiga.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class PlataformaContainer : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Vector3 objetivo;
    private Vector3 ultimaPosicion;

    /// <summary>
    /// Jugador que está actualmente en la plataforma.
    /// </summary>
    private Transform jugadorSobrePlataforma;

    void Start()
    {
        objetivo = puntoB.position;
        ultimaPosicion = transform.position;

        // Asegura que el BoxCollider sea físico (no trigger)
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = false;

        // Asegura que el Rigidbody esté como kinematic
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Update()
    {
        MoverPlataforma();
    }

    void MoverPlataforma()
    {
        // Guarda la posición actual antes de mover
        Vector3 posicionAnterior = transform.position;

        // Mueve la plataforma
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

        // Calcula cuánto se movió esta frame
        Vector3 deltaMovimiento = transform.position - posicionAnterior;

        // Si hay un jugador encima, muévelo manualmente
        if (jugadorSobrePlataforma != null)
        {
            jugadorSobrePlataforma.position += deltaMovimiento;
        }

        // Cambia de dirección si llegó al objetivo
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorSobrePlataforma = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorSobrePlataforma = null;
        }
    }
}

using UnityEngine;

public class PlataformaContainer : MonoBehaviour
{
    public Transform puntoA; // Punto inicial
    public Transform puntoB; // Punto final
    public float velocidad = 2f; // Velocidad del movimiento
    private Vector3 objetivo; // Punto objetivo actual

    void Start()
    {
        objetivo = puntoB.position; // Comienza moviéndose hacia puntoB
    }

    void Update()
    {
        MoverPlataforma();
    }

    void MoverPlataforma()
    {
        Vector3 posicionAnterior = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }
        //if (posicionAnterior != transform.position)
        //{
        //    Debug.Log("PlataformaContenedor moviéndose a: " + transform.position);
        //}
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

}
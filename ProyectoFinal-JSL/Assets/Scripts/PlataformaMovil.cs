using UnityEngine;

public class PlataformaMovil : MonoBehaviour
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
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }
}

//    public Transform startPos, endPos; // Posiciones inicial y final
//    public float speed = 2f; // Velocidad del movimiento
//    private float startTime; // Tiempo de inicio del movimiento
//    private float journeyLength; // Distancia total entre los puntos
//    private bool movingToEnd = true; // Controla la dirección (hacia endPos o startPos)

//    private void Start()
//    {
//        // Calculamos la distancia inicial
//        CalculateDist();
//        startTime = Time.time;
//    }

//    private void FixedUpdate()
//    {
//        // Calculamos la fracción del viaje basada en el tiempo transcurrido
//        float distCovered = (Time.time - startTime) * speed;
//        float fractionOfJourney = distCovered / journeyLength;

//        // Si la fracción es mayor o igual a 1, cambiamos de dirección
//        if (fractionOfJourney >= 1f)
//        {
//            fractionOfJourney = 1f; // Limitamos la fracción a 1
//            movingToEnd = !movingToEnd; // Cambiamos la dirección
//            startTime = Time.time; // Reiniciamos el cronómetro
//            distCovered = 0f; // Reiniciamos la distancia cubierta
//        }

//        // Interpolamos la posición entre startPos y endPos según la dirección
//        if (movingToEnd)
//        {
//            transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);
//        }
//        else
//        {
//            transform.position = Vector3.Lerp(endPos.position, startPos.position, fractionOfJourney);
//        }
//    }

//    private void CalculateDist()
//    {
//        journeyLength = Vector3.Distance(startPos.position, endPos.position);
//    }

//    // Detecta cuando un objeto entra en colisión con la plataforma
//    private void OnCollisionEnter(Collision collision)
//    {
//        // Si el objeto que colisiona es el personaje (tag "Player")
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // Hacer que el personaje sea hijo de la plataforma
//            collision.transform.SetParent(transform);
//        }
//    }

//    // Detecta cuando un objeto sale de la colisión con la plataforma
//    private void OnCollisionExit(Collision collision)
//    {
//        // Si el objeto que sale es el personaje (tag "Player")
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // Quitar al personaje como hijo de la plataforma
//            collision.transform.SetParent(null);
//        }
//    }
//}




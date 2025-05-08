using UnityEngine;
//using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private int rutina;
    private float cronometro;
    public Animator animator;
    private Quaternion angulo;
    private float grado;
    public int zonaDeDeteccion = 10;
    public float zonaDeAtaque = 2;
    public float VelocidadMovimiento = 2f;
    public float velocidadAtaque = 0.5f;

    public GameObject player;
    public bool atacando;

    private float tiempoUltimoDanio; // Controlar el último momento en que se aplicó daño
    public float intervaloDanio = 1f; // Intervalo entre aplicaciones de daño (en segundos)
    public AudioClip sonidoAtaque;
    private AudioSource audioSource;

    //public NavMeshAgent navMeshAgent;
    //public float distancia_ataque;

    void Start()
    {
        animator = GetComponent<Animator>();
        tiempoUltimoDanio = -intervaloDanio;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {
        float distancia = Vector3.Distance(player.transform.position, transform.position);

        if (distancia > zonaDeDeteccion)
        {
            animator.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            // Mirar hacia el jugador
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);

            if (distancia > zonaDeAtaque)
            {
                // Correr hacia el jugador si está fuera del rango de ataque
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * VelocidadMovimiento * Time.deltaTime);

                animator.SetBool("attack", false);
                atacando = false;
            }
            else
            {
                // Dentro del rango de ataque, activar animación de morder
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                atacando = true;

                // Moverse hacia el jugador con la velocidad definida mientras ataca
                transform.Translate(Vector3.forward * velocidadAtaque * Time.deltaTime);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= tiempoUltimoDanio + intervaloDanio)
        {
            Debug.Log("Contacto con Player");
            GameManager.Instance.TakeDamage();
            tiempoUltimoDanio = Time.time;
            Debug.Log("Daño aplicado al jugador");
            if (sonidoAtaque != null)
            {
                audioSource.PlayOneShot(sonidoAtaque);
            }
        }
    }

    public void Final_Ani()
    {
        animator.SetBool("attack", false);
        atacando = false;
    }

    public void FixedUpdate()
    {
        // Método vacío, mantenido por si necesitas añadir lógica en el futuro
    }
}
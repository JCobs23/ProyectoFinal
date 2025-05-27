using UnityEngine;
//using UnityEngine.AI;

/// <summary>
/// Clase que controla el comportamiento de un enemigo en el juego, incluyendo movimiento, deteccion y ataque al jugador.
/// </summary>
public class Enemigo : MonoBehaviour
{
    /// <summary>
    /// Tipo de rutina actual del enemigo (0: inactivo, 1: rotar, 2: caminar).
    /// </summary>
    private int rutina;

    /// <summary>
    /// Temporizador para cambiar entre rutinas del enemigo.
    /// </summary>
    private float cronometro;

    /// <summary>
    /// Componente Animator para controlar las animaciones del enemigo.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Angulo hacia el cual el enemigo rotara.
    /// </summary>
    private Quaternion angulo;

    /// <summary>
    /// Grado de rotacion aleatorio para el movimiento del enemigo.
    /// </summary>
    private float grado;

    /// <summary>
    /// Distancia maxima para detectar al jugador.
    /// </summary>
    public int zonaDeDeteccion = 10;

    /// <summary>
    /// Distancia maxima para atacar al jugador.
    /// </summary>
    public float zonaDeAtaque = 2;

    /// <summary>
    /// Velocidad de movimiento del enemigo al perseguir al jugador.
    /// </summary>
    public float VelocidadMovimiento = 2f;

    /// <summary>
    /// Velocidad de movimiento del enemigo mientras ataca.
    /// </summary>
    public float velocidadAtaque = 0.5f;

    /// <summary>
    /// Referencia al objeto del jugador.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Indica si el enemigo esta atacando.
    /// </summary>
    public bool atacando;

    /// <summary>
    /// Momento en que se aplico el ultimo danio al jugador.
    /// </summary>
    private float tiempoUltimoDanio;

    /// <summary>
    /// Intervalo de tiempo entre aplicaciones de danio al jugador (en segundos).
    /// </summary>
    public float intervaloDanio = 1f;

    /// <summary>
    /// Sonido reproducido al atacar al jugador.
    /// </summary>
    public AudioClip sonidoAtaque;

    /// <summary>
    /// Componente AudioSource para reproducir sonidos.
    /// </summary>
    private AudioSource audioSource;

    //public NavMeshAgent navMeshAgent;
    //public float distancia_ataque;

    /// <summary>
    /// Inicializa los componentes del enemigo al comenzar el juego.
    /// </summary>
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

    /// <summary>
    /// Actualiza el comportamiento del enemigo en cada frame.
    /// </summary>
    void Update()
    {
        Comportamiento_Enemigo();
    }

    /// <summary>
    /// Controla el comportamiento del enemigo segun la distancia al jugador, alternando entre patrulla, persecucion y ataque.
    /// </summary>
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
                // Correr hacia el jugador si esta fuera del rango de ataque
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * VelocidadMovimiento * Time.deltaTime);

                animator.SetBool("attack", false);
                atacando = false;
            }
            else
            {
                // Dentro del rango de ataque, activar animacion de morder
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                atacando = true;

                // Moverse hacia el jugador con la velocidad definida mientras ataca
                transform.Translate(Vector3.forward * velocidadAtaque * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Maneja el contacto continuo con el jugador, aplicando danio si corresponde.
    /// </summary>
    /// <param name="collision">Informacion de la colision con el objeto.</param>
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= tiempoUltimoDanio + intervaloDanio)
        {
            Debug.Log("Contacto con Player");
            GameManager.Instance.TakeDamage();
            tiempoUltimoDanio = Time.time;
            Debug.Log("Danio aplicado al jugador");
            if (sonidoAtaque != null)
            {
                audioSource.PlayOneShot(sonidoAtaque);
            }
        }
    }

    /// <summary>
    /// Finaliza la animacion de ataque y restablece el estado de ataque.
    /// </summary>
    public void Final_Ani()
    {
        animator.SetBool("attack", false);
        atacando = false;
    }

    public void FixedUpdate()
    {
        //Metodo vacio
    }
}
using UnityEngine;
using StarterAssets;

/// <summary>
/// Clase que representa un objeto coleccionable en el juego, como monedas o power-ups.
/// </summary>
public class Coin : MonoBehaviour
{
    /// <summary>
    /// Enumeracion que define los tipos de objetos coleccionables.
    /// </summary>
    public enum ItemType
    {
        Thunder,
        JumpBoost,
        Heart,
        Spiral,
        Cubie,
        Hexagon,
        SphereGem,
        Diamondo
    }

    /// <summary>
    /// Puntuacion otorgada al recoger el objeto.
    /// </summary>
    [SerializeField] private int points = 100;

    /// <summary>
    /// Sonido reproducido al recoger el objeto.
    /// </summary>
    [SerializeField] private AudioClip collectSound;

    /// <summary>
    /// Tipo de elemento coleccionable.
    /// </summary>
    [SerializeField] private ItemType itemType;

    /// <summary>
    /// Multiplicador de velocidad aplicado al recoger un objeto Thunder (0 para sin efecto).
    /// </summary>
    [SerializeField] private float speedBoostMultiplier = 2f;

    /// <summary>
    /// Multiplicador de altura de salto aplicado al recoger un objeto JumpBoost (0 para sin efecto).
    /// </summary>
    [SerializeField] private float jumpBoostMultiplier = 2f;

    /// <summary>
    /// Multiplicador de gravedad para potencia de salto (0 para sin efecto, 1 para gravedad normal).
    /// </summary>
    [SerializeField] private float jumpPowerMultiplier = 0.5f;

    /// <summary>
    /// Puntos de vida otorgados al recoger un objeto Heart (0 para sin efecto).
    /// </summary>
    [SerializeField] private int heartPoints = 1;

    /// <summary>
    /// Referencia al CharacterController asignado en el Inspector.
    /// </summary>
    [SerializeField] private CharacterController playerController;

    /// <summary>
    /// Metodo ejecutado cuando otro collider entra en contacto con el objeto.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con el objeto.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Colision detectada con: {other.name}, Tag: {other.tag}");
            // Envia los puntos al GameManager
            var gameManager = GameManager.Instance;
            if (gameManager != null)
            {
                gameManager.AddScore(points);
            }
            else
            {
                Debug.LogError("GameManager no encontrado!");
            }

            // Aplica efectos segun el tipo de elemento
            ApplyItemEffect(other);

            // Reproduce el sonido
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Destruye el objeto
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Aplica el efecto correspondiente al tipo de objeto coleccionado.
    /// </summary>
    /// <param name="player">El collider del jugador que recoge el objeto.</param>
    private void ApplyItemEffect(Collider player)
    {
        if (playerController == null)
        {
            Debug.LogWarning("CharacterController no asignado en el Inspector para este objeto!");
            return;
        }

        var gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager no encontrado para aplicar efectos!");
            return;
        }

        switch (itemType)
        {
            case ItemType.Thunder:
                if (speedBoostMultiplier != 0)
                {
                    StartCoroutine(ApplySpeedBoost(player, 5f));
                    gameManager.AddThunder();
                }
                break;

            case ItemType.JumpBoost:
                if (jumpBoostMultiplier != 0 || jumpPowerMultiplier != 0)
                {
                    StartCoroutine(ApplyJumpBoost(player, 5f));
                    gameManager.AddJumpBoost();
                }
                break;

            case ItemType.Heart:
                if (heartPoints != 0)
                {
                    gameManager.AddHealth(heartPoints);
                    gameManager.heartCount++;
                    Debug.Log($"Aplicando Heart: {heartPoints} puntos de vida otorgados");
                }
                break;

            case ItemType.Spiral:
                gameManager.AddSpiral();
                break;

            case ItemType.Diamondo:
                gameManager.AddDiamondo();
                break;

            case ItemType.Cubie:
                gameManager.AddCubie();
                break;

            case ItemType.Hexagon:
                gameManager.AddHexagon();
                break;

            case ItemType.SphereGem:
                gameManager.AddSphereGem();
                break;
            default:
                // Otros tipos no tienen efectos especiales
                break;
        }
    }

    /// <summary>
    /// Aplica un efecto de aumento de velocidad al jugador durante un tiempo determinado.
    /// </summary>
    /// <param name="player">El collider del jugador que recibe el efecto.</param>
    /// <param name="duration">Duracion del efecto en segundos.</param>
    /// <returns>Un IEnumerator para controlar la corrutina.</returns>
    private System.Collections.IEnumerator ApplySpeedBoost(Collider player, float duration)
    {
        Debug.Log($"Iniciando SpeedBoost con multiplicador: {speedBoostMultiplier}");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Detecta la entrada del jugador para la direccion del movimiento
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

            // Aplica movimiento adicional en la direccion de entrada
            if (moveDirection != Vector3.zero)
            {
                float speed = speedBoostMultiplier * 5f; // Ajusta 5f segun la velocidad base del juego
                Vector3 move = moveDirection * speed * Time.deltaTime;
                playerController.Move(move);
                Debug.Log($"-aplicando velocidad: {move.magnitude} en frame {Time.frameCount}");
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("SpeedBoost finalizado");
    }

    /// <summary>
    /// Aplica un efecto de aumento de salto al jugador durante un tiempo determinado.
    /// </summary>
    /// <param name="player">El collider del jugador que recibe el efecto.</param>
    /// <param name="duration">Duracion del efecto en segundos.</param>
    /// <returns>Un IEnumerator para controlar la corrutina.</returns>
    private System.Collections.IEnumerator ApplyJumpBoost(Collider player, float duration)
    {
        var thirdPersonController = player.GetComponent<ThirdPersonController>();
        if (thirdPersonController == null)
        {
            Debug.LogWarning("ThirdPersonController no encontrado en el jugador!");
            yield break;
        }

        // Guarda los valores originales
        float originalJumpHeight = thirdPersonController.JumpHeight;
        float originalGravity = thirdPersonController.Gravity;
        // Aplica los aumentos
        thirdPersonController.JumpHeight *= jumpBoostMultiplier;
        thirdPersonController.Gravity *= jumpPowerMultiplier;
        Debug.Log($"Iniciando JumpBoost: JumpHeight cambiado de {originalJumpHeight} a {thirdPersonController.JumpHeight}, Gravity cambiado de {originalGravity} a {thirdPersonController.Gravity}");

        // Espera la duracion del efecto
        yield return new WaitForSeconds(duration);

        // Restaura los valores originales
        thirdPersonController.JumpHeight = originalJumpHeight;
        thirdPersonController.Gravity = originalGravity;
        Debug.Log($"JumpBoost finalizado: JumpHeight restaurado a {originalJumpHeight}, Gravity restaurado a {originalGravity}");
    }
}
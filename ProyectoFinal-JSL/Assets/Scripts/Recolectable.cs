using UnityEngine;
using StarterAssets;

public class Coin : MonoBehaviour
{
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

    [SerializeField] private int points = 100; // Puntuación del objeto
    [SerializeField] private AudioClip collectSound; // Sonido al recoger
    [SerializeField] private ItemType itemType; // Tipo de elemento
    [SerializeField] private float speedBoostMultiplier = 2f; // Multiplicador de velocidad (0 para sin efecto)
    [SerializeField] private float jumpBoostMultiplier = 2f; // Multiplicador de altura de salto (0 para sin efecto)
    [SerializeField] private float jumpPowerMultiplier = 0.5f; // Multiplicador de gravedad para potencia de salto (0 para sin efecto, 1 para gravedad normal)
    [SerializeField] private int heartPoints = 1; // Puntos de vida otorgados por Heart (0 para sin efecto)
    [SerializeField] private CharacterController playerController; // CharacterController asignado en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Colisión detectada con: {other.name}, Tag: {other.tag}");
            // Envía los puntos al GameManager
            var gameManager = GameManager.Instance;
            if (gameManager != null)
            {
                gameManager.AddScore(points);
            }
            else
            {
                Debug.LogError("GameManager no encontrado!");
            }

            // Aplica efectos según el tipo de elemento
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

    private System.Collections.IEnumerator ApplySpeedBoost(Collider player, float duration)
    {
        Debug.Log($"Iniciando SpeedBoost con multiplicador: {speedBoostMultiplier}");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Detecta la entrada del jugador para la dirección del movimiento
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

            // Aplica movimiento adicional en la dirección de entrada
            if (moveDirection != Vector3.zero)
            {
                float speed = speedBoostMultiplier * 5f; // Ajusta 5f según la velocidad base del juego
                Vector3 move = moveDirection * speed * Time.deltaTime;
                playerController.Move(move);
                Debug.Log($"Aplicando velocidad: {move.magnitude} en frame {Time.frameCount}");
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("SpeedBoost finalizado");
    }

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

        // Espera la duración del efecto
        yield return new WaitForSeconds(duration);

        // Restaura los valores originales
        thirdPersonController.JumpHeight = originalJumpHeight;
        thirdPersonController.Gravity = originalGravity;
        Debug.Log($"JumpBoost finalizado: JumpHeight restaurado a {originalJumpHeight}, Gravity restaurado a {originalGravity}");
    }
}
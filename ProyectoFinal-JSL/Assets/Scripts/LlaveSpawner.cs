using UnityEngine;

public class LlaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    private void Reset()
    {
        spawnPoint = transform; // Para que se use la misma posici�n del objeto
    }
}


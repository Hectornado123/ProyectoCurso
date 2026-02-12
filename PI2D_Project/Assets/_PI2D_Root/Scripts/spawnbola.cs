using UnityEngine;

public class BolaSpawner : MonoBehaviour
{
    public GameObject prefabBola;
    public float tiempoSpawn = 7f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBola), 0f, tiempoSpawn);
    }

    void SpawnBola()
    {
        Instantiate(prefabBola, transform.position, Quaternion.identity);
    }
}

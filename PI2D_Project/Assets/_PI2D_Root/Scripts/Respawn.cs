using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 respawnPosition;

    void Start()
    {
        // Posición inicial como primer respawn
        respawnPosition = transform.position;
    }

    public void Respaawn()
    {
        transform.position = respawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            respawnPosition = collision.transform.position;
        }
    }
}


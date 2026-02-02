using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private GameObject objectToDestroy;

    private bool used = false;

    public void Activate()
    {
        if (used) return;

        used = true;

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        Debug.Log("Interruptor activado");

        // Opcional: destruir el interruptor
        // Destroy(gameObject);
    }
}

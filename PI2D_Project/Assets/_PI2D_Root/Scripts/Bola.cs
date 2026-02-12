using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BolaRueda : MonoBehaviour
{
    public float velocidad = 5f; // velocidad hacia la izquierda

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movimiento constante hacia la izquierda
        rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);

        // Rotación visual para que parezca que rueda
        float angulo = velocidad * 360 * Time.fixedDeltaTime;
        transform.Rotate(0, 0, angulo);
    }
}

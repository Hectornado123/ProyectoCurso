

using UnityEngine;

public class Escorpion : MonoBehaviour
{
    public float speed = 2f;
    public Transform puntoA;
    public Transform puntoB;
    public float distanciaMinima = 0.05f; // tolerancia para cambiar de punto

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform objetivo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Empezar hacia B
        objetivo = puntoB;
    }

    void FixedUpdate()
    {
        if (puntoA == null || puntoB == null) return;

        // Calcula dirección
        float direccion = objetivo.position.x > transform.position.x ? 1f : -1f;

        // Mueve al escorpión
        rb.linearVelocity = new Vector2(direccion * speed, rb.linearVelocity.y);

       

        // Cambiar de objetivo si llegó al punto
        if ((direccion > 0 && transform.position.x >= objetivo.position.x - distanciaMinima) ||
            (direccion < 0 && transform.position.x <= objetivo.position.x + distanciaMinima))
        {
            objetivo = (objetivo == puntoA) ? puntoB : puntoA;
        }
    }
}
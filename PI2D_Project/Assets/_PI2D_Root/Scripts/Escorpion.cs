using UnityEngine;

public class Escorpion : MonoBehaviour
{
    public float speed = 2f;
    public Transform puntoA;
    public Transform puntoB;
    public float distanciaMinima = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform objetivo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        objetivo = puntoB;
    }

    void FixedUpdate()
    {
        if (puntoA == null || puntoB == null) return;

        // Dirección hacia el objetivo
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        // Movimiento solo en X
        rb.linearVelocity = new Vector2(direccion.x * speed, rb.linearVelocity.y);

        // Animación siempre activa
        animator.SetBool("camina", true);

        // Si llegó al punto, cambiar objetivo
        if (Vector2.Distance(transform.position, objetivo.position) <= distanciaMinima)
        {
            objetivo = (objetivo == puntoA) ? puntoB : puntoA;
        }
    }
}

using UnityEngine;

public class Arana : MonoBehaviour
{
    public float velocidad = 2f;

    private Rigidbody2D rb;
    private Vector2 direccion = Vector2.right; // Direcci�n inicial
    private bool enContacto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // La gravedad la manejamos manualmente seg�n la pared/suelo
    }

    void FixedUpdate()
    {
        // Movimiento constante
        rb.linearVelocity = direccion * velocidad;

        // Flip visual seg�n la direcci�n
        if (direccion.x != 0)
            GetComponent<SpriteRenderer>().flipX = direccion.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AjustarDireccion(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        AjustarDireccion(collision);
    }

    private void AjustarDireccion(Collision2D collision)
    {
        foreach (ContactPoint2D contacto in collision.contacts)
        {
            Vector2 normal = contacto.normal;

            // Si toca el suelo o pared, ajusta direcci�n
            if (Mathf.Abs(normal.y) > 0.9f)
            {
                // Suelo o techo
                direccion = new Vector2(direccion.x, 0);
            }
            else if (Mathf.Abs(normal.x) > 0.9f)
            {
                // Pared izquierda/derecha
                direccion = new Vector2(0, normal.y > 0 ? 1 : -1); // sube o baja por la pared
            }
        }
    }
}

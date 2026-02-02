using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 input;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [Header("Movement")]
    public float speed = 10f;
    private Vector2 moveInput;
    private bool facingRight = true;
    [SerializeField] private float velocidadEscalar;
    private BoxCollider2D boxCollider2D;
    private float gravedadInicial;
    private bool escalando;

    [Header("Jump")]
    public float jumpForce = 6f;
    private bool isGrounded;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] private float dashPower = 25f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 1f;

    // --- NUEVAS VARIABLES PARA INTERACTUAR ---
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        isDashing = false;
        escalando = false;
        gravedadInicial = rb.gravityScale;
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        Move();
        //Escalar();
    }

    // ---------------- MOVIMIENTO ----------------
    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        if (!isDashing && !escalando)
        {
            if (moveInput.x > 0 && !facingRight) Flip();
            else if (moveInput.x < 0 && facingRight) Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    // ---------------- SALTO ----------------
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }

    // ---------------- DASH ----------------
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        anim.SetBool("Dash", true);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirection = facingRight ? 1f : -1f;
        tr.emitting = true;

        float timer = 0f;
        while (timer < dashDuration)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashPower, 0f);
            timer += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = originalGravity;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        tr.emitting = false;
        anim.SetBool("Dash", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // ---------------- INTERACTUAR ----------------
    void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            interactionRange,
            interactableLayer
        );

        if (hit != null)
        {
            Switch sw = hit.GetComponent<Switch>();
            if (sw != null)
            {
                sw.Activate();
            }
        }
    }

    // ---------------- GROUND CHECK ----------------
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.25f,
            groundLayer
        );
    }

    // ---------------- INPUT SYSTEM ----------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (isGrounded) Jump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!canDash || isDashing) return;

        StartCoroutine(Dash());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Interact();
        }
    }

    // ---------------- ESCALAR (CORREGIDO) ----------------
    /*void Escalar()
    {
        bool tocandoEscalera = Physics2D.OverlapBox(
            transform.position,
            boxCollider2D.size,
            0f,
            LayerMask.GetMask("Escaleras")
        );

        if (tocandoEscalera && Mathf.Abs(moveInput.y) > 0.1f)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                moveInput.y * velocidadEscalar
            );
            escalando = true;
        }
        else if (!tocandoEscalera)
        {
            rb.gravityScale = gravedadInicial;
            escalando = false;
        }

        if (isGrounded && !tocandoEscalera)
        {
            rb.gravityScale = gravedadInicial;
            escalando = false;
        }
    }*/

    // Visualizar el rango de interacción en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
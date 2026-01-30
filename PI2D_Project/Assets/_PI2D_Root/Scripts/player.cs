using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [Header("Movement")]
    public float speed = 10f;
    private Vector2 moveInput;
    private bool facingRight = true;

    [Header("Jump")]
    public float jumpForce = 6f;
    private bool isGrounded;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] private float dashPower = 25f;
    [SerializeField] private float dashDuration = 0.25f;   // 👈 duración REAL
    [SerializeField] private float dashCooldown = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        Move();
    }

    // ---------------- MOVIMIENTO ----------------
    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        if (moveInput.x > 0 && !facingRight) Flip();
        else if (moveInput.x < 0 && facingRight) Flip();

        anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
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

    // ---------------- DASH POR DURACIÓN ----------------
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

        // FIN DEL DASH
        rb.gravityScale = originalGravity;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        tr.emitting = false;
        anim.SetBool("Dash", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // ---------------- GROUND CHECK ----------------
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.25f,
            groundLayer
        );

        anim.SetBool("Grounded", isGrounded);
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
}

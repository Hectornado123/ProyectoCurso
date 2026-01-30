using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [Header("Movement")]
    public float speed = 10f;
    private Vector2 moveInput;

    [Header("Jump")]
    public float jumpForce = 6f;
    private bool isGrounded;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 25f;
    [SerializeField] private float dashDuration = 0.25f;     // ?? 1 segundo
    [SerializeField] private float dashCooldown = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (isDashing) return; // no movimiento normal durante dash
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        // Girar personaje seg�n direcci�n
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Direcci�n a la que mira el personaje
        float dashDirection = transform.localScale.x;

        tr.emitting = true;

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashPower, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.25f,
            groundLayer
        );
    }

    #region Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (isGrounded)
        {
            Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!canDash || isDashing) return;

        StartCoroutine(Dash());
    }
    #endregion
}

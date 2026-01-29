using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Editor References")]
    private Rigidbody2D rb;

    [Header("Movement Parameters")]
    public float speed = 10f;
    private Vector2 moveInput;

    [Header("Jump Parameters")]
    public float jumpForce = 6f;
    public bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
     
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    #region Input Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("Jump pressed");

        if (isGrounded)
        {
            Jump();
        }
    }
    #endregion
}

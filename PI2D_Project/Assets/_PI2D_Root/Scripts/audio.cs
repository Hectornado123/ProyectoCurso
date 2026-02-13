using UnityEngine;

public class PlayerJumpSound2D : MonoBehaviour
{
    public AudioClip jumpSFX;
    private AudioSource audioSource;
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    public bool isGrounded;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(jumpSFX);
    }
}
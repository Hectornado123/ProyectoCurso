using UnityEngine;

public class Escorpion : MonoBehaviour
{
    public float speed = 2f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("camina", true);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}

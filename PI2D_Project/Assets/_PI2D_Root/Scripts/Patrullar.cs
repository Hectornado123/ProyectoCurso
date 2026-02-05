using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;

    private Vector3 escalaInicial;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        escalaInicial = transform.localScale; // guarda tamaño real
        ElegirNuevoPunto();
    }

    private void Girar()
    {
        if (puntosMovimiento[numeroAleatorio].position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            puntosMovimiento[numeroAleatorio].position,
            velocidadMovimiento * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            ElegirNuevoPunto();
        }
    }

    private void ElegirNuevoPunto()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        Girar();
    }
}

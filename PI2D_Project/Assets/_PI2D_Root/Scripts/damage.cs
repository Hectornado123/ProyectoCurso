using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        Debug.Log("Vida inicial: " + vidaActual);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemigo"))
        {
            QuitarVida(1);
        }

        if (other.CompareTag("filo"))
        {
            QuitarVida(3);
        }
    }

    void QuitarVida(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Daño recibido: " + cantidad + " | Vida: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("💀 PLAYER MUERTO");
        // Aquí puedes:
        // Destroy(gameObject);
        // Recargar escena
        // Animación de muerte
    }
}

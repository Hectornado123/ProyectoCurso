using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual = 3;
    private Respawn Respawn;
    void Start()
    {
        vidaActual = vidaMaxima;
        Respawn = GetComponent<Respawn>();
        Debug.Log("Vida inicial: " + vidaActual);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemigo"))
        {
            vidaActual--;
        }

      

        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("filo"))
        {
            Morir();
        }
    }
    // void QuitarVida(int cantidad)
    // {
    //      vidaActual -= cantidad;
    //  Debug.Log("Daño recibido: " + cantidad + " | Vida: " + vidaActual);


    // }

    void Morir()
    {
        Debug.Log("💀 PLAYER MUERTO");
        // Respawn
        Respawn.Respaawn();

        // Resetear vida
        vidaActual = vidaMaxima;
        // Aquí puedes:
        // Destroy(gameObject);
        // Recargar escena
        // Animación de muerte
    }
}

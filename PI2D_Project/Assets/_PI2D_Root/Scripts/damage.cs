using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual = 3;

    void Start()
    {
        vidaActual = vidaMaxima;
        Debug.Log("Vida inicial: " + vidaActual);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemigo"))
        {
            vidaActual--;
            
            
            //QuitarVida(1);
        }

        if (other.CompareTag("filo"))
        {
            vidaActual = 0;
        }

        if (vidaActual <= 0)
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
        // Aquí puedes:
        // Destroy(gameObject);
        // Recargar escena
        // Animación de muerte
    }
}

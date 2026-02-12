using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    public int valor = 1;                
    public bool destruirAlRecoger = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que toca tiene tag "Player"
        if (other.CompareTag("Player"))
        {
            // Aquí puedes poner el efecto de recoger
            Debug.Log("¡Pickup recogido! Valor: " + valor);

            // Por ejemplo, aumentar score o vida
           
        
        }
    }
}

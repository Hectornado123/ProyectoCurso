using UnityEngine;
using UnityEngine.SceneManagement;

public class Orbe : MonoBehaviour
{
    public string nombreEscena; // Nombre exacto de la escena a cargar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}

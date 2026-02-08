using UnityEngine;

public class RadarTexto : MonoBehaviour
{
    public GameObject textoCanvas; // Canvas con el texto

    private void Start()
    {
        textoCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textoCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textoCanvas.SetActive(false);
        }
    }
}

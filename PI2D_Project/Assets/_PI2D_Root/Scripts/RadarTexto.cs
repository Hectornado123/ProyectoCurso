using UnityEngine;
using UnityEngine;

public class RadarTexto : MonoBehaviour
{
    [SerializeField] private GameObject textoCanvas;

    private void Awake()
    {
        if (textoCanvas != null)
            textoCanvas.SetActive(false);
        else
            Debug.LogError("❌ TextoCanvas NO está asignado en el Inspector", this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!textoCanvas) return;

        if (other.CompareTag("Player"))
        {
            textoCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!textoCanvas) return;

        if (other.CompareTag("Player"))
        {
            textoCanvas.SetActive(false);
        }
    }
}

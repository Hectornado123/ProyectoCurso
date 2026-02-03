using System.Collections;
using UnityEngine;
using TMPro;

public class GuillotinaRotatoria : MonoBehaviour
{
    public float intervalo = 6f;
    public float anguloRotacion = 170f;
    public float velocidadRotacion = 2f;

    public TextMeshProUGUI contadorTexto;

    private float tiempoRestante;
    private Quaternion rotacionInicial;
    private bool atacando = false;

    public Transform saw; // referencia a la guillotina

    void Start()
    {
        saw = GameObject.Find("Saw").transform; // BUSCA EL OBJETO Saw
        rotacionInicial = saw.rotation;
        tiempoRestante = intervalo;
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        contadorTexto.text = tiempoRestante.ToString("F1") + "s";

        if (tiempoRestante <= 0 && !atacando)
        {
            StartCoroutine(Ataque());
            tiempoRestante = intervalo;
        }
    }

    IEnumerator Ataque()
    {
        atacando = true;

        Quaternion rotObjetivo = rotacionInicial * Quaternion.Euler(0, 0, -anguloRotacion);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * velocidadRotacion;
            saw.rotation = Quaternion.Lerp(rotacionInicial, rotObjetivo, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadRotacion;
            saw.rotation = Quaternion.Lerp(rotObjetivo, rotacionInicial, t);
            yield return null;
        }

        atacando = false;
    }
}
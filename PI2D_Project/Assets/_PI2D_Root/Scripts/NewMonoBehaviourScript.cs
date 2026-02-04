using System.Collections;
using UnityEngine;

public class SawPenduloInvertido : MonoBehaviour
{
    [Header("Tiempo")]
    public float intervalo = 6f;

    [Header("Movimiento")]
    public float anguloMaximo = 85f; // 85 + 85 = 170 total
    public float velocidad = 6f;

    [Header("Easing")]
    public AnimationCurve curva;

    private float tiempo;
    private Quaternion rotIzquierda;
    private Quaternion rotDerecha;
    private bool moviendo = false;
    private bool haciaDerecha = false; // 👈 INVERTIDO

    void Start()
    {
        tiempo = intervalo;

        // Rotaciones
        rotIzquierda = Quaternion.Euler(0, 0, anguloMaximo);
        rotDerecha = Quaternion.Euler(0, 0, -anguloMaximo);

        // Curva default si no hay
        if (curva == null || curva.length == 0)
            curva = AnimationCurve.EaseInOut(0, 0, 1, 1);

        // 🔁 EMPIEZA MIRANDO A LA DERECHA
        transform.rotation = rotDerecha;
    }

    void Update()
    {
        tiempo -= Time.deltaTime;

        if (tiempo <= 0 && !moviendo)
        {
            StartCoroutine(Oscilar());
            tiempo = intervalo;
        }
    }

    IEnumerator Oscilar()
    {
        moviendo = true;

        Quaternion desde = haciaDerecha ? rotIzquierda : rotDerecha;
        Quaternion hacia = haciaDerecha ? rotDerecha : rotIzquierda;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidad;
            float curvaValor = curva.Evaluate(t);
            transform.rotation = Quaternion.Lerp(desde, hacia, curvaValor);
            yield return null;
        }

        haciaDerecha = !haciaDerecha;
        moviendo = false;
    }
}

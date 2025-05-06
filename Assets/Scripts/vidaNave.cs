using UnityEngine;
using UnityEngine.UI;

public class vidaNave : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int saludMaxima = 100;
    public int saludActual;

    [Header("UI (opcional)")]
    public Slider barraSalud;
    public GameObject muerte;

    [Header("Invulnerabilidad")]
    public float tiempoInvulnerable = 1f;
    private bool esInvulnerable = false;

    void Start()
    {
        saludActual = saludMaxima;
        ActualizarBarraSalud();
    }

    void OnTriggerEnter(Collider other)
    {
        if (esInvulnerable) return;

        if (other.CompareTag("Proyectil"))
        {
            TomarDaño(10);
        }
        else if (other.CompareTag("Meteorito"))
        {
            TomarDaño(20);
            Destroy(other.gameObject);
        }
    }

    void TomarDaño(int cantidad)
    {
        saludActual -= cantidad;
        saludActual = Mathf.Clamp(saludActual, 0, saludMaxima);
        ActualizarBarraSalud();

        if (saludActual <= 0)
        {
            Morir();
        }
        else
        {
            StartCoroutine(InvulnerabilidadTemporal());
        }
    }

    System.Collections.IEnumerator InvulnerabilidadTemporal()
    {
        esInvulnerable = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        esInvulnerable = false;
    }

    void ActualizarBarraSalud()
    {
        if (barraSalud != null)
        {
            barraSalud.value = (float)saludActual / saludMaxima;
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        gameObject.SetActive(false);
        muerte.SetActive(true);
    }
}

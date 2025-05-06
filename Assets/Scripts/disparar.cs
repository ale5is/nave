using UnityEngine;

public class disparar : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float velocidadDisparo = 20f;
    public float tiempoEntreDisparos = 0.3f;

    private float tiempoProximoDisparo;
    private bool disparando;

    public void ComenzarDisparo()  // Llamado por el botón móvil (OnPointerDown)
    {
        disparando = true;
    }

    public void DetenerDisparo()   // Llamado por el botón móvil (OnPointerUp)
    {
        disparando = false;
    }

    void Update()
    {
        if (disparando && Time.time >= tiempoProximoDisparo)
        {
            Disparar();
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        if (proyectilPrefab == null || puntoDisparo == null) return;

        // Direccion hacia el centro de lo que ve la cámara
        Vector3 direccion = Camera.main.transform.forward; // Dirección hacia donde está mirando la cámara
        direccion.y = 0; // Opcional: evitar que las balas disparen hacia arriba o abajo (solo en el plano horizontal)

        // Instancia del proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Rotar hacia la dirección en la que está mirando la cámara
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        Quaternion rotacionCorregida = rotacionObjetivo * Quaternion.Euler(90f, 0f, 0f); // Aplica corrección en X

        proyectil.transform.rotation = rotacionCorregida;

        // Aplicar velocidad
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccion * velocidadDisparo;
        }
    }
}

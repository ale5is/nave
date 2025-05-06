using UnityEngine;

public class balas : MonoBehaviour
{
    public float distanciaMaxima = 50f;
    public float tiempoMaximoVida = 10f;

    private Vector3 origen;

    void Start()
    {
        origen = transform.position;
        Destroy(gameObject, tiempoMaximoVida); // Seguridad por si no se aleja lo suficiente
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, origen);
        if (distancia > distanciaMaxima)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí podrías aplicar daño, reproducir efectos, etc.
            Destroy(gameObject);
        }
    }
}

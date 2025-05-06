using UnityEngine;

public class iaEnemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float distanciaDisparo = 10f;
    public float distanciaDeAcercarse = 15f;
    public float distanciaMinimaDeSeparacion = 5f;
    public float tiempoEntreDisparos = 1.5f;
    public float dispersionGrados = 5f; // ← Nueva variable editable desde el Inspector

    public GameObject proyectilPrefab;
    public Transform puntoDisparo;

    private Transform jugador;
    private float tiempoSiguienteDisparo;

    void Start()
    {
        GameObject jugadorGO = GameObject.FindWithTag("Player");
        if (jugadorGO != null)
        {
            jugador = jugadorGO.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        RotarHaciaJugador();

        if (distancia < distanciaMinimaDeSeparacion)
        {
            AlejarseDelJugador();
        }
        else if (distancia > distanciaDisparo && distancia <= distanciaDeAcercarse)
        {
            MoverseHaciaElJugador();
        }
        else if (distancia <= distanciaDisparo)
        {
            if (Time.time >= tiempoSiguienteDisparo)
            {
                Disparar();
                tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
            }
        }
    }

    void RotarHaciaJugador()
    {
        Vector3 direccion = jugador.position - transform.position;

        // Calcula la rotación completa hacia el jugador (incluye eje Y)
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);
    }


    void MoverseHaciaElJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    void AlejarseDelJugador()
    {
        Vector3 direccion = (transform.position - jugador.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    void Disparar()
    {
        if (proyectilPrefab == null || puntoDisparo == null) return;

        // Instancia la bala
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Dirección base hacia el jugador
        Vector3 direccion = (jugador.position - puntoDisparo.position).normalized;

        // Aplica dispersión aleatoria
        direccion = Quaternion.Euler(
            Random.Range(-dispersionGrados, dispersionGrados),
            Random.Range(-dispersionGrados, dispersionGrados),
            Random.Range(-dispersionGrados, dispersionGrados)
        ) * direccion;

        // Calcula la rotación final (con corrección del modelo en X)
        Quaternion lookRotation = Quaternion.LookRotation(direccion);
        Quaternion rotacionFinal = lookRotation * Quaternion.Euler(90f, 0f, 0f);
        proyectil.transform.rotation = rotacionFinal;

        // Aplica la velocidad
        proyectil.GetComponent<Rigidbody>().velocity = direccion * 10f;
    }
}

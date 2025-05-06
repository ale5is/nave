using UnityEngine;

public class iaEnemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float distanciaDisparo = 10f;
    public float distanciaDeAcercarse = 15f;
    public float distanciaMinimaDeSeparacion = 5f;
    public float tiempoEntreDisparos = 1.5f;
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
            // Solo dispara si ha pasado el tiempo entre disparos
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

        // Evita errores si la dirección es cero
        if (direccion.sqrMagnitude > 0.001f)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 5f);
        }
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

        // Calcula la dirección hacia el jugador
        Vector3 direccion = (jugador.position - puntoDisparo.position).normalized;

        // Calcula la rotación hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(direccion);

        // Aplica la rotación del prefab (90° en X) sobre la rotación hacia el jugador
        Quaternion rotacionFinal = lookRotation * Quaternion.Euler(90f, 0f, 0f);
        proyectil.transform.rotation = rotacionFinal;

        // Aplica la velocidad al proyectil
        proyectil.GetComponent<Rigidbody>().velocity = direccion * 10f;
    }







}

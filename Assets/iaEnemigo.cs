using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iaEnemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float distanciaDisparo = 10f; // Rango de disparo
    public float distanciaAlejarse = 5f; // Rango en el que el enemigo se aleja del jugador
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

        // Rotar al enemigo hacia el jugador
        RotarHaciaJugador();

        // Si está más cerca que la distancia de alejarse, el enemigo se aleja
        if (distancia < distanciaAlejarse)
        {
            AlejarseDelJugador();
        }
        // Si está fuera del rango de disparo, se mueve alrededor del jugador
        else if (distancia > distanciaDisparo)
        {
            MoverseAlrededorDelJugador();
        }
        // Si está dentro del rango de disparo, dispara
        else if (distancia <= distanciaDisparo && Time.time >= tiempoSiguienteDisparo)
        {
            Disparar();
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    // Método para rotar el enemigo hacia el jugador
    void RotarHaciaJugador()
    {
        Vector3 direccion = jugador.position - transform.position;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f); // Suavizado
    }

    // Método para alejarse del jugador cuando está demasiado cerca
    void AlejarseDelJugador()
    {
        Vector3 direccion = (transform.position - jugador.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime; // Se aleja en la dirección opuesta al jugador
    }

    // Método para mover el enemigo alrededor del jugador si está fuera del rango de disparo
    void MoverseAlrededorDelJugador()
    {
        // Mover al enemigo en un círculo alrededor del jugador
        Vector3 direccion = (transform.position - jugador.position).normalized;
        Vector3 nuevaPosicion = jugador.position + direccion * distanciaDisparo * Mathf.Sin(Time.time * velocidad) * 0.5f;
        transform.position = Vector3.MoveTowards(transform.position, nuevaPosicion, velocidad * Time.deltaTime);
    }

    // Método para disparar al jugador
    void Disparar()
    {
        if (proyectilPrefab == null || puntoDisparo == null) return;

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

        // Mantiene la rotación en X en 90 grados y ajusta Y y Z para mirar hacia el jugador
        Vector3 direccion = (jugador.position - proyectil.transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        proyectil.transform.rotation = Quaternion.Euler(90f, rotacion.eulerAngles.y, rotacion.eulerAngles.z);

        // Aplica la velocidad al proyectil
        proyectil.GetComponent<Rigidbody>().velocity = direccion * 10f;
    }
}

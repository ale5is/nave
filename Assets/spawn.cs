using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [Header("Prefab del Obstáculo")]
    public GameObject obstaculoPrefab;

    [Header("Generación")]
    public float intervaloGeneracion = 2f;
    public int maximoObstaculos = 10;

    [Header("Destrucción")]
    public float distanciaMaxima = 60f;

    [Header("Aleatoriedad en el Spawn")]
    public float rangoHorizontal = 5f;
    public float rangoVertical = 3f;
    public float offsetZ = 15f;

    private float tiempoSiguiente = 0f;
    private List<GameObject> obstaculosActivos = new List<GameObject>();

    void Update()
    {
        tiempoSiguiente += Time.deltaTime;

        if (tiempoSiguiente >= intervaloGeneracion && obstaculosActivos.Count < maximoObstaculos)
        {
            tiempoSiguiente = 0f;

            float offsetX = Random.Range(-rangoHorizontal, rangoHorizontal);
            float offsetY = Random.Range(-rangoVertical, rangoVertical);
            Vector3 posicionSpawn = transform.position + new Vector3(offsetX, offsetY, offsetZ);

            GameObject nuevo = Instantiate(obstaculoPrefab, posicionSpawn, Quaternion.identity);
            obstaculosActivos.Add(nuevo);
        }

        // Buscar al jugador solo para calcular distancias
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            for (int i = obstaculosActivos.Count - 1; i >= 0; i--)
            {
                GameObject obs = obstaculosActivos[i];
                if (obs == null)
                {
                    obstaculosActivos.RemoveAt(i);
                    continue;
                }

                float distancia = Vector3.Distance(obs.transform.position, jugador.transform.position);
                if (distancia > distanciaMaxima)
                {
                    Destroy(obs);
                    obstaculosActivos.RemoveAt(i);
                }
            }
        }
    }
}

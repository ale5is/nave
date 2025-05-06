using UnityEngine;

public class spawnEnemigos : MonoBehaviour
{
    [Header("Configuración de Spawneo")]
    public GameObject enemigoPrefab;
    public float radioSpawn = 20f;
    public float tiempoEntreSpawns = 3f;
    public int maxEnemigos = 5;

    private Transform jugador;
    private int enemigosActuales = 0;

    void Start()
    {
        GameObject jugadorGO = GameObject.FindWithTag("Player");
        if (jugadorGO != null)
            jugador = jugadorGO.transform;

        InvokeRepeating(nameof(SpawnEnemigo), 1f, tiempoEntreSpawns);
    }

    void SpawnEnemigo()
    {
        if (jugador == null || enemigosActuales >= maxEnemigos) return;

        // Posición aleatoria en un círculo alrededor del jugador
        Vector2 pos2D = Random.insideUnitCircle.normalized * radioSpawn;
        Vector3 posicionSpawn = jugador.position + new Vector3(pos2D.x, 0, pos2D.y);

        GameObject enemigo = Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);
        enemigosActuales++;

        // Asociar el spawner al enemigo
        //AutoDestruirEnemigo autodes = enemigo.AddComponent<AutoDestruirEnemigo>();
        //autodes.spawner = this;
    }

    public void NotificarMuerte()
    {
        enemigosActuales = Mathf.Max(0, enemigosActuales - 1);
    }
}

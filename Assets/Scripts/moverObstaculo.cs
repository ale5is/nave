using UnityEngine;

public class moverObstaculo : MonoBehaviour
{
    public float velocidad = 5f;
    public float desviacionMaxima = 3f;

    private Vector3 direccion;

    void Start()
    {
        GameObject jugadorObj = GameObject.FindWithTag("Player");

        if (jugadorObj != null)
        {
            Vector3 offset = new Vector3(
                Random.Range(-desviacionMaxima, desviacionMaxima),
                Random.Range(-desviacionMaxima, desviacionMaxima),
                0 // solo desviación lateral y vertical
            );

            Vector3 objetivo = jugadorObj.transform.position + offset;
            direccion = (objetivo - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con tag 'Player'. El obstáculo no se moverá.");
            direccion = Vector3.zero;
        }
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }
}

using UnityEngine;

public class balaNave : MonoBehaviour
{
    public float rangoMaximo = 30f;
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, posicionInicial);
        if (distancia >= rangoMaximo)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objetivo"))
        {
            // Sumar puntos si el GameManager existe
            if (gameManager.instancia != null)
            {
                gameManager.instancia.SumarPuntos(20); // Puedes cambiar la cantidad
            }

            Destroy(other.gameObject);  // Destruye el objetivo
            Destroy(gameObject);        // Destruye la bala
        }
        if (other.CompareTag("Meteorito"))
        {
            // Sumar puntos si el GameManager existe
            if (gameManager.instancia != null)
            {
                gameManager.instancia.SumarPuntos(10); // Puedes cambiar la cantidad
            }

            Destroy(other.gameObject);  // Destruye el objetivo
            Destroy(gameObject);        // Destruye la bala
        }
    }
}

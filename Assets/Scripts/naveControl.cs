using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naveControl : MonoBehaviour
{
    // Usamos [SerializeField] para hacer que estas variables sean editables en el Inspector,
    // incluso si no son públicas. O puedes mantenerlas públicas si prefieres.
    [SerializeField]
    private float velocidadMovimiento = 5f;
    [SerializeField]
    private float velocidadAvance = 10f;

    private bool avanzar = false;

    void Update()
    {
        Vector3 acelerometro = Input.acceleration;

        // Asegúrate de que el acelerómetro se maneje dentro de los límites de velocidad
        float moverX = Mathf.Clamp(acelerometro.x, -1f, 1f) * velocidadMovimiento;
        float moverY = Mathf.Clamp(acelerometro.y, -1f, 1f) * velocidadMovimiento;

        Vector3 movimiento = new Vector3(moverX, moverY, avanzar ? velocidadAvance : 0f);

        transform.Translate(movimiento * Time.deltaTime);
    }

    public void IniciarAvance()
    {
        avanzar = true;
    }

    public void DetenerAvance()
    {
        avanzar = false;
    }
}

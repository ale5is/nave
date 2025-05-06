using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naveControl : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float velocidadAvance = 10f;

    private bool avanzar = false;

    void Update()
    {
        Vector3 acelerometro = Input.acceleration;

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

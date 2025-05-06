using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform nave;         // La nave que la cámara va a seguir
    public Vector3 offset = new Vector3(0, 2, -10); // Distancia de la cámara a la nave

    void LateUpdate()
    {
        if (nave != null)
        {
            transform.position = nave.position + offset;
        }
    }
}

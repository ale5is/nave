using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiarEscena : MonoBehaviour
{
    [Header("Número de escena en Build Settings")]
    public int numeroEscena = 0;

    public void CargarEscenaPorNumero()
    {
        if (numeroEscena >= 0 && numeroEscena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(numeroEscena);
        }
        else
        {
            Debug.LogWarning("El número de escena no existe en Build Settings.");
        }
    }
}

using UnityEngine;
using TMPro; // Asegúrate de tener esta línea
using UnityEngine.SceneManagement; // Necesario para cargar y recargar escenas
using UnityEngine.UI;
//using UnityEditor.Build.Reporting; // Necesario para interactuar con los botones de la UI

public class gameManager : MonoBehaviour
{
    public static gameManager instancia;

    [Header("Puntuación")]
    public int puntos = 0;

    [Tooltip("Arrastra aquí el TextMeshProUGUI que mostrará los puntos.")]
    public TextMeshProUGUI textoPuntos;

    [Header("Botones")]
    public Button botonReiniciar;  // Botón para reiniciar la escena
    public Button botonCargarEscena;  // Botón para cargar otra escena

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);

        // Asegúrate de que los botones están asignados en el inspector
        if (botonReiniciar != null)
            botonReiniciar.onClick.AddListener(ResetearEscena);

        if (botonCargarEscena != null)
            botonCargarEscena.onClick.AddListener(() => CargarEscena()); // Cambia "NombreDeTuEscena" por el nombre de tu escena
    }

    void Start()
    {
        ActualizarUI(); // Actualiza la UI al inicio
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntos;
        else
            Debug.LogWarning("No se asignó el TextMeshProUGUI en el inspector.");
    }

    // Función para reiniciar la escena actual
    void ResetearEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena activa
    }

    // Función para cargar una escena especificada
    void CargarEscena()
    {
        SceneManager.LoadScene(0);// Carga la escena especificada
    }
}

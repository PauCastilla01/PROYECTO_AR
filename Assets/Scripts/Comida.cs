using UnityEngine;
using UnityEngine.UI;

public class ComidaControl : MonoBehaviour
{
    public GameObject[] panels; // Los 5 paneles del Canvas (Ingredientes, Paso 1, Paso 2, Paso 3, Paso 4)
    public GameObject[] modelosPanel1; // Modelos para el panel de ingredientes
    public GameObject[] modelosPanel2; // Modelos para el paso 1
    public GameObject[] modelosPanel3; // Modelos para el paso 2
    public GameObject[] modelosPanel4; // Modelos para el paso 3
    public GameObject[] modelosPanel5; // Modelos para el paso 4

    private int pasoActual = 0; // Inicialmente estamos en el panel de Ingredientes

    // Botones para todos los pasos
    public Button[] btnSiguiente; // Botones para el siguiente paso
    public Button[] btnAnterior; // Botones para el paso anterior
    public Button btnReiniciar; // Botón para reiniciar

    void Start()
    {
        // Configurar botones de siguiente y anterior
        for (int i = 0; i < btnSiguiente.Length; i++)
        {
            btnSiguiente[i].onClick.AddListener(() => SiguientePaso(i));
            btnAnterior[i].onClick.AddListener(() => PasoAnterior(i));
        }

        btnReiniciar.onClick.AddListener(Reiniciar);

        // Iniciar mostrando el panel de ingredientes
        MostrarPanel(0); // Ingredientes
    }

    // Mostrar el panel correspondiente y activar los modelos asociados
    void MostrarPanel(int index)
    {
        // Asegurarnos de que el índice es válido antes de intentar acceder a los paneles
        if (index < 0 || index >= panels.Length)
        {
            Debug.LogError("Índice fuera de rango: " + index);
            return;
        }

        // Asegurarnos de que el panel que queremos mostrar está dentro de los límites
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index); // Solo el panel correspondiente al índice será activado
        }

        // Activar modelos correspondientes a cada paso
        ActivarModelos(index);

        // Configurar visibilidad de botones según el panel actual
        btnSiguiente[pasoActual].gameObject.SetActive(pasoActual < panels.Length - 1); // El botón "Siguiente" se muestra si no estamos en el último panel
        btnAnterior[pasoActual].gameObject.SetActive(pasoActual > 0); // El botón "Anterior" se muestra si no estamos en el primer panel
        btnReiniciar.gameObject.SetActive(pasoActual == panels.Length - 1); // El botón "Reiniciar" solo se muestra en el último panel
    }

    // Activar modelos dependiendo del paso
    void ActivarModelos(int paso)
    {
        // Desactivar todos los modelos 3D
        DesactivarTodosLosModelos();

        // Activar los modelos específicos del paso actual
        switch (paso)
        {
            case 0:
                ActivarModelosDePanel(modelosPanel1);
                break;
            case 1:
                ActivarModelosDePanel(modelosPanel2);
                break;
            case 2:
                ActivarModelosDePanel(modelosPanel3);
                break;
            case 3:
                ActivarModelosDePanel(modelosPanel4);
                break;
            case 4:
                ActivarModelosDePanel(modelosPanel5);
                break;
        }
    }

    // Activar los modelos de un panel específico
    void ActivarModelosDePanel(GameObject[] modelos)
    {
        foreach (var modelo in modelos)
        {
            if (modelo != null)
                modelo.SetActive(true);
        }
    }

    // Desactivar todos los modelos 3D
    void DesactivarTodosLosModelos()
    {
        foreach (var modelo in modelosPanel1) modelo.SetActive(false);
        foreach (var modelo in modelosPanel2) modelo.SetActive(false);
        foreach (var modelo in modelosPanel3) modelo.SetActive(false);
        foreach (var modelo in modelosPanel4) modelo.SetActive(false);
        foreach (var modelo in modelosPanel5) modelo.SetActive(false);
    }

    // Función para pasar al siguiente paso
    void SiguientePaso(int index)
    {
        if (pasoActual < panels.Length - 1)
        {
            pasoActual++;
            MostrarPanel(pasoActual);
        }
    }

    // Función para volver al paso anterior
    void PasoAnterior(int index)
    {
        if (pasoActual > 0)
        {
            pasoActual--;
            MostrarPanel(pasoActual);
        }
    }

    // Función para reiniciar todo al primer paso
    void Reiniciar()
    {
        pasoActual = 0; // Volver al panel 0 (ingredientes)
        MostrarPanel(pasoActual);
    }

    // Función para ocultar todos los paneles y modelos si el Image Target es perdido
    void OcultarTodo()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        DesactivarTodosLosModelos();
    }
}

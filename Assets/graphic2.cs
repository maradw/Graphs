using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphic2 : MonoBehaviour
{
    private List<float> temperatureData = new List<float>();  // Almacena los datos de temperatura
    public int maxDataCount = 25;  // L�mite de puntos a mostrar
    public float graphWidth = 500f;  // Ancho total de la gr�fica
    public float graphHeight = 300f;  // Altura total de la gr�fica
    public float temperatureMin = 0f;  // M�nimo valor de temperatura
    public float temperatureMax = 40f;  // M�ximo valor de temperatura
    public RectTransform graphContainer;  // Contenedor de la UI

    void Start()
    {
        // Aqu� puedes simular los datos de temperatura directamente
        // Agregar algunos datos de ejemplo para probar
        AddTemperatureData(18f);
        AddTemperatureData(20f);
        AddTemperatureData(22f);
        AddTemperatureData(24f);
        AddTemperatureData(26f);
        AddTemperatureData(28f);
        AddTemperatureData(30f);
        AddTemperatureData(32f);
        AddTemperatureData(34f);
        AddTemperatureData(36f);
        AddTemperatureData(38f);
        AddTemperatureData(40f);
        AddTemperatureData(37f);
        AddTemperatureData(35f);
        AddTemperatureData(33f);
        AddTemperatureData(31f);
        AddTemperatureData(29f);
        AddTemperatureData(27f);
        AddTemperatureData(25f);
        AddTemperatureData(23f);
        AddTemperatureData(21f);
        AddTemperatureData(19f);
        AddTemperatureData(17f);
        AddTemperatureData(15f);
    }

    // Agrega un nuevo dato a la lista y mantiene el tama�o m�ximo
    void AddTemperatureData(float newTemperature)
    {
        temperatureData.Add(newTemperature);

        // Si hay m�s de 'maxDataCount' puntos, eliminamos el primero
        if (temperatureData.Count > maxDataCount)
        {
            temperatureData.RemoveAt(0);
        }

        UpdateGraph();
    }

    // Actualiza la visualizaci�n del gr�fico
    void UpdateGraph()
    {
        // Elimina los elementos visuales existentes
        foreach (Transform child in graphContainer)
        {
            Destroy(child.gameObject);
        }

        // Dibuja las l�neas entre los puntos
        float xSpacing = graphWidth / (float)(temperatureData.Count - 1);

        Vector2 previousPoint = Vector2.zero;  // Para almacenar el punto anterior y dibujar la l�nea

        for (int i = 0; i < temperatureData.Count; i++)
        {
            float x = i * xSpacing;  // Posici�n en X
            float y = Mathf.Lerp(0, graphHeight, (temperatureData[i] - temperatureMin) / (temperatureMax - temperatureMin));  // Normalizamos la temperatura en el rango de la gr�fica

            Vector2 currentPoint = new Vector2(x, y);

            // Crea una l�nea entre el punto anterior y el actual
            if (i > 0) // Para asegurarnos de que el primer punto no intente conectar a nada
            {
                DrawLine(previousPoint, currentPoint);
            }

            // Establece el punto anterior para la siguiente iteraci�n
            previousPoint = currentPoint;
        }
    }

    // Dibuja una l�nea entre dos puntos
    void DrawLine(Vector2 start, Vector2 end)
    {
        GameObject line = new GameObject("Line", typeof(Image));
        line.transform.SetParent(graphContainer, false);

        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Vector2.Distance(start, end), 2f);  // Ancho de la l�nea
        rectTransform.anchoredPosition = start + (end - start) / 2f;  // Centra la l�nea entre los dos puntos

        float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;  // �ngulo de la l�nea
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);

        Image image = line.GetComponent<Image>();
        image.color = Color.blue;  // Color de la l�nea
    }
}

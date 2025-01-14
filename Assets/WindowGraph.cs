using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private RectTransform _graphContainer;
    [SerializeField] private Sprite _circleSp;
    [SerializeField] private  LineRenderer _connectionLine;
    // Start is called before the first frame update
    private List<Vector3> _values;
    void Start()
    {
        

      //_connectionLine.transform.SetParent(_graphContainer, false);
    }
    private GameObject CreateCircle(Vector3 circlePosition)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(_graphContainer, true);
        circle.GetComponent<Image>().sprite = _circleSp;
        RectTransform recTransform = circle.GetComponent<RectTransform>();
        recTransform.anchoredPosition = circlePosition;
        recTransform.sizeDelta = new Vector3(8,8);
        recTransform.anchorMin = new Vector3(0, 0);
        recTransform.anchorMax = new Vector3(0, 0);
        return circle;
    }
    private void Awake()
    {
        _connectionLine.useWorldSpace = false;
        //_connectionLine.transform.InverseTransformDirection(_graphContainer.transform.position);
        _connectionLine.startWidth = 2;
        _connectionLine.endWidth = 2;
        _values = new List<Vector3>();
        List<float> values = new List<float>() { 5, 2, 6, 8, 10, 16, 17, 20, 25, 2 };
        ShowGraph(values);
    }
    void PosicitionsConnection()
    {
        _connectionLine.positionCount = _values.Count;
        _connectionLine.SetPositions(_values.ToArray());
    }
    void ShowGraph(List<float> temperature_values)
    {
        float xSize = 25f;
        float yMax = 100f;
        float GraphWidth = _graphContainer.sizeDelta.x;  // Ancho del contenedor
        float GraphHeight = _graphContainer.sizeDelta.y;  // Altura del contenedor
        Debug.Log("GraphWidth " + GraphWidth + " / GraphHeight "+ GraphHeight);
        _connectionLine.positionCount = temperature_values.Count;
        //GameObject lastCircleGameObject = null;
        for (int i = 0;  i < temperature_values.Count; i++)
        {
            
            float xPosition = (i / (float)(temperature_values.Count - 1)) * GraphWidth;
            // (temperature_values[i] / yMax) normaliza la temperatura de 0-1
            float yPosition = (temperature_values[i] / yMax) * GraphHeight;
            Debug.Log("GraphHeight " + GraphHeight);
            Vector3 _valuePoint = new Vector3(xPosition, yPosition, 0);

            // Convertir _valuePoint a espacio local del contenedor (_graphContainer)
            //Vector3 localPosition = _graphContainer.InverseTransformPoint(_valuePoint);

            // Crear el círculo en esa posición (en espacio local)
            GameObject circleGameObject = CreateCircle(_valuePoint);

            //_values.Add(localPosition);
 
            // Asigna la posición al LineRenderer en el índice correspondiente
            _connectionLine.SetPosition(i, _valuePoint);
        }
        //for(int i = 0; i<_values.Count; i++)
        //{
        //    Debug.Log(_values[i]);
        //}
        //PosicitionsConnection();
    }
    
     
   
}

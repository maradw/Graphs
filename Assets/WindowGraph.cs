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
        _connectionLine.useWorldSpace = false;
        //_connectionLine.transform.InverseTransformDirection(_graphContainer.transform.position);
        _connectionLine.startWidth = 2;
        _connectionLine.endWidth = 2;

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
        float GraphHeight = _graphContainer.sizeDelta.y;
        GameObject lastCircleGameObject = null;
        for(int i = 0;  i < temperature_values.Count; i++)
        {
            float xPosition =xSize+ i * xSize;
            float yPosition = (temperature_values[i] / yMax) * GraphHeight;
            Vector3 _valuePoint = new Vector3(xPosition, yPosition, 0);
            GameObject circleGameObject =  CreateCircle(_valuePoint);
            _values.Add(_valuePoint);
            if (lastCircleGameObject != null) 
            { 
               // CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject= circleGameObject;
        }
        for(int i = 0; i<_values.Count; i++)
        {
            Debug.Log(_values[i]);
        }
        PosicitionsConnection();
    }
    
    void Update()
    {
        
    }
   
}

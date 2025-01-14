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
       // _connectionLine = gameObject.AddComponent<LineRenderer>();
        _connectionLine.startWidth = 2;
        _connectionLine.endWidth = 2;
        
    }
    private GameObject CreateCircle(Vector2 circlePosition)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(_graphContainer, false);
        circle.GetComponent<Image>().sprite = _circleSp;
        RectTransform recTransform = circle.GetComponent<RectTransform>();
        recTransform.anchoredPosition = circlePosition;
        recTransform.sizeDelta = new Vector2(11, 11);
        recTransform.anchorMin = new Vector2(0, 0);
        recTransform.anchorMax = new Vector2(0, 0);
        return circle;
    }
    private void Awake()
    {
        _values = new List<Vector3>();
         /*{
             new Vector3(100,2,20),
           new Vector3(13,20,51),
           new Vector3(200,10,1),

         };*/



        //CreateCircle(new Vector2(100, 20));
        List<float> values = new List<float>() { 5, 2, 6, 8, 10, 16, 17, 20, 25, 2 };
        ShowGraph(values);
    }
    void PosicitionsConnection()
    {
        //_connectionLine.SetPositions()
        _connectionLine.positionCount = _values.Count;

        // Asigna las posiciones al LineRenderer
        _connectionLine.SetPositions(_values.ToArray());
    }
    void ShowGraph(List<float> temperature_values)
    {
        float xSize = 20f;
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
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject= circleGameObject;
        }
        for(int i = 0; i<_values.Count; i++)
        {
            Debug.Log(_values[i]);
        }
        
    }
    void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject connection = new GameObject("dot Connection", typeof(Image));
        connection.transform.SetParent(_graphContainer, false);
        connection.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = connection.GetComponent<RectTransform>();

        Vector2 direction = (dotPositionB - dotPositionA).normalized;
        //
        float disctance = Vector2.Distance(dotPositionA, dotPositionB);
        //
        rectTransform.sizeDelta = new Vector2(disctance, 2f);
        //
        rectTransform.anchoredPosition = dotPositionA;
        //
        float angleInRadians = Mathf.Atan2(direction.y, direction.x); // Obtener el ángulo en radianes
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        //

        rectTransform.rotation = Quaternion.Euler(0, 0, angleInDegrees);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        
       

       

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

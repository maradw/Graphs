using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private RectTransform _graphContainer;
    [SerializeField] private Sprite _circleSp;

    // Start is called before the first frame update
    void Start()
    {
       
        
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
        
        //CreateCircle(new Vector2(100, 20));
        List<float> values = new List<float>() { 5, 2, 6, 8, 10, 16, 17, 20, 25, 2 };
        ShowGraph(values);
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
            GameObject circleGameObject =  CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) 
            { 
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject= circleGameObject;
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
        float angleInRadians = Mathf.Atan2(direction.y, direction.x); // Obtener el ángulo en radianes
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        //
        float disctance = Vector2.Distance(dotPositionA, dotPositionB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(100, 3f);
        rectTransform.anchoredPosition = dotPositionA;

        rectTransform.localEulerAngles = new Vector3(0,0, angleInDegrees);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Window_Graph : MonoBehaviour {

    public Sprite circleSprite;

    //public List<GameObject> dotList;
    //RectTransform graphContainer;

    //private void Awake() {
    //    //graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

    //    //List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
    //    //ShowGraph(valueList);
    //}

    //void Start()
    //{
    //    graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    //}

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));

        RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }


    public void makeLine(float yMaximum, float lineMarkerHeight, Color currColor)
    {
        RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;


        GameObject gameObject = new GameObject("dotConnection", typeof(Image));

        
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = currColor;

        CreateDotConnection(gameObject, new Vector2(0, (lineMarkerHeight / yMaximum) * graphHeight), new Vector2(graphWidth, (lineMarkerHeight / yMaximum) * graphHeight));
    }

    public List<GameObject> setupPoints(float xMaximum, Color currColor)
    {
        List<GameObject> dotList = new List<GameObject>();

        for (int i = 0; i < xMaximum; i++)
        {
            //GameObject circleGameObject = CreateCircle(new Vector2(0, 0));

            GameObject gameObject = new GameObject("dotConnection", typeof(Image));

            RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = currColor;

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            gameObject.SetActive(false);
            dotList.Add(gameObject);
        }

        return dotList;
    }

    public void makeAxes(float xMaximum, float yMaximum, int deltaXAxis, int deltaYAxis)
    {
        RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        RectTransform labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        RectTransform labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        

        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;
        //float totalSpace = (graphWidth - xMaximum) / xMaximum + 1;

        

        for (int i = 0; i <= deltaXAxis; i++)
        {
            //float xPosition = i * totalSpace + 10f;
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            float normalizedValue = (i * 1f) / deltaXAxis;
            labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -5f);
            labelX.GetComponent<Text>().text = (normalizedValue * xMaximum).ToString();

            
        }

        
        for (int i = 0; i <= deltaYAxis; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = (i * 1f) / deltaYAxis;
            labelY.anchoredPosition = new Vector2(-5f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = (normalizedValue * yMaximum).ToString();
        }


        
    }

    public void ShowGraph(List<GameObject> dotList, List<float> valueList, float yMaximum, float xMaximum) {

        RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        RectTransform labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        RectTransform labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;
        float totalSpace = (graphWidth - xMaximum) / xMaximum + 1;


        //Debug.Log(valueList.Count);
        //GameObject lastCircleGameObject = null;

        Vector2 lastPos = new Vector2(-1, -1);
        for (int i = 0; i < valueList.Count; i++) {

            float xPosition = i * totalSpace;
            //float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            //GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            GameObject circleGameObject = dotList[i];
            circleGameObject.SetActive(true);
            //circleGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

            Vector2 currPos = new Vector2(xPosition, yPosition);


            if (lastPos.x != -1)
            {
                CreateDotConnection(circleGameObject, lastPos, currPos);
            }
            lastPos = currPos;

            //if (lastCircleGameObject != null) {
            //    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, currColor);
            //}
            //lastCircleGameObject = circleGameObject;

            //CreateDotConnection(new Vector2(0, (lineMarkerHeight/ yMaximum)* graphHeight), new Vector2(graphWidth, (lineMarkerHeight / yMaximum) * graphHeight), Color.blue);


            //if (i % 10 == 0)
            //{
            //    RectTransform labelX = Instantiate(labelTemplateX);
            //    labelX.SetParent(graphContainer);
            //    labelX.gameObject.SetActive(true);
            //    labelX.anchoredPosition = new Vector2(xPosition, -5f);
            //    labelX.GetComponent<Text>().text = (i).ToString();
            //}

        }

        //int separatorCount = 10;
        //for (int i = 0; i <= separatorCount; i++)
        //{
        //    RectTransform labelY = Instantiate(labelTemplateY);
        //    labelY.SetParent(graphContainer);
        //    labelY.gameObject.SetActive(true);
        //    float normalizedValue = (i * 1f) / separatorCount;
        //    labelY.anchoredPosition = new Vector2(-5f, normalizedValue * graphHeight);
        //    labelY.GetComponent<Text>().text = (normalizedValue * yMaximum).ToString();
        //}


    }

    private void CreateDotConnection(GameObject currObj, Vector2 dotPositionA, Vector2 dotPositionB) { // ,Color currColor
        //GameObject gameObject = new GameObject("dotConnection", typeof(Image));

        //RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().color = currColor;
        
        RectTransform rectTransform = currObj.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }





    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}

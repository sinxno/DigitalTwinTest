// based on: https://www.youtube.com/watch?v=CmU5-v-v1Qo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class WindowGraph : MonoBehaviour
{
    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;

    private List<GameObject> graphObjects = new List<GameObject>();

    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private TextMeshProUGUI nameLabel;
    private void Awake()
    {
        gameObject.SetActive(false);
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();
        nameLabel = transform.Find("nameLabel").GetComponent<TextMeshProUGUI>();

    }
    public void CreateGraph(List<Measurement> measurements, Instrument instrument)
    {
        gameObject.SetActive(true);
        nameLabel.text = instrument.instrumentName;
        List<float> valueList = new List<float>();
        List<uint> timeStampList = new List<uint>();
        List<bool> alarmLow = new List<bool>();
        List<bool> alarmHigh = new List<bool>();
        foreach (Measurement measurement in measurements)
        {
            valueList.Add(measurement.valueScaled);
            timeStampList.Add(measurement.timestamp);
            alarmHigh.Add(measurement.ah_active);
            alarmLow.Add(measurement.al_active);
        }

        ShowGraph(valueList, timeStampList, instrument, alarmLow, alarmHigh);

    }

    public void HideGraph()
    {
        for (int i = 0; i < graphObjects.Count; i++)
        {
            Destroy(graphObjects[i]);
        }
        graphObjects.Clear();    
        gameObject.SetActive(false);
    }


    private GameObject CreateCircle(Vector2 anchroedPosition, bool alarm)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;

        if(alarm)
            gameObject.GetComponent<Image>().color = Color.red;
        
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchroedPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        graphObjects.Add(gameObject);
        return gameObject;
    }

    private void ShowGraph(List<float> valueList, List<uint> timeStampList, Instrument instrument, List<bool> alarmLow, List<bool> alarmHigh)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = instrument.URV;
        float xSize = graphContainer.GetComponent<RectTransform>().sizeDelta.x/(valueList.Count+1);

        GameObject prevCircleGameObject = null;
        
        int modulo = FindBestModulo(timeStampList.Count, 5, 2); //Dette fungerer ikke som planlagt, men fungerer
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = CreateDot(valueList, graphHeight, yMaximum, xSize, ref prevCircleGameObject, i, alarmHigh, alarmLow);
            if (i % modulo == 0)
            {
                CreateXLabel(timeStampList, i, xPosition);
                
                CreateXDash(xPosition);
            }
            
        }

        CreateYLabel(10, graphHeight, yMaximum);
    }

    private void CreateXDash(float xPosition)
    {
        RectTransform dashX = Instantiate(dashTemplateX);
        graphObjects.Add(dashX.gameObject);
        dashX.SetParent(graphContainer,false);
        dashX.gameObject.SetActive(true);
        dashX.anchoredPosition = new Vector2(xPosition, -2);
    }
    private void CreateYDash(float normalizedValue, float graphHeight)
    {
        RectTransform dashY = Instantiate(dashTemplateY);
        graphObjects.Add(dashY.gameObject);
        dashY.SetParent(graphContainer, false);
        dashY.gameObject.SetActive(true);
        dashY.anchoredPosition = new Vector2(-2, normalizedValue*graphHeight);
    }

    private void CreateXLabel(List<uint> timeStampList, int i, float xPosition)
    {
        RectTransform labelX = Instantiate(labelTemplateX);
        graphObjects.Add(labelX.gameObject);
        labelX.SetParent(graphContainer);
        labelX.gameObject.SetActive(true);
        labelX.anchoredPosition = new Vector2(xPosition, 0);
        DateTime dt = CommonUtilities.GetTimestampAsDatetime(timeStampList[i]);
        labelX.GetComponent<TextMeshProUGUI>().text = string.Format("{0}\n{1}", dt.ToLongTimeString(), dt.ToShortDateString());
    }

    private void CreateYLabel(int seperatorCount, float graphHeight, float yMaximum)
    {
        for (int i = 0; i <= seperatorCount; i++) 
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            graphObjects.Add(labelY.gameObject);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = (float)i/ seperatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<TextMeshProUGUI>().text = (normalizedValue *yMaximum).ToString();
            if(i!=0 && i!=seperatorCount)
                CreateYDash(normalizedValue, graphHeight);
        }

        
    }

    private float CreateDot(List<float> valueList, float graphHeight, float yMaximum, float xSize, ref GameObject prevCircleGameObject, int i, List<bool> alarmHigh, List<bool> alarmLow)
    {
        float xPosition = xSize + i * xSize;
        float yPosition = (valueList[i] / yMaximum) * graphHeight;
        bool alarm = false;
        if(alarmHigh[i] || alarmLow[i]) alarm = true;
        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), alarm);
        if (prevCircleGameObject != null)
        {
            CreateDotConnection(prevCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
        }
        prevCircleGameObject = circleGameObject;
        return xPosition;
    }

    private int FindBestModulo(int count, int maxWantedPoints, int minSpacing)
    {
        //Dette fungerer ikke som planlagt, men fungerer
        int modulo = maxWantedPoints +1;
        int result = 0;
        while(result < minSpacing)
        {
            modulo--;
            result = count/modulo;
            Debug.Log(result);
        }
        Debug.Log(modulo);
        return modulo;
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1,.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax= new Vector2(0, 0);
        rectTransform.sizeDelta= new Vector2(distance, 3);
        rectTransform.anchoredPosition = dotPositionA + dir *distance *.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, CommonUtilities.GetAngleFromVectorFloat(dir));
        graphObjects.Add(gameObject);
    }


}

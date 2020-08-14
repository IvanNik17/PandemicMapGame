using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekNumberVisualization : MonoBehaviour
{

    public GameObject weekDay;
    public int numWeeks = 14;
    GameObject[,] allWeekDayObjs;

    

    float[] sick = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 6, 7, 8, 10, 11, 13, 15, 17, 20, 22, 25, 27, 30, 32, 34, 36, 37, 37, 37, 37, 36, 35, 33, 31, 29, 26, 24, 22, 19, 17, 15, 13, 12, 10, 9, 8, 7, 6, 5, 4, 4, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] dead = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
    float[] hospitalized = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9, 8, 7, 6, 5, 5, 4, 4, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] mental_health = { 200, 199, 198, 197, 196, 195, 194, 193, 192, 191, 190, 189, 188, 187, 186, 185, 184, 183, 182, 181, 180, 179, 178, 177, 176, 175, 174, 173, 172, 171, 170, 169, 168, 167, 166, 165, 164, 163, 162, 161, 160, 159, 158, 157, 156, 155, 154, 153, 152, 151, 150, 149, 148, 147, 146, 145, 144, 143, 142, 141, 140, 139, 138, 137, 136, 135, 134, 133, 132, 131, 130, 129, 128, 127, 126, 125, 124, 123, 122, 121, 120, 119, 118, 117, 116, 115, 114, 113, 112, 111, 110, 109, 108, 107, 106, 105, 104, 103 };
    float[] budget = { 100f, 99.5f, 99f, 98.5f, 98f, 97.5f, 97f, 96.5f, 96f, 95.5f, 95f, 94.5f, 94f, 93.5f, 93f, 92.5f, 92f, 91.5f, 91f, 90.5f, 90f, 89.5f, 89f, 88.5f, 88f, 87.5f, 87f, 86.5f, 86f, 85.5f, 85f, 84.5f, 84f, 83.5f, 83f, 82.5f, 82f, 81.5f, 81f, 80.5f, 80f, 79.5f, 79f, 78.5f, 78f, 77.5f, 77f, 76.5f, 76f, 75.5f, 75f, 74.5f, 74f, 73.5f, 73f, 72.5f, 72f, 71.5f, 71f, 70.5f, 70f, 69.5f, 69f, 68.5f, 68f, 67.5f, 67f, 66.5f, 66f, 65.5f, 65f, 64.5f, 64f, 63.5f, 63f, 62.5f, 62f, 61.5f, 61f, 60.5f, 60f, 59.5f, 59f, 58.5f, 58f, 57.5f, 57f, 56.5f, 56f, 55.5f, 55f, 54.5f, 54f, 53.5f, 53f, 52.5f, 52f, 51.5f };
    // days school is closed per class, days without going out, days without sports, handwashing every day

    bool activateCurr = false;

    float[,] newVals;
    float minVal;
    float maxVal;

    public Gradient gradient;

    bool isScaling = false;
    
    // Start is called before the first frame update
    void Start()
    {

        

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        




        newVals = new float[numWeeks, 7];

        Vector3 dayPos = Vector3.zero;

        allWeekDayObjs = new GameObject[numWeeks, 7];
        for (int i = 0; i < numWeeks; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject currDayObj = Instantiate(weekDay);
                dayPos.x = j;
                dayPos.z = i;
                currDayObj.transform.position = dayPos;
                currDayObj.transform.localScale = Vector3.one / 2f;

                allWeekDayObjs[i, j] = currDayObj;
            }
        }
    }



    float[,] createRandomVals(int numWeeks, string type, out float minVal, out float maxVal)
    {
        float[,] output = new float[numWeeks,7];

        minVal = -1;
        maxVal = -1;
        int counter = 0;
        for (int i = 0; i < numWeeks; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (type == "sick")
                {
                    output[i, j] = sick[counter];

                    minVal = Mathf.Min(sick);
                    maxVal = Mathf.Max(sick);
                }
                else if (type == "dead")
                {
                    output[i, j] = dead[counter];

                    minVal = Mathf.Min(dead);
                    maxVal = Mathf.Max(dead);
                }
                else if (type == "hospitalized")
                {
                    output[i, j] = hospitalized[counter];

                    minVal = Mathf.Min(hospitalized);
                    maxVal = Mathf.Max(hospitalized);
                }
                else if (type == "mental_health")
                {
                    output[i, j] = mental_health[counter];

                    minVal = Mathf.Min(mental_health);
                    maxVal = Mathf.Max(mental_health);
                }
                else if (type == "budget")
                {
                    output[i, j] = budget[counter];

                    minVal = Mathf.Min(budget);
                    maxVal = Mathf.Max(budget);
                }
                else if (type == "class 1-3" || type == "class 4-6" || type == "class 7-9" || type == "class 10-12" || type=="goingOut" || type == "sports")
                {
                    output[i, j] = Random.Range(0, 2);

                    minVal = 0;
                    maxVal = 1;
                }
                else if (type == "handwashing")
                {
                    output[i, j] = Random.Range(0, 15);

                    minVal = 0;
                    maxVal = 15;
                }

                counter++;
            }
        }

        return output;


    }

    float remapVal(float value, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, value);
        return Mathf.Lerp(bLow, bHigh, normal);
    }


    public virtual IEnumerator scaleTo(float delayTime, GameObject currObj, Vector3 oldScale, Vector3 newScale, float speedScaling)
    {
        isScaling = true;
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= speedScaling)
        { // until one second passed

            currObj.transform.localScale = Vector3.Lerp(oldScale, newScale, (Time.time - startTime) / speedScaling); // lerp from A to B in one second


            yield return 1; // wait for next frame
        }

        currObj.transform.localScale = newScale;

        yield return new WaitForSeconds(delayTime); // start at time X
        isScaling = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "sick", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "dead", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "hospitalized", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "mental_health", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.T) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "budget", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.T) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "class 1-3", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "class 4-6", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.U) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "class 7-9", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "class 10-12", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.O) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "goingOut", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "sports", out minVal, out maxVal);

            activateCurr = true;
        }
        else if (Input.GetKeyDown(KeyCode.L) && !isScaling)
        {
            newVals = createRandomVals(numWeeks, "handwashing", out minVal, out maxVal);

            activateCurr = true;
        }
       // Debug.Log("HIII");

        if (activateCurr)
        {
            for (int i = 0; i < numWeeks; i++)
            {
                for (int j = 0; j < 7; j++)
                {

                    Debug.Log(newVals[i, j]);
                    GameObject currObj = allWeekDayObjs[i, j];
                    Vector3 oldScale = currObj.transform.localScale;
                    Vector3 currScale = currObj.transform.localScale;

                    currScale.y = remapVal(newVals[i, j], minVal, maxVal, 0, 2);

                    StartCoroutine(scaleTo(0, currObj, oldScale, currScale, 0.5f));
                    //currObj.transform.localScale = currScale;

                    Color currColor = gradient.Evaluate(remapVal(currScale.y, 0, 2, 0, 1));

                    currObj.transform.GetComponentInChildren<MeshRenderer>().material.color = currColor;
                }
            }
            activateCurr = false;
        }


        
    }
}

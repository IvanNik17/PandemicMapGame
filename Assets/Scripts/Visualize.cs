using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Visualize : MonoBehaviour
{

    public GameObject barColumn;
    public GameObject barStart;

    float maxBarHeight = 30f;

    int prevDay;

    GameObject closeSchoolsButt;

    public bool isSchoolOpen = true;

    public GameObject daysTextUI;
    public GameObject hoursTextUI;

    float maxDays;

    // Start is called before the first frame update
    void Start()
    {
        //maxBarHeight = (int)(transform.GetComponent<SEIR_implementation>().population * 0.2f);
        //closeSchoolsButt = GameObject.Find("CloseSchool");
        //closeSchoolsButt.GetComponentInChildren<Text>().color = Color.red;
        prevDay = GlobalTimer.current.daysPassed;

        maxDays = transform.GetComponent<SEIR_implementation>().days_max;

        hoursTextUI.GetComponent<Text>().text = "Time: 1" +":00";
        daysTextUI.GetComponent<Text>().text = "Day: 0 / " + maxDays.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        int currDay = GlobalTimer.current.daysPassed;

        //if (currDay != prevDay)
        //{
        //    Vector3 positionBarColumn = barStart.transform.position;
        //    positionBarColumn.z -= currDay * (barColumn.transform.localScale.z);
        //    GameObject currColumn = Instantiate(barColumn, positionBarColumn, Quaternion.identity);
        //    Vector3 currColumn_scale = currColumn.transform.localScale;
        //    currColumn_scale.y = (SEIR_implementation.currInfected/ maxBarHeight)*100f;
        //    //Debug.Log(currColumn_scale.y);
        //    currColumn.transform.localScale = currColumn_scale;
        //}


        hoursTextUI.GetComponent<Text>().text = "Time: " + GlobalTimer.current.hoursPassed.ToString() + ":00";
        daysTextUI.GetComponent<Text>().text = "Day: " + GlobalTimer.current.daysPassed.ToString() + " / " + maxDays.ToString();

        prevDay = currDay;
    }

    public void closeSchoolNow()
    {
        isSchoolOpen = !isSchoolOpen;

        if (isSchoolOpen)
        {
            closeSchoolsButt.GetComponentInChildren<Text>().text = "Close School";
            closeSchoolsButt.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            closeSchoolsButt.GetComponentInChildren<Text>().text = "Open School";
            closeSchoolsButt.GetComponentInChildren<Text>().color = Color.blue;
        }
        

    }

}

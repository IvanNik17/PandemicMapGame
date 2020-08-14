using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalTimer current;

    //FixedDeltaTime == 0.02
    public float hourLength = 10f;
    public float dayLength;

    public float timer = 0;

    public int startTime = 1;

    public int daysPassed = 1;
    public int hoursPassed = 1;

    public int schoolStart = 7;
    public int schoolEnd = 15;

    //public GameObject hoursText;
    //public GameObject daysText;


    public int skipTo = 0;
    int saveDay = 0;
    float oldHourLength = 0;

    bool isSkipped = false;

    //int shoppingTime = 0;
    void Awake()
    {
        current = this;
    }


    void Start()
    {

        GlobalEvents.current.onSkipTimer += changeTimer;

        dayLength = hourLength * 24f;

        hoursPassed = startTime;

        timer = (float)startTime;

        //hoursText.GetComponent<TextMesh>().text = "Time: " + startTime.ToString();
        //daysText.GetComponent<TextMesh>().text = "Days: 0";


        //shoppingTime = Random.Range(9, 22);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (CheckEndConditions.current.isEnd == -1)
        {
            timer += Time.fixedDeltaTime;


            //Debug.Log(skipTo >= (daysPassed - saveDay));
            if (isSkipped && skipTo <= (daysPassed - saveDay))
            {
                hourLength = oldHourLength;

                dayLength = hourLength * 24f;
                timer = hourLength * (hoursPassed - 1);
                isSkipped = false;
            }
            


            //Debug.Log(timer / (hourLength * hoursPassed));

            if (timer >= (hourLength * hoursPassed))
            {

                // Debug.Log("hoursPassed = " + hoursPassed);
                //hoursText.GetComponent<TextMesh>().text = "Time: " + hoursPassed.ToString();

                GlobalEvents.current.hourPassed();
                hoursPassed++;

                if (!isSkipped && hoursPassed == schoolStart)
                {
                    GlobalEvents.current.goToPlace(2);
                }

                if (!isSkipped && hoursPassed == schoolEnd)
                {
                    GlobalEvents.current.backFromPlace(2);
                }

            }

            if (timer >= (dayLength))
            {

                
                

                // Debug.Log("daysPassed = " + daysPassed);
                //daysText.GetComponent<TextMesh>().text = "Days: " + daysPassed.ToString();

                GlobalEvents.current.dayPassed();

                daysPassed++;
                hoursPassed = 0;

                //hoursText.GetComponent<TextMesh>().text = "Time: " + hoursPassed.ToString();
                timer = 0;

                //shoppingTime = Random.Range(9, 22);
            }
        }


    }

    void changeTimer(int skipHowMuch)
    {
        skipTo = skipHowMuch;

        oldHourLength = hourLength;
        saveDay = daysPassed;

        hourLength = 0.001f;
        dayLength = hourLength * 24f;
        timer = hourLength * (hoursPassed - 1);

        isSkipped = true;

        GlobalEvents.current.backFromPlace(2);

    }
}
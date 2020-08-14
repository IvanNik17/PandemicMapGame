using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSchoolToggle : MonoBehaviour
{

    bool isItOpen;

    public Gradient gradient;

    public Image fill;

    public float addValue = 0.1f;
    public float addValueAdditional = 0;
    public float addValueMental = 0;

    public List<int> whichClasses;

    float prevNormValue = 0f;
    bool valTriggeredFirst = false;
    bool valTriggeredSecond = false;
    // Start is called before the first frame update
    void Start()
    {
        isItOpen = transform.GetComponentInChildren<Toggle>().isOn;

        GlobalEvents.current.onDayPassed += setHappiness;

        fill.color = gradient.Evaluate(1f);

        prevNormValue = transform.GetComponentInChildren<Slider>().normalizedValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeClassToggle(bool currToggle)
    {
        GlobalEvents.current.changeSchoolClass(whichClasses, currToggle);
        if (currToggle)
        {

            

            SEIR_implementation.current.currRho += addValue;
            if (SEIR_implementation.current.currRho > 1)
            {
                SEIR_implementation.current.currRho = 1;
            }
            SEIR_implementation.current.changeEconomyValue -= 1;

            
        }
        else
        {
            SEIR_implementation.current.currRho -= addValue;
            SEIR_implementation.current.changeEconomyValue += 1;
        }

        isItOpen = currToggle;

        valTriggeredFirst = false;
        valTriggeredSecond = false;
    }




    void setHappiness()
    {
        if (isItOpen && transform.GetComponentInChildren<Slider>().value < transform.GetComponentInChildren<Slider>().maxValue)
        {
            transform.GetComponentInChildren<Slider>().value += 1;

            
        }
        else if (!isItOpen && transform.GetComponentInChildren<Slider>().value > transform.GetComponentInChildren<Slider>().minValue)
        {
            transform.GetComponentInChildren<Slider>().value -= 1;
        }

        fill.color = gradient.Evaluate(transform.GetComponentInChildren<Slider>().normalizedValue);

        
        

        addValueAdditional = 0;

        addValueMental = 0;
        if (valTriggeredFirst == false && (transform.GetComponentInChildren<Slider>().normalizedValue == 0.5f || prevNormValue == 0.5f) )
        {
            if (!isItOpen)
            {
                addValueAdditional = 0.02f;

                addValueMental = -1f;
            }
            else
            {
                addValueAdditional = -0.02f;

                addValueMental = 1f;
            }

            valTriggeredFirst = true;
        }
        else if (valTriggeredSecond == false && (transform.GetComponentInChildren<Slider>().normalizedValue == 0.2f || prevNormValue == 0.2f))
        {
            if (!isItOpen)
            {
                addValueAdditional = 0.03f;

                addValueMental = -1f;
            }
            else
            {
                addValueAdditional = -0.03f;

                addValueMental = 1f;
            }

            valTriggeredSecond = true;
        }

        

        //if (SEIR_implementation.current.currRho< 1)
        //{
        //    SEIR_implementation.current.currRho += addValueAdditional;
        //}

        SEIR_implementation.current.currRho += addValueAdditional;

        SEIR_implementation.current.changeMentalHealthValue += addValueMental;


        prevNormValue = transform.GetComponentInChildren<Slider>().normalizedValue;
    }
}

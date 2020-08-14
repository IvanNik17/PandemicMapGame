using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infectedBar : MonoBehaviour
{

    public Gradient gradient;

    public Image fill;

    public Text showNumbers;

    public GameObject deadCounter;


    public void setMaxPossibleInfected(int hospitalBeds)
    {
        this.GetComponent<Slider>().maxValue = hospitalBeds;

        fill.color = gradient.Evaluate(1f);

        showNumbers.text = "0 / " + hospitalBeds.ToString();
    }


    public void setInfected(int infected)
    {
        this.GetComponent<Slider>().value = infected;

        fill.color = gradient.Evaluate(this.GetComponent<Slider>().normalizedValue);

        showNumbers.text = infected.ToString() + " / " + this.GetComponent<Slider>().maxValue.ToString();

        deadCounter.GetComponent<Text>().text = "Deaths: " + DebriefingGather.current.maxDead;

    }




}

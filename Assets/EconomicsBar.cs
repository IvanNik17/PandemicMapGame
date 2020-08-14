using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomicsBar : MonoBehaviour
{

    public Gradient gradient;

    public Image fill;

    public Text showNumbers;


    public void setMaxPossibleEconomics(int moneyMax)
    {
        this.GetComponent<Slider>().maxValue = moneyMax;

        fill.color = gradient.Evaluate(1f);

        showNumbers.text = moneyMax.ToString() + " / " + moneyMax.ToString();
    }


    public void setEconomics(int money)
    {
        this.GetComponent<Slider>().value = money;

        fill.color = gradient.Evaluate(this.GetComponent<Slider>().normalizedValue);

        showNumbers.text = money.ToString() + " / " + this.GetComponent<Slider>().maxValue.ToString();

    }
}

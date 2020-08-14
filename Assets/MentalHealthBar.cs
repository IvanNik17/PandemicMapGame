using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentalHealthBar : MonoBehaviour
{

    public Gradient gradient;

    public Image fill;

    public Text showNumbers;


    public void setMaxMentalHealth(float healthMax)
    {
        this.GetComponent<Slider>().maxValue = healthMax;

        fill.color = gradient.Evaluate(1f);

        showNumbers.text = healthMax.ToString() + " / " + healthMax.ToString();
    }


    public void setMentalHealth(float health)
    {
        this.GetComponent<Slider>().value = health;

        fill.color = gradient.Evaluate(this.GetComponent<Slider>().normalizedValue);

        showNumbers.text = health.ToString() + " / " + this.GetComponent<Slider>().maxValue.ToString();

    }
}

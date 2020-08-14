using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebriefingGather : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxExposed = 0f;
    public float maxInfected = 0f;
    public float maxDead = 0f;

    public float usedUpBudgetPercent = 0f;

    //public float numDead = 0f;

    public static DebriefingGather current;

    public int pandemicDayEnd = -1;

    bool isEndFound = false;


    public int[] days_schoolClosed;
    public int days_noGoingOut = 0;
    public int days_noSports = 0;
    public List<float> handSanitizationEachDay;

    public List<float> mentalHealthEachDay;

    public float sickPercent;
    public float hospitalizedPercent;
    public float deadPercent;


    public GameObject classesHolder;
    public GameObject goingOutToggle;
    public GameObject sportsToggle;
    public GameObject handSanitizationSlider;

    Toggle[] classToggles;

    private void Awake()
    {
        current = this;
    }


    void Start()
    {
        GlobalEvents.current.onDayPassed += GatherInfo;

        classToggles = classesHolder.GetComponentsInChildren<Toggle>();
        days_schoolClosed = new int[classToggles.Length];
        handSanitizationEachDay = new List<float>();
        mentalHealthEachDay = new List<float>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GatherInfo()
    {
        if (maxExposed < SEIR_implementation.current.currExposed)
        {
            maxExposed = SEIR_implementation.current.currExposed;
        }

        if (maxInfected < SEIR_implementation.current.currInfected)
        {
            maxInfected = SEIR_implementation.current.currInfected;

            //if (SEIR_implementation.current.currInfected > SEIR_implementation.current.maxHospitalBeds)
            //{
            //    numDead = SEIR_implementation.current.currInfected - SEIR_implementation.current.maxHospitalBeds;
            //}
            
        }
        if (maxDead < SEIR_implementation.current.currDead)
        {
            maxDead = SEIR_implementation.current.currDead;
        }

        if (!isEndFound && GlobalTimer.current.daysPassed > 15 && (SEIR_implementation.current.currInfected + SEIR_implementation.current.currExposed) < 0.01f * SEIR_implementation.current.population && (SEIR_implementation.current.currRecovered + SEIR_implementation.current.currSusceptible) >= 0.95f * SEIR_implementation.current.population)
        {
            pandemicDayEnd = GlobalTimer.current.daysPassed;
            isEndFound = true;
        }

        usedUpBudgetPercent = SEIR_implementation.current.maxEconomy / SEIR_implementation.current.startEconomy;

        for (int i = 0; i < classToggles.Length; i++)
        {
            if (!classToggles[i].isOn)
            {
                days_schoolClosed[i] += 1;
            }
        }

        if (!goingOutToggle.GetComponent<Toggle>().isOn)
        {
            days_noGoingOut++;
        }

        if (!sportsToggle.GetComponent<Toggle>().isOn)
        {
            days_noSports++;
        }

        handSanitizationEachDay.Add(handSanitizationSlider.GetComponent<Slider>().value);

        mentalHealthEachDay.Add(SEIR_implementation.current.currMentalHealth);

        sickPercent = ((maxExposed + maxInfected) / SEIR_implementation.current.population) * 100;
        hospitalizedPercent = (maxInfected / SEIR_implementation.current.population) * 100;
        deadPercent = (maxDead / SEIR_implementation.current.population) * 100;


    }


}

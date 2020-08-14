using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndConditions : MonoBehaviour
{

    public static CheckEndConditions current;


    public int isEnd = -1;

    void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.onDayPassed += checkEnd;
    }


    void checkEnd()
    {
        //if (GlobalTimer.current.daysPassed > 10 && (SEIR_implementation.current.currRecovered + SEIR_implementation.current.currSusceptible) >= 0.90f * SEIR_implementation.current.population)

        if (GlobalTimer.current.daysPassed > 15 && (SEIR_implementation.current.currInfected + SEIR_implementation.current.currExposed) < 0.01f * SEIR_implementation.current.population && (SEIR_implementation.current.currRecovered + SEIR_implementation.current.currSusceptible) >= 0.95f * SEIR_implementation.current.population)
        {
            isEnd = 0;

        }
        //else if (SEIR_implementation.current.currInfected > SEIR_implementation.current.maxHospitalBeds)
        //{
        //    isEnd = 0;


        //}
        else if (SEIR_implementation.current.startEconomy <= 0)
        {
            isEnd = 1;


        }
        //else if (GlobalTimer.current.daysPassed> SEIR_implementation.current.days_max)
        //{
        //    isEnd = 0;


        //}

        else if (SEIR_implementation.current.currMentalHealth <= 0)
        {
            isEnd = 3;
        }


        else if (GlobalTimer.current.daysPassed >= SEIR_implementation.current.days_max)
        {
            isEnd = 2;


        }

        GlobalEvents.current.setGameEnd(isEnd);
    }





}

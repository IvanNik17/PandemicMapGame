using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class GlobalEvents : MonoBehaviour
{

    public static GlobalEvents current;

    void Awake()
    {
        current = this;
    }



    public event Action onDayPassed;
    public void dayPassed()
    {
        if (onDayPassed != null)
        {
            onDayPassed();
        }
    }

    public event Action onHourPassed;
    public void hourPassed()
    {
        if (onHourPassed != null)
        {
            onHourPassed();
        }
    }


    public event Action<int> onGoToPlace;
    public void goToPlace(int place)
    {
        if (onGoToPlace != null)
        {
            onGoToPlace(place);
        }
    }

    public event Action<int> onBackFromPlace;
    public void backFromPlace(int place)
    {
        if (onBackFromPlace != null)
        {
            onBackFromPlace(place);
        }
    }

    //public event Action<int> onChangeSchoolClass;
    //public void changeSchoolClass(int newValue)
    //{
    //    if (onChangeSchoolClass != null)
    //    {
    //        onChangeSchoolClass(newValue);
    //    }
    //}

    public event Action<List<int>, bool> onChangeSchoolClass;
    public void changeSchoolClass(List<int> newValue, bool isOpen)
    {
        if (onChangeSchoolClass != null)
        {
            onChangeSchoolClass(newValue, isOpen);
        }
    }


    public event Action<int> onSetGameEnd;
    public void setGameEnd(int whichEnd)
    {
        if (onSetGameEnd != null)
        {
            onSetGameEnd(whichEnd);
        }
    }

    public event Action<int> onSkipTimer;
    public void skipTimer(int howMuch)
    {
        if (onSkipTimer != null)
        {
            onSkipTimer(howMuch);
        }
    }

}

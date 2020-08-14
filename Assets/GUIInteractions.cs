using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteractions : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject sliderSchoolOpen;

    GameObject testSchoolOpen;

    GameObject infectedBar;

    GameObject endGame;

    GameObject sliderHandSanitize;
    GameObject textHandSanitize;

    //GameObject endGraph;

    GameObject sliderTimeSpeed;

    GameObject currGraph;

    List<GameObject> pointsList_S;
    List<GameObject> pointsList_E;
    List<GameObject> pointsList_I;
    List<GameObject> pointsList_R;

    public float goingOutWithFriends_rho = 0.1f;
    public float outdoorSports_rho = 0.1f;
    

    public float goingOutWithFriends_mental = 1f;
    public float outdoorSports_mental = 1f;

    public int goingOutWithFriends_economy = 1;
    public int outdoorSports_economy = 1;


    float oldSanitizationVal = 0f;

    void Start()
    {

        GlobalEvents.current.onSetGameEnd += showEndScreen;

        GlobalEvents.current.onDayPassed += updateGraph;

        sliderSchoolOpen = GameObject.Find("LetClassesSlider");
        testSchoolOpen = GameObject.Find("sliderText");

        sliderHandSanitize = GameObject.Find("handSanitizationSlider");
        textHandSanitize = GameObject.Find("handSanitizationText");
        textHandSanitize.GetComponent<Text>().text = "Hand sanitizations \n per day: " + sliderHandSanitize.GetComponent<Slider>().value;
        oldSanitizationVal = sliderHandSanitize.GetComponent<Slider>().value;

        sliderTimeSpeed = GameObject.Find("TimeSpeedSlider");
        sliderTimeSpeed.GetComponent<Slider>().value = sliderTimeSpeed.GetComponent<Slider>().maxValue - GlobalTimer.current.hourLength;

        //changeWhichClasses(GlobalMapRules.current.uptoWhichClassIsOpen);

        endGame = GameObject.Find("EndScreen");
        endGame.SetActive(false);

        //endGraph = GameObject.Find("EndGraph");
        //endGraph.SetActive(false);

        currGraph = GameObject.Find("CurrGraph");
        currGraph.GetComponent<Window_Graph>().makeAxes(SEIR_implementation.current.days_max, SEIR_implementation.current.population, 10, 10);
        currGraph.GetComponent<Window_Graph>().makeLine(SEIR_implementation.current.population, SEIR_implementation.current.maxHospitalBeds, Color.blue);

        pointsList_S = currGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max, Color.gray);
        pointsList_E = currGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max, new Color(1.0f, 0.64f, 0.0f));
        pointsList_I = currGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max, Color.red);
        pointsList_R = currGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max, Color.green);

        //GlobalEvents.current.changeSchoolClass((int)sliderSchoolOpen.GetComponent<Slider>().value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void updateGraph()
    {
       // List<float> valueList = SEIR_implementation.current.S_population;

        
        
        List<float> valueList = SEIR_implementation.current.S_population;
        currGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_S, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

        
        valueList = SEIR_implementation.current.E_population;
        currGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_E, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

        
        valueList = SEIR_implementation.current.I_population;
        currGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_I, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

        
        valueList = SEIR_implementation.current.R_population;
        currGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_R, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);
    }


    void showEndScreen(int showWhat)
    {
        if (showWhat != -1)
        {
            endGame.SetActive(true);
            foreach (Transform endChildObj in endGame.transform)
            {
                //if (endChildObj.name == "EndText" && showWhat == 1)
                //{
                //    endChildObj.GetComponent<Text>().text = "You Win!";
                //}
                //else if (endChildObj.name == "EndText" && showWhat == 0)
                //{
                //    endChildObj.GetComponent<Text>().text = "Sorry, You Lose!";
                //}

                if (endChildObj.name == "EndText_headline")
                {
                    if (showWhat == 0)
                    {
                        endChildObj.GetComponent<Text>().text = "Pandemic Ended on Day " + GlobalTimer.current.daysPassed;
                    }
                    else if (showWhat == 2)
                    {
                        endChildObj.GetComponent<Text>().text = "Pandemic Continues after Day " + SEIR_implementation.current.days_max;
                    }
                    else if (showWhat == 1)
                    {
                        endChildObj.GetComponent<Text>().text = "The Pandemic Prevention Budget Lasted until Day " + GlobalTimer.current.daysPassed;
                    }
                    else if (showWhat == 3)
                    {
                        endChildObj.GetComponent<Text>().text = "Mental health got too low on Day " + GlobalTimer.current.daysPassed;
                    }
                }

                if (endChildObj.name == "EndText")
                {
                    string endEconomy = "";
                    if (showWhat != 1)
                    {
                        endEconomy = "There was " + SEIR_implementation.current.startEconomy + " from the prevention budget left";
                    }
                    //else
                    //{
                    //    endEconomy = "The economy did not survive";
                    //}

                    string endMentalHealth = "";
                    if (showWhat != 3)
                    {
                        endMentalHealth = "You managed to preserve people's mental health at " + SEIR_implementation.current.currMentalHealth;
                    }
                    

                    string youSaved = "";
                    //if (showWhat == 1)
                    //{
                    youSaved = "You saved: " + ((SEIR_implementation.current.currRecovered + SEIR_implementation.current.currSusceptible) - DebriefingGather.current.maxDead).ToString();// + ", from them " + SEIR_implementation.current.currSusceptible.ToString() + " were never infected";
                   // }
                    //else
                    //{
                    //    youSaved = "You saved: " + (SEIR_implementation.current.currRecovered).ToString() + " ,but from them" + SEIR_implementation.current.currSusceptible.ToString() + " are still at risk";
                    //}


                    string endMessage = "You started with " + SEIR_implementation.current.population.ToString() + " people\n" +
                        DebriefingGather.current.sickPercent + "% got sick\n" +
                        DebriefingGather.current.hospitalizedPercent + "% were hospitalized\n" +
                         DebriefingGather.current.deadPercent + "% died\n" +
                        //youSaved + "\n" +
                         endEconomy + "\n" + endMentalHealth;


                    endChildObj.GetComponent<Text>().text = endMessage;
                }
            }

            // endGraph.SetActive(true);

            // SEIR_implementation.current.finalSEIR();


            // endGraph.GetComponent<Window_Graph>().makeAxes(SEIR_implementation.current.days_max, SEIR_implementation.current.population, 10, 10);


            // //List<GameObject> pointsList_S = endGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max);
            // List<float> valueList = SEIR_implementation.current.S_population;
            // endGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_S, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

            //// List<GameObject> pointsList_E = endGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max);
            // valueList = SEIR_implementation.current.E_population;
            // endGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_E, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

            // //List<GameObject> pointsList_I = endGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max);
            // valueList = SEIR_implementation.current.I_population;
            // endGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_I, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);

            // //List<GameObject> pointsList_R = endGraph.GetComponent<Window_Graph>().setupPoints(SEIR_implementation.current.days_max);
            // valueList = SEIR_implementation.current.R_population;
            // endGraph.GetComponent<Window_Graph>().ShowGraph(pointsList_R, valueList, SEIR_implementation.current.population, SEIR_implementation.current.days_max);





            //List<float> valueList = SEIR_implementation.current.S_population;
            //endGraph.GetComponent<Window_Graph>().ShowGraph(valueList, SEIR_implementation.current.population, Color.gray);

            //valueList = SEIR_implementation.current.E_population;
            //endGraph.GetComponent<Window_Graph>().ShowGraph(valueList, SEIR_implementation.current.population, new Color(1.0f, 0.64f, 0.0f));

            //valueList = SEIR_implementation.current.I_population;
            //endGraph.GetComponent<Window_Graph>().ShowGraph(valueList, SEIR_implementation.current.population, Color.red);

            //valueList = SEIR_implementation.current.R_population;
            //endGraph.GetComponent<Window_Graph>().ShowGraph(valueList, SEIR_implementation.current.population, Color.green);

            //endGraph.GetComponent<Window_Graph>().makeLine(SEIR_implementation.current.population, SEIR_implementation.current.maxHospitalBeds, Color.blue);
        }
    }


    //public void speedUpTime()
    //{

    //    if (GlobalTimer.current.hourLength > 0.2f)
    //    {
    //        GlobalTimer.current.hourLength -= 0.1f;

    //        GlobalTimer.current.dayLength = GlobalTimer.current.hourLength * 24f;
    //        GlobalTimer.current.timer = GlobalTimer.current.hourLength * (GlobalTimer.current.hoursPassed-1);
    //    }
        
    //    Debug.Log(GlobalTimer.current.hourLength);
    //}

    //public void speedDownTime()
    //{
    //    GlobalTimer.current.hourLength += 0.1f;
    //    //GlobalTimer.timer+= 0.1f;

    //    GlobalTimer.current.dayLength = GlobalTimer.current.hourLength * 24f;
    //    GlobalTimer.current.timer = GlobalTimer.current.hourLength * (GlobalTimer.current.hoursPassed - 1);
    //    Debug.Log(GlobalTimer.current.hourLength);
    //}

    public void timeSpeedControl(float timeSpeed)
    {
        //GlobalTimer.current.hourLength = 0.01f;
        GlobalTimer.current.hourLength = sliderTimeSpeed.GetComponent<Slider>().maxValue + 0.1f - timeSpeed;

        GlobalTimer.current.dayLength = GlobalTimer.current.hourLength * 24f;
        GlobalTimer.current.timer = GlobalTimer.current.hourLength * (GlobalTimer.current.hoursPassed - 1);

        Debug.Log(GlobalTimer.current.hourLength);
    }

    public void skipTime (int howMuch)
    {
        GlobalEvents.current.skipTimer(howMuch);
    }


    public void goingOut(bool currToggle)
    {
        if (currToggle)
        {
            SEIR_implementation.current.currRho += goingOutWithFriends_rho;
            SEIR_implementation.current.changeMentalHealthValue += goingOutWithFriends_mental;

            SEIR_implementation.current.changeEconomyValue -= goingOutWithFriends_economy;
        }
        else
        {
            SEIR_implementation.current.currRho -= goingOutWithFriends_rho;
            SEIR_implementation.current.changeMentalHealthValue -= goingOutWithFriends_mental;

            SEIR_implementation.current.changeEconomyValue += goingOutWithFriends_economy;
        }
    }

    public void doingSports(bool currToggle)
    {
        if (currToggle)
        {
            SEIR_implementation.current.currRho += outdoorSports_rho;
            SEIR_implementation.current.changeMentalHealthValue += outdoorSports_mental;

            SEIR_implementation.current.changeEconomyValue -= outdoorSports_economy;
        }
        else
        {
            SEIR_implementation.current.currRho -= outdoorSports_rho;
            SEIR_implementation.current.changeMentalHealthValue -= outdoorSports_mental;

            SEIR_implementation.current.changeEconomyValue += outdoorSports_economy;
        }
    }


    public void changeHandSanitizationTimes(float currValue)
    {

        textHandSanitize.GetComponent<Text>().text = "Hand sanitizations \n per day: " + currValue;

        if (currValue > oldSanitizationVal)
        {
            SEIR_implementation.current.currRho -= 0.1f / sliderHandSanitize.GetComponent<Slider>().maxValue;

            if (sliderHandSanitize.GetComponent<Slider>().normalizedValue > 0.9f)
            {
                
                SEIR_implementation.current.changeEconomyValue += 1;
            }

            if (sliderHandSanitize.GetComponent<Slider>().normalizedValue > 0.6f)
            {
                SEIR_implementation.current.changeMentalHealthValue -= 0.5f;
            }
        }
        else
        {
            SEIR_implementation.current.currRho += 0.1f / sliderHandSanitize.GetComponent<Slider>().maxValue;

            if (sliderHandSanitize.GetComponent<Slider>().normalizedValue > 0.8f)
            {
                
                SEIR_implementation.current.changeEconomyValue -= 1;
            }

            if (sliderHandSanitize.GetComponent<Slider>().normalizedValue >= 0.6f)
            {
                SEIR_implementation.current.changeMentalHealthValue += 0.5f;
            }
        }

        
        

        oldSanitizationVal = currValue;
    }

    //public void changeWhichClasses(float upToWhich)
    //{
    //    Debug.Log(upToWhich);
    //    GlobalEvents.current.changeSchoolClass((int)upToWhich);


    //    if (upToWhich == 0)
    //    {
    //        testSchoolOpen.transform.GetComponent<Text>().text = "School is closed";
    //    }
    //    else if (upToWhich == 1)
    //    {
    //        testSchoolOpen.transform.GetComponent<Text>().text = "First grade is open";
    //    }
    //    else
    //    {
    //        testSchoolOpen.transform.GetComponent<Text>().text = "Grades up to " + upToWhich.ToString() + " are open";
    //    }

    //}



}

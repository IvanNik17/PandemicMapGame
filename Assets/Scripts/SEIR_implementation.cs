using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SEIR_implementation : MonoBehaviour
{

    public static SEIR_implementation current;


    public float[] classToRho;

    public float incubation_period = 5f;

    public float infection_period = 2f;

    float alpha;
    float gamma;
    public float R_0_setup = 3.5f;

    float beta;

    //public float rho_open = 1f;
    //public float rho_closed = 0.4f;

    public float currRho = 1f;

    public float population = 5000f;

    float num_immune = 1f;
    public float num_exposed = 100f;

    float I_0;
    float S_0;
    float E_0;
    float R_0;

    


    public float days_max = 100f;
    float dt = 1f;

    Vector4 init_vals;

    Vector4 paramsToAdd;


    List<float> S;
    List<float> E;
    List<float> I;
    List<float> R;

    public List<float> S_population;
    public List<float> E_population;
    public List<float> I_population;
    public List<float> R_population;

    int counter = 0;

    List<float> time_space;

    int prevDay;

    public float currInfected = 0f;
    public float currRecovered = 0f;
    public float currExposed = 0f;
    public float currSusceptible = 0f;
    public float currDead = 0f;

    float prevInfected = 0;
    float prevExposed = 0;
    float prevDead = 0;

    float exposedCnt = 0;

    public int maxHospitalBeds = 10;


    public int maxEconomy = 200;
    public int startEconomy = 200;

    

    public int changeEconomyValue = 1;


    public float maxMentalHealth = 100f;
    public float currMentalHealth;

    public float changeMentalHealthValue = 0f;
    public float changeMentalHealthBase = 0f;


    //public int maxPossibleInfected;

    public infectedBar infectedBar;
    public EconomicsBar economicsBar;
    public MentalHealthBar mentalHealthBar;

    


    void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        //population = SetupResources.current.numberOfHouses * SetupResources.current.landMasses.Length;

        //maxPossibleInfected = (int)(population / 2);

        infectedBar.setMaxPossibleInfected(maxHospitalBeds);

        startEconomy = maxEconomy;

        economicsBar.setMaxPossibleEconomics(startEconomy);
        economicsBar.setEconomics(startEconomy);

        currMentalHealth = maxMentalHealth;
        mentalHealthBar.setMaxMentalHealth(maxMentalHealth);
        mentalHealthBar.setMentalHealth(maxMentalHealth);

        GlobalEvents.current.onDayPassed += ProgressSEIR;

        GlobalEvents.current.onDayPassed += InfectChildren;

       

        //GlobalEvents.current.onChangeSchoolClass += changeEconomy;

        //GlobalEvents.current.onChangeSchoolClass += changeRho;

        alpha = 1f / incubation_period;
        gamma = 1f / infection_period;
        beta = R_0_setup * gamma;

        Debug.Log(alpha.ToString() + ", " + beta.ToString() + ", " + gamma.ToString());


        I_0 = 0f;
        S_0 = 1f - (num_immune / population);
        E_0 = num_exposed / population;
        R_0 = 0f;

        init_vals = new Vector4(S_0, E_0, I_0, R_0);

        time_space = new List<float>();

        int counterInit = 0;
        for (float i = 0f; i < days_max; i += 1) //i += dt
        {
            time_space.Add(i);

            counterInit++;
        }


        //paramsToAdd = new Vector4(alpha, beta, gamma, rho);

        S = new List<float>();
        E = new List<float>();
        I = new List<float>();
        R = new List<float>();

        S_population = new List<float>();
        E_population = new List<float>();
        I_population = new List<float>();
        R_population = new List<float>();

        S.Add(S_0);
        E.Add(E_0);
        I.Add(I_0);
        R.Add(R_0);

        S_population.Add(S_0 * population);
        E_population.Add(E_0 * population);
        I_population.Add(I_0 * population);
        R_population.Add(R_0 * population);


        prevInfected = 1;
        prevExposed = 0;

        //prevDay = GlobalTimer.current.daysPassed;
        //float rho = 1f;
        //counter = 0;
        //string test = "";
        //while (counter < time_space.Count)
        //{
        //    float next_S = S[counter] - (rho * beta * S[counter] * I[counter]) * dt;
        //    float next_E = E[counter] + (rho * beta * S[counter] * I[counter] - alpha * E[counter]) * dt;
        //    float next_I = I[counter] + (alpha * E[counter] - gamma * I[counter]) * dt;
        //    float next_R = R[counter] + (gamma * I[counter]) * dt;

        //    //Debug.Log("Model output: " + next_S.ToString() + "|" + next_E.ToString() + "|" + next_I.ToString() + "|" + next_R.ToString());

        //    S.Add(next_S);
        //    E.Add(next_E);
        //    I.Add(next_I);
        //    R.Add(next_R);

        //    test += " ," + (next_E*population).ToString();

        //    counter++;
        //}


        //Debug.Log(test);



    }

    // Update is called once per frame
    void Update()
    {

        //int currDay = GlobalTimer.daysPassed;

        //if (currDay!= prevDay && counter < time_space.Count)
        //{
        //    float rho = 0;
        //    if (transform.GetComponent<Visualize>().isSchoolOpen)
        //    {
        //        rho = rho_open;
        //    }
        //    else
        //    {
        //        rho = rho_closed;
        //    }

        //    float next_S = S[counter] - (rho * beta * S[counter] * I[counter]) * dt;
        //    float next_E = E[counter] + (rho * beta * S[counter] * I[counter] - alpha * E[counter]) * dt;
        //    float next_I = I[counter] + (alpha * E[counter] - gamma * I[counter]) * dt;
        //    float next_R = R[counter] + (gamma * I[counter]) * dt;

        //    //Debug.Log("Model output: " + next_S.ToString() + "|" + next_E.ToString() + "|" + next_I.ToString() + "|" + next_R.ToString())

        //    //Debug.Log("Susceptible: " + (next_S* population).ToString() + "| Exposed: " + (next_E * population).ToString() + "| Infected: " + (next_I * population).ToString() + "| Recovered: " + (next_R * population).ToString());
        //    //Debug.Log("Infected: " + (next_I * population).ToString());

        //    currInfected = next_I * population;

        //    S.Add(next_S);
        //    E.Add(next_E);
        //    I.Add(next_I);
        //    R.Add(next_R);

        //    counter++;
        //}


        //prevDay = currDay;
    }


    void InfectChildren()
    {

        float newExposed = Mathf.Round(currExposed) - Mathf.Round(prevExposed);

        float newInfected = (float)(Math.Ceiling(currInfected) - Math.Ceiling(prevInfected));

        float newDead = Mathf.Round(currDead) - Mathf.Round(prevDead);
        //Debug.Log(currInfected + " | " + prevInfected + " = " + newInfected);

        //Debug.Log(newInfected);

        if (newExposed >= 1)
        {
            exposedCnt += newExposed;

            int newExpCounter = 0;
            List<GameObject> allChildrenList = SetupResources.current.allChildren;

            while (newExpCounter < newExposed)
            {

                int i = UnityEngine.Random.Range(0, SetupResources.current.allChildren.Count - 1);
                if (allChildrenList[i].GetComponent<ChildS>().currentState == 0)
                {
                    allChildrenList[i].GetComponent<ChildS>().currentState = 1;

                    allChildrenList[i].GetComponent<ChildS>().changeMaterial(1);

                    foreach (Transform childHome in allChildrenList[i].GetComponent<ChildS>().homeObject.transform)
                    {
                        if (childHome.name == "Infected")
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }

                    newExpCounter++;
                }
            }



            //for (int i = 0; i < allChildrenList.Count; i++)
            //{

            //    if (newExpCounter< newExposed && allChildrenList[i].GetComponent<ChildS>().currentState == 0)
            //    {
            //        allChildrenList[i].GetComponent<ChildS>().currentState = 1;

            //        allChildrenList[i].GetComponent<ChildS>().changeMaterial(1);

            //        foreach (Transform childHome in allChildrenList[i].GetComponent<ChildS>().homeObject.transform)
            //        {
            //            if (childHome.name == "Infected")
            //            {
            //                childHome.GetComponent<MeshRenderer>().enabled = true;
            //            }
            //            else
            //            {
            //                childHome.GetComponent<MeshRenderer>().enabled = false;
            //            }
            //        }

            //        newExpCounter++;
            //    }
            //}
        }

        //THERE MIGHT BE A NEED TO RANDOMIZE THE INFECTED and RECOVERED as WELL

        else if (newExposed <= -1)
        {
            int newRecCounter = 0;
            List<GameObject> allChildrenList = SetupResources.current.allChildren;
            for (int i = 0; i < allChildrenList.Count; i++)
            {

                if (newRecCounter < Mathf.Abs(newExposed) && allChildrenList[i].GetComponent<ChildS>().currentState == 1)
                {
                    allChildrenList[i].GetComponent<ChildS>().currentState = 3;

                    allChildrenList[i].GetComponent<ChildS>().changeMaterial(3);

                    foreach (Transform childHome in allChildrenList[i].GetComponent<ChildS>().homeObject.transform)
                    {
                        if (childHome.name == "Immune")
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }

                    newRecCounter++;
                }
            }

        }



        //Debug.Log("How many exposed" + exposedCnt.ToString());

        if (newInfected >= 1)
        {
            infectedBar.setInfected((int)Math.Round(currInfected));
            int newInfCounter = 0;
            List<GameObject> allChildrenList = SetupResources.current.allChildren;


            //for (int i = 0; i < allChildrenList.Count; i++)
            while (newInfCounter < newInfected)
            {
                int i = UnityEngine.Random.Range(0, SetupResources.current.allChildren.Count - 1);

                
                if (allChildrenList[i].GetComponent<ChildS>().currentState == 0)
                {
                    allChildrenList[i].GetComponent<ChildS>().currentState = 2;

                    allChildrenList[i].GetComponent<ChildS>().changeMaterial(2);

                    foreach (Transform childHome in allChildrenList[i].GetComponent<ChildS>().homeObject.transform)
                    {
                        if (childHome.name == "Infected")
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }

                    newInfCounter++;
                }
                
                
            }
        }
        else if (newInfected <= -1)
        {

            infectedBar.setInfected((int)Math.Round(currInfected));
            int newRecCounter = 0;
            List<GameObject> allChildrenList = SetupResources.current.allChildren;


            foreach (GameObject childObj in allChildrenList)
            {

                if (childObj.GetComponent<ChildS>().currentState == 2 && newRecCounter < Mathf.Abs(newInfected))
                {
                    childObj.GetComponent<ChildS>().currentState = 3;

                    childObj.GetComponent<ChildS>().changeMaterial(3);

                    foreach (Transform childHome in childObj.GetComponent<ChildS>().homeObject.transform)
                    {
                        if (childHome.name == "Immune")
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }

                    newRecCounter++;
                }
            }

            

        }


        if (newDead >= 1)
        {
            infectedBar.setInfected((int)Math.Round(currInfected));


            //If too many dead mental health degrades!!!!!:
            changeMentalHealthBase -= newDead;

            int newInfCounter = 0;
            List<GameObject> allChildrenList = SetupResources.current.allChildren;


            //for (int i = 0; i < allChildrenList.Count; i++)
            while (newInfCounter < newDead)
            {
                int i = UnityEngine.Random.Range(0, SetupResources.current.allChildren.Count - 1);


                if (allChildrenList[i].GetComponent<ChildS>().currentState == 0)
                {
                    allChildrenList[i].GetComponent<ChildS>().currentState = 4;

                    allChildrenList[i].GetComponent<ChildS>().changeMaterial(4);

                    foreach (Transform childHome in allChildrenList[i].GetComponent<ChildS>().homeObject.transform)
                    {
                        if (childHome.name == "Dead")
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            childHome.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }

                    newInfCounter++;
                }

            }
        }



        //List<GameObject> allChildrenListT = SetupResources.current.allChildren;
        //int cnt = 0;
        //foreach (GameObject childObj in allChildrenListT)
        //{
        //    if (childObj.GetComponent<ChildS>().currentState == 2)
        //    {
        //        cnt++;
        //    }
        //}
        //Debug.Log("Num infected: " + cnt);


        prevExposed = currExposed;
        prevInfected = currInfected;
        prevDead = currDead;
    }


    void ProgressSEIR()
    {

        if (counter < time_space.Count-1)
        {
            //float rho = 0;
            //if (transform.GetComponent<Visualize>().isSchoolOpen)
            //{
            //    rho = rho_open;
            //}
            //else
            //{
            //    rho = rho_closed;
            //}

            //Debug.Log(currRho);

            currRho = Mathf.Clamp(currRho, 0, 1);

            float next_S = S[counter] - (currRho * beta * S[counter] * I[counter]) * dt;
            float next_E = E[counter] + (currRho * beta * S[counter] * I[counter] - alpha * E[counter]) * dt;
            float next_I = I[counter] + (alpha * E[counter] - gamma * I[counter]) * dt;
            float next_R = R[counter] + (gamma * I[counter]) * dt;

            //Debug.Log("Model output: " + next_S.ToString() + "|" + next_E.ToString() + "|" + next_I.ToString() + "|" + next_R.ToString())

            //Debug.Log("Susceptible: " + (next_S* population).ToString() + "| Exposed: " + (next_E * population).ToString() + "| Infected: " + (next_I * population).ToString() + "| Recovered: " + (next_R * population).ToString());
            //Debug.Log("Infected: " + (next_I * population).ToString());



            //Debug.Log((Mathf.Round(next_I * population) - currInfected));
            //Currently dead are infected after hospital overflow

            if (Mathf.Round(next_I * population) <= maxHospitalBeds)
            {
                currInfected = Mathf.Round(next_I * population);
            }
            else if (Mathf.Round(next_I * population) > maxHospitalBeds )
            {

                currDead = Mathf.Round(next_I * population) - currInfected;
            }

            currRecovered = Mathf.Round(next_R * population);

            currExposed = Mathf.Round(next_E * population);

            currSusceptible = Mathf.Round(next_S * population);



            //infectedBar.setInfected((int)Math.Round(currInfected));

            //Change economy
            startEconomy -= changeEconomyValue;
            economicsBar.setEconomics(startEconomy);


            Debug.Log(changeMentalHealthValue + " | " + currMentalHealth);

            currMentalHealth = Mathf.Clamp(currMentalHealth + changeMentalHealthValue + changeMentalHealthBase, 0, maxMentalHealth);
            //if (currMentalHealth<= maxMentalHealth && currMentalHealth>= 0)
            //{
            //    Debug.Log("HEREEE");
            //    currMentalHealth += changeMentalHealthValue;
            //}

            mentalHealthBar.setMentalHealth(currMentalHealth);
            

            S.Add(next_S);
            E.Add(next_E);
            I.Add(next_I);
            R.Add(next_R);

            S_population.Add(Mathf.Round(next_S * population));
            E_population.Add(Mathf.Round(next_E * population));
            I_population.Add(Mathf.Round(next_I * population));
            R_population.Add(Mathf.Round(next_R * population));



            counter++;
        }

    }

    public void finalSEIR()
    {
        while (counter < time_space.Count-1)
        {

            float next_S = S[counter] - (currRho * beta * S[counter] * I[counter]) * dt;
            float next_E = E[counter] + (currRho * beta * S[counter] * I[counter] - alpha * E[counter]) * dt;
            float next_I = I[counter] + (alpha * E[counter] - gamma * I[counter]) * dt;
            float next_R = R[counter] + (gamma * I[counter]) * dt;

            S.Add(next_S);
            E.Add(next_E);
            I.Add(next_I);
            R.Add(next_R);

            S_population.Add(Mathf.Round(next_S * population));
            E_population.Add(Mathf.Round(next_E * population));
            I_population.Add(Mathf.Round(next_I * population));
            R_population.Add(Mathf.Round(next_R * population));

            counter++;
        }
        
    }


    //void changeRho(int schoolClasses)
    //{
    //    currRho = classToRho[schoolClasses];
    //}


    //void changeEconomy(int changeVal)
    //{
    //    if (changeVal>=0 && changeVal < 4)
    //    {
    //        changeEconomyValue = 4;
    //    }
    //    else if (changeVal>= 4 && changeVal< 7)
    //    {
    //        changeEconomyValue = 3;
    //    }
    //    else if (changeVal >= 7 && changeVal < 10)
    //    {
    //        changeEconomyValue = 2;
    //    }
    //    else if (changeVal >= 10 && changeVal < 12)
    //    {
    //        changeEconomyValue = 1;
    //    }



    //    //currChangeEconomyValue = changeEconomyValue + 4 - changeVal / 3;
    //}
}
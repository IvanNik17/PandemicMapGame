using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospitalize : MonoBehaviour
{

    public GameObject sickPersonPrefab;

    public GameObject recoveredPersonPrefab;

    public GameObject hospital;

    public GameObject outsideHospital;

    public List<GameObject> allHospitalized;
    public List<int> dayInfected;

    public GameObject visualBar;
    public GameObject visualMax;

    float barMaxPosHeight;

    //float changeBar = 0.5f;
    [Range(200.0f, 500.0f)]
    public float hospitalMax = 300f;

    float prevInfected;

    float timeStep;
    //float maxTimeInDay;

    float maxInstances = 24f; // maximum to fit in the smallest chosen timestep

    Vector3 hospitalPosOut;

    int recoverCounter = 0;
    // Start is called before the first frame update
    void Start()
    {

        timeStep = transform.GetComponent<GlobalTimer>().hourLength;

        
        prevInfected = SEIR_implementation.current.currInfected;

        hospitalPosOut = outsideHospital.transform.position;

        barMaxPosHeight = visualMax.transform.localScale.y;

        allHospitalized = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //maxTimeInDay = transform.GetComponent<GlobalTimer>().dayLength;
       
        

        float infected = SEIR_implementation.current.currInfected;
        float recovered = SEIR_implementation.current.currRecovered;

        float newInfected = infected - prevInfected;

        // Debug.Log(prevInfected.ToString() + " | " + infected.ToString());




        if (newInfected != 0 && newInfected > 0) //
        {

            Vector3 hospitalPos = Vector3.one;

            if (infected < hospitalMax)
            {
                hospitalPos = hospital.transform.position;
            }
            else
            {
                hospitalPos = hospitalPosOut;
                
            }


            if (Mathf.Abs(newInfected) < 1)
            {
                int whichHousehold = Random.Range(0, SetupResources.current.allHouseholds.Count);

                Vector3 homePos = SetupResources.current.allHouseholds[whichHousehold].transform.position;
                GameObject newInfPerson = Instantiate(sickPersonPrefab, homePos, Quaternion.identity);

                if (infected > hospitalMax)
                {
                    hospitalPosOut.x -= sickPersonPrefab.transform.localScale.x / 2f;
                    hospitalPos.x = hospitalPosOut.x;
                }


                StartCoroutine(moveBetweenHospital(0, newInfPerson, homePos, hospitalPos, timeStep * 2));

                allHospitalized.Add(newInfPerson);
                dayInfected.Add(GlobalTimer.current.daysPassed);

                Vector3 barScale = visualBar.transform.localScale;
                barScale.y = ((infected / hospitalMax) * barMaxPosHeight);
                visualBar.transform.localScale = barScale;
            }
            else if (Mathf.Abs(newInfected) > 1)
            {
                float currInstances = Mathf.Min(maxInstances / timeStep, newInfected);

               // Debug.Log(currInstances);
                for (int i = 0; i < (int)currInstances; i++)
                {
                    int whichHousehold = Random.Range(0, SetupResources.current.allHouseholds.Count);

                    Vector3 homePos = SetupResources.current.allHouseholds[whichHousehold].transform.position;
                    GameObject newInfPerson = Instantiate(sickPersonPrefab, homePos, Quaternion.identity);

                    if (infected > hospitalMax)
                    {
                        hospitalPosOut.x-= sickPersonPrefab.transform.localScale.x / 2f;
                        hospitalPos.x = hospitalPosOut.x;
                    }

                    StartCoroutine(moveBetweenHospital(i * (timeStep / 20), newInfPerson, homePos, hospitalPos, timeStep * 2));

                    allHospitalized.Add(newInfPerson);
                    dayInfected.Add(GlobalTimer.current.daysPassed);
                }

                Vector3 barScale = visualBar.transform.localScale;
                //barScale.y -= (changeBar * newInfected);
                barScale.y = ((infected / hospitalMax) * barMaxPosHeight);
                visualBar.transform.localScale = barScale;

            }     

        }
        else if (newInfected < 0)
        {
            if (recoverCounter< allHospitalized.Count)
            {
                if (Mathf.Abs(newInfected) < 1)
                {
                    int whichHousehold = Random.Range(0, SetupResources.current.allHouseholds.Count);

                    Vector3 homePos = SetupResources.current.allHouseholds[whichHousehold].transform.position;
                    GameObject newInfPerson = Instantiate(recoveredPersonPrefab, allHospitalized[recoverCounter].transform.position, Quaternion.identity);


                    StartCoroutine(moveBetweenHospital(0, newInfPerson, allHospitalized[recoverCounter].transform.position, homePos, timeStep * 2));

                    GameObject.Destroy(allHospitalized[recoverCounter]);
                    allHospitalized[recoverCounter] = newInfPerson;

                    Vector3 barScale = visualBar.transform.localScale;
                    barScale.y = ((infected / hospitalMax) * barMaxPosHeight);
                    visualBar.transform.localScale = barScale;

                    recoverCounter++;
                }
                else if (Mathf.Abs(newInfected) > 1)
                {
                    float currInstances = Mathf.Min(maxInstances / timeStep, Mathf.Abs(newInfected));

                    // Debug.Log(currInstances);
                    for (int i = 0; i < (int)currInstances; i++)
                    {
                        int whichHousehold = Random.Range(0, SetupResources.current.allHouseholds.Count);

                        Vector3 homePos = SetupResources.current.allHouseholds[whichHousehold].transform.position;
                        GameObject newInfPerson = Instantiate(recoveredPersonPrefab, allHospitalized[recoverCounter].transform.position, Quaternion.identity);


                        StartCoroutine(moveBetweenHospital(i * (timeStep / 20), newInfPerson, allHospitalized[recoverCounter].transform.position, homePos, timeStep * 2));

                        GameObject.Destroy(allHospitalized[recoverCounter]);
                        allHospitalized[recoverCounter] = newInfPerson;

                        recoverCounter++;


                    }

                    Vector3 barScale = visualBar.transform.localScale;
                    //barScale.y -= (changeBar * newInfected);
                    barScale.y = ((infected / hospitalMax) * barMaxPosHeight);
                    visualBar.transform.localScale = barScale;

                }
            }
            

            
        }
       // Debug.Log(allHospitalized.Count);

        


        prevInfected = infected;
    }



    public IEnumerator moveBetweenHospital(float delayTime, GameObject movedObj, Vector3 oldPositions, Vector3 newPositions, float timeStepSpeed)
    {
        yield return new WaitForSeconds(delayTime); // start at time X

        //float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        float startTime = GlobalTimer.current.timer;
        while (GlobalTimer.current.timer - startTime <= timeStepSpeed + delayTime)
        { // until one second passed

            movedObj.transform.position = Vector3.Lerp(oldPositions, newPositions, (GlobalTimer.current.timer - startTime)/ timeStepSpeed); // lerp from A to B in one second


            yield return timeStepSpeed; // wait for next frame
        }

    }
}

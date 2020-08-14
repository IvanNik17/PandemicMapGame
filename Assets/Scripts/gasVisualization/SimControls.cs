using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimControls : MonoBehaviour
{
    public bool pauseSim = false;

    public bool endSim = false;

    [Range(0.0f, 1.0f)]
    public float percentShutdown;

    [Range(0.0f, 1.0f)]
    public float R_0_percent;

    [Range(0.0f, 1.0f)]
    public float spreadRadius;

    float oldShutdown;



    // Start is called before the first frame update
    void Start()
    {

        GameObject sliderShutdown = GameObject.Find("Slider_shutdown");
        sliderShutdown.GetComponent<Slider>().value = percentShutdown*10;
        oldShutdown = percentShutdown;



        GameObject sliderR_0 = GameObject.Find("R_0");
        sliderR_0.GetComponent<Slider>().value = R_0_percent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.GetComponent<InfectionCounter>().numInfected == 0 && transform.GetComponent<InfectionCounter>().numHealed != 0)
        {
            endSim = true;
        }


        if (percentShutdown != oldShutdown)
        {
            //changeShutdown();
            //float numActors = transform.GetComponent<SpawnObjects>().numberOfActors;

            //List<GameObject> allActors_curr = transform.GetComponent<SpawnObjects>().allActors;
            //int quarantine_max = (int)(numActors * percentShutdown);
            //int quarantine_counter = 0;

            //if (percentShutdown > oldShutdown)
            //{
            //    quarantine_counter = (int)(numActors * oldShutdown);
            //    for (int i = 0; i < numActors; i++)
            //    {
            //        if (quarantine_counter < quarantine_max)
            //        {
            //            allActors_curr[i].transform.GetComponent<BallBounce>().isMoving = false;
            //            quarantine_counter++;
            //        }
            //    }
            //}
            //else
            //{
            //    Debug.Log(quarantine_max);
            //    quarantine_max = (int)(numActors * oldShutdown) - (int)(numActors * percentShutdown);

            //    Debug.Log(quarantine_max);
            //    for (int i = 0; i < numActors; i++)
            //    {
            //        if (quarantine_counter < quarantine_max)
            //        {
            //            allActors_curr[i].transform.GetComponent<BallBounce>().isMoving = true;
            //            quarantine_counter++;
            //        }
            //    }
            //}

        }




        //oldShutdown = percentShutdown;
    }


    public void changeR_0(float newValue)
    {
        R_0_percent = newValue;

    }

    public void changeSpreadRad(float newValue)
    {
        spreadRadius = newValue;

        List<GameObject> allActors_curr = transform.GetComponent<SpawnObjects>().allActors;


        for (int i = 0; i < allActors_curr.Count; i++)
        {


            allActors_curr[i].transform.localScale = this.transform.GetComponent<SpawnObjects>().initialSize + Vector3.one * transform.GetComponent<SimControls>().spreadRadius;



        }


        

        


    }


    public void changeShutdown(float newValue)
    {

        percentShutdown = (float)newValue / 10f;
        float numActors = transform.GetComponent<SpawnObjects>().numberOfActors;

        List<GameObject> allActors_curr = transform.GetComponent<SpawnObjects>().allActors;
        int quarantine_max = (int)(numActors * percentShutdown);
        int quarantine_counter = 0;

        if (percentShutdown > oldShutdown)
        {
            quarantine_counter = (int)(numActors * oldShutdown);
            for (int i = 0; i < numActors; i++)
            {
                if (quarantine_counter < quarantine_max)
                {
                    if (allActors_curr[i].transform.GetComponent<BallBounce>().isMoving == true)
                    {
                        if (transform.GetComponent<InfectionCounter>().numInfected <=1 && allActors_curr[i].transform.GetComponent<BallBounce>().currState == 2)
                        {
                            continue;
                        }
                        else
                        {
                            allActors_curr[i].transform.GetComponent<BallBounce>().isMoving = false;

                            quarantine_counter++;
                        }
                        
                    }
                    
                    
                }
            }
        }
        else
        {
            Debug.Log(quarantine_max);
            quarantine_max = (int)(numActors * oldShutdown) - (int)(numActors * percentShutdown);

            Debug.Log(quarantine_max);
            for (int i = 0; i < numActors; i++)
            {
                if (quarantine_counter < quarantine_max)
                {
                    if (allActors_curr[i].transform.GetComponent<BallBounce>().isMoving == false)
                    {
                        allActors_curr[i].transform.GetComponent<BallBounce>().isMoving = true;

                        quarantine_counter++;
                    }
                    
                    
                }
            }
        }

        oldShutdown = percentShutdown;
    }



}

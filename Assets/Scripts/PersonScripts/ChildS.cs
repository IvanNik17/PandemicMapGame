using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildS : Person
{
    // Start is called before the first frame update

    public string schoolAddress;
    public GameObject schoolObject;

    public int grade;

    public int randHourFirst = 0;
    public int randHourSecond = 0;
    
    void Start()
    {
        //managerObject = GameObject.Find("Manager");

        GlobalEvents.current.onGoToPlace += moveToSchool;

        GlobalEvents.current.onBackFromPlace += goHomeFromSchool;


    }

    // Update is called once per frame
    void Update()
    {


        // Fix problem with multiple calls - Maybe with EVENT
        //if (!isMoving && currentPlace==0 && GlobalTimer.hoursPassed == 8 && GameObject.Find("Manager").transform.GetComponent<Visualize>().isSchoolOpen)
        //{
        //    Debug.Log("HERE " + transform.name);
        //    StartCoroutine(movePerson(0, homeObject.transform.position, schoolObject.transform.position, 2));
        //}
        //else if (!isMoving && currentPlace == 2 &&  GlobalTimer.hoursPassed == 15 && GameObject.Find("Manager").transform.GetComponent<Visualize>().isSchoolOpen)
        //{
        //    StartCoroutine(movePerson(0, schoolObject.transform.position, homeObject.transform.position, 0));
        //}

        if (currentState == 2 && currentPlace != 1)
        {
            
            if (!isMoving)
            {

                StartCoroutine(movePerson(0, homeObject.transform.position, hospitalObject.transform.position, 1));
            }
            
        }

        if (currentState == 4 && currentPlace != 1)
        {
            if (!isMoving)
            {

                //changeMaterial(4);
                Vector3 deadPos = homeObject.transform.position;
                deadPos.y += 100;
                StartCoroutine(movePerson_slow(0, homeObject.transform.position, deadPos, 3, 5));
                //currentState = 4;
            }
        }


        if (currentState == 3 && currentPlace == 1)
        {
            if (!isMoving)
            {

                StartCoroutine(movePerson(0, hospitalObject.transform.position, homeObject.transform.position, 0));
            }
        }

        //Test for random movement if you remove it get the GLobalTimer calculated movement back
        //if (grade <= GlobalMapRules.current.uptoWhichClassIsOpen && currentState != 2 && currentPlace == 0 && !isMoving)
        //{
        //    StartCoroutine(movePerson_slow(Random.Range(0,10), homeObject.transform.position, schoolObject.transform.position, 2,5));
        //}
        //else if (grade <= GlobalMapRules.current.uptoWhichClassIsOpen && currentState != 2 && currentPlace == 2 && !isMoving)
        //{
        //    StartCoroutine(movePerson_slow(Random.Range(0, 10), schoolObject.transform.position, homeObject.transform.position, 0,5));
        //}

    }




    //There's a problem with going back from school!!!
    void moveToSchool(int place)
    {
        // if (grade<=GlobalMapRules.current.uptoWhichClassIsOpen && place == 2)
        if (GlobalMapRules.current.whichClassesAreOpen[grade-1] == true && place == 2)
        {
            if (!isMoving && currentPlace == 0 )
            {
                //Debug.Log("HERE " + transform.name);
                StartCoroutine(movePerson(0, homeObject.transform.position, schoolObject.transform.position, 2));
            }
        }


        
    }

    void goHomeFromSchool(int place)
    {
        if (place == 2)
        {
            if (!isMoving && currentPlace == 2 )
            {
                StartCoroutine(movePerson(0, schoolObject.transform.position, homeObject.transform.position, 0));
            }
        }
        
    }


}

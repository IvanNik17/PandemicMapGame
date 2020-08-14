using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{

    public GameObject homeObject;
    public string houseAddress;
    public int currentState; // 0 - susceptable, 1-infectedNoSymptoms, 2-infectedWithSymptoms, 3-recovered, 4-dead
    public int currentPlace; // 0 - house, 1- hospital, 2 - school

    public GameObject hospitalObject;

    public int age;

    public bool isMoving = false;

    

    //public GameObject managerObject;

    // Start is called before the first frame update
    void Start()
    {
        //managerObject = GameObject.Find("Manager");

        


        currentState = 0;

        isMoving = false;

        changeMaterial(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual  void changeMaterial(int currState)
    {
        switch (currState)
        {
            case 0:
                transform.GetComponentInChildren<Renderer>().material = SetupResources.current.possibleStatesMat[0];
                break;
            case 1:
                transform.GetComponentInChildren<Renderer>().material = SetupResources.current.possibleStatesMat[1];
                break;
            case 2:
                transform.GetComponentInChildren<Renderer>().material = SetupResources.current.possibleStatesMat[2];
                break;
            case 3:
                transform.GetComponentInChildren<Renderer>().material = SetupResources.current.possibleStatesMat[3];
                break;
            case 4:
                transform.GetComponentInChildren<Renderer>().material = SetupResources.current.possibleStatesMat[4];
                break;
            default:
                break;
        }

    }


    public virtual IEnumerator movePerson_slow(float delayTime, Vector3 oldPositions, Vector3 newPositions, int newPlace, int speedMovement)
    {
        
        isMoving = true;
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= speedMovement)
        { // until one second passed

            transform.position = Vector3.Lerp(oldPositions, newPositions, (Time.time - startTime)/ speedMovement); // lerp from A to B in one second


            yield return 1; // wait for next frame
        }
        
        transform.position = newPositions;

        if (newPlace == 3)
        {
            
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        currentPlace = newPlace;
        isMoving = false;
        
        yield return new WaitForSeconds(delayTime); // start at time X
        
    }

    public virtual IEnumerator movePerson(float delayTime, Vector3 oldPositions, Vector3 newPositions, int newPlace)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        isMoving = true;
        float startTime = GlobalTimer.current.timer; // Time.time contains current frame time, so remember starting point
        while (GlobalTimer.current.timer - startTime <= GlobalTimer.current.hourLength)
        { // until one second passed

            transform.position = Vector3.Lerp(oldPositions, newPositions, (GlobalTimer.current.timer - startTime) / GlobalTimer.current.hourLength); // lerp from A to B in one second

            
            yield return GlobalTimer.current.hourLength; // wait for next frame
        }

        transform.position = newPositions;

        currentPlace = newPlace;
        isMoving = false;
    }


    //IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    //{

    //    isMoving = true;
    //    float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
    //    float t = 0;
    //    while (t <= 1.0f)
    //    {
    //        t += step; // Goes from 0 to 1, incrementing by step each time
    //        transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
    //        yield return GlobalTimer.current.hourLength;         // Leave the routine and return here in the next frame
    //    }
    //    transform.position = b;

    //    currentPlace = newPlace;
    //    isMoving = false;
    //}

    //public IEnumerator moveBetweenHospital(float delayTime, GameObject movedObj, Vector3 oldPositions, Vector3 newPositions, float timeStepSpeed)
    //{
    //    yield return new WaitForSeconds(delayTime); // start at time X

    //    //float startTime = Time.time; // Time.time contains current frame time, so remember starting point
    //    float startTime = GlobalTimer.timer;
    //    while (GlobalTimer.timer - startTime <= timeStepSpeed + delayTime)
    //    { // until one second passed

    //        movedObj.transform.position = Vector3.Lerp(oldPositions, newPositions, (GlobalTimer.timer - startTime) / timeStepSpeed); // lerp from A to B in one second


    //        yield return timeStepSpeed; // wait for next frame
    //    }

    //}


}

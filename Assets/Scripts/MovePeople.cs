using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePeople : MonoBehaviour
{




    //List<GameObject> allChildren_obj;
    //List<Vector3> allChildren_initPos;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < transform.GetComponent<SpawnFamily>().allHouseholds.Count; i++)
        //{

        //    GameObject currHousehold = transform.GetComponent<SpawnFamily>().allHouseholds[i];

            

        //    for (int j = 0; j < currHousehold.GetComponent<House>().residentChildren.Count; j++)
        //    {

        //        GameObject currChild = currHousehold.GetComponent<House>().residentChildren[j];

        //        Debug.Log(currChild.name);

        //    }

        //}


        //if (GlobalTimer.hoursPassed == 8 && transform.GetComponent<Visualize>().isSchoolOpen)
        //{
        //    Debug.Log("Here");
        //    for (int i = 0; i < transform.GetComponent<SpawnFamily>().allHouseholds.Count; i++)
        //    {

        //        GameObject currHousehold = transform.GetComponent<SpawnFamily>().allHouseholds[i];

        //        for (int j = 0; j < currHousehold.GetComponent<House>().residentChildren.Count; j++)
        //        {

        //            GameObject currChild = currHousehold.GetComponent<House>().residentChildren[j];

        //            if (currChild.GetComponent<ChildS>().currentPlace == 0)
        //            {
        //                StartCoroutine(currChild.GetComponent<ChildS>().movePerson(0, currChild.GetComponent<ChildS>().homeObject.transform.position, currChild.GetComponent<ChildS>().schoolObject.transform.position,2));

        //            }

        //        }

        //    }


        //    //StartCoroutine(WaitAndMove(0, transform.GetComponent<SpawnFamily>().allChildren, transform.GetComponent<SpawnFamily>().initialPos, transform.GetComponent<SpawnFamily>().schoolPos_perPerson));
        //}
        //else if (GlobalTimer.hoursPassed == 15 && transform.GetComponent<Visualize>().isSchoolOpen)
        //{
        //    //StartCoroutine(WaitAndMove(0, transform.GetComponent<SpawnFamily>().allChildren, transform.GetComponent<SpawnFamily>().schoolPos_perPerson, transform.GetComponent<SpawnFamily>().initialPos));

        //    for (int i = 0; i < transform.GetComponent<SpawnFamily>().allHouseholds.Count; i++)
        //    {

        //        GameObject currHousehold = transform.GetComponent<SpawnFamily>().allHouseholds[i];

        //        for (int j = 0; j < currHousehold.GetComponent<House>().residentChildren.Count; j++)
        //        {

        //            GameObject currChild = currHousehold.GetComponent<House>().residentChildren[j];

        //            if (currChild.GetComponent<ChildS>().currentPlace == 2)
        //            {
        //                StartCoroutine(currChild.GetComponent<ChildS>().movePerson(0, currChild.GetComponent<ChildS>().schoolObject.transform.position, currChild.GetComponent<ChildS>().homeObject.transform.position, 0));

        //            }

        //        }

        //    }
        //}

    }

    //public static IEnumerator WaitAndMove(float delayTime, List<GameObject> objectsToMove, List<Vector3> oldPositions, List<Vector3> newPositions)
    //{
    //    yield return new WaitForSeconds(delayTime); // start at time X
    //    float startTime = Time.time; // Time.time contains current frame time, so remember starting point
    //    while (Time.time - startTime <= 1)
    //    { // until one second passed

    //        for (int i = 0; i < objectsToMove.Count; i++)
    //        {

    //            objectsToMove[i].transform.position = Vector3.Lerp(oldPositions[i], newPositions[i], Time.time - startTime); // lerp from A to B in one second
    //        }


    //        yield return 1; // wait for next frame
    //    }
    //}
}

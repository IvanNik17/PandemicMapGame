using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTimeline : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dayVisualPrefab;

    public GameObject circleCenter;
    public GameObject pillarHolder;


    public List<GameObject> allPillars;
    public List<Vector3> startPos;

    public int numObjects = 10;

    public float radius = 1f;

    bool isMoving = false;

    public float rotSpeed = 1f;

    void Start()
    {

        allPillars = new List<GameObject>();
        startPos = new List<Vector3>();

        Vector3 newCenterPos = Camera.main.transform.position;
        newCenterPos.z += 1.2f*radius;
        circleCenter.transform.position = newCenterPos;

        Vector3 center = circleCenter.transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            float a = 360f / (float)numObjects * (float)i;
            //Debug.Log(a);
            Vector3 pos = CirclePos(center, radius, a);
            GameObject currPillar = Instantiate(dayVisualPrefab, pos, Quaternion.identity);

            currPillar.GetComponentInChildren<TextMesh>().text = i.ToString();

            allPillars.Add(currPillar);
            startPos.Add(pos);

            currPillar.transform.parent = pillarHolder.transform;
        }
    }


    Vector3 CirclePos(Vector3 center, float radius, float a)
    {
        
        float ang = -a;
        Vector3 pos;
        pos.x = center.x - radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z - radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }

    public virtual IEnumerator moveToPos(float delayTime, GameObject moveObj, Vector3 oldPositions, Vector3 newPositions, float speedMovement)
    {
        isMoving = true;
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= speedMovement)
        { // until one second passed

            moveObj.transform.position = Vector3.Lerp(oldPositions, newPositions, (Time.time - startTime) / speedMovement); // lerp from A to B in one second


            yield return 1; // wait for next frame
        }

        moveObj.transform.position = newPositions;

        yield return new WaitForSeconds(delayTime); // start at time X
        isMoving = false;
    }


    void moveBackwards(float speed)
    {
        Vector3 firstPos = allPillars[0].transform.position;
        for (int i = 0; i < allPillars.Count; i++)
        {

            Vector3 newPos = Vector3.zero;
            if (i != allPillars.Count - 1)
            {
                newPos = allPillars[i + 1].transform.position;
                Vector3 oldPos = allPillars[i].transform.position;
                StartCoroutine(moveToPos(0, allPillars[i], oldPos, newPos, speed));
                //allPillars[i].transform.position = newPos;
            }
            else
            {
                Vector3 oldPos = allPillars[i].transform.position;
                StartCoroutine(moveToPos(0, allPillars[i], oldPos, firstPos, speed));
                //allPillars[i].transform.position = firstPos;
            }
        }
    }

    void moveForwards(float speed)
    {
        Vector3 lastPos = allPillars[allPillars.Count - 1].transform.position;
        for (int i = allPillars.Count - 1; i >= 0; i--)
        {

            Vector3 newPos = Vector3.zero;
            if (i != 0)
            {
                newPos = allPillars[i - 1].transform.position;
                Vector3 oldPos = allPillars[i].transform.position;
                StartCoroutine(moveToPos(0, allPillars[i], oldPos, newPos, speed));
                //allPillars[i].transform.position = newPos;
            }
            else
            {
                Vector3 oldPos = allPillars[i].transform.position;
                StartCoroutine(moveToPos(0, allPillars[i], oldPos, lastPos, speed));
                //allPillars[i].transform.position = lastPos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            moveBackwards(rotSpeed);
        }

        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            moveForwards(rotSpeed);
        }


        
    }
}

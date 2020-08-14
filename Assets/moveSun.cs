using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSun : MonoBehaviour
{

    public GameObject sun;

    float sunInitialIntensity;

    float currentTimeOfDay;
    // Start is called before the first frame update
    void Start()
    {
        sunInitialIntensity = sun.transform.GetComponent<Light>().intensity;

        GlobalEvents.current.onHourPassed += moveSunAround;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // currentTimeOfDay = GlobalTimer.current.hoursPassed / 24f;
        //Debug.Log(currentTimeOfDay);
        //sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170, 0);
        //sun.transform.localRotation = Quaternion.Lerp(sun.transform.rotation, Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170, 0), (Time.time)*GlobalTimer.current.hourLength);
        
    }

    void moveSunAround()
    {
        currentTimeOfDay = GlobalTimer.current.hoursPassed / 24f;
        //Debug.Log(currentTimeOfDay);
        //sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170, 0);
        //sun.transform.localRotation = Quaternion.Lerp(sun.transform.rotation, Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170, 0), GlobalTimer.current.timer*10f);

        StartCoroutine(rotateSun(0, Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170, 0)));
        //float intensityMultiplier = 1;
        //if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        //{
        //    intensityMultiplier = 0;
        //}
        //else if (currentTimeOfDay <= 0.25f)
        //{
        //    intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        //}
        //else if (currentTimeOfDay >= 0.73f)
        //{
        //    intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        //}

        //sun.transform.GetComponent<Light>().intensity = sunInitialIntensity * intensityMultiplier;

    }


    public virtual IEnumerator rotateSun(float delayTime, Quaternion newRotation)
    {
         // start at time X
        
        float startTime = GlobalTimer.current.timer; // Time.time contains current frame time, so remember starting point
        while (GlobalTimer.current.timer - startTime <= GlobalTimer.current.hourLength)
        { // until one second passed

            //sun.transform.localRotation = Vector3.Lerp(oldPositions, newPositions, (GlobalTimer.current.timer - startTime) / GlobalTimer.current.hourLength); // lerp from A to B in one second
            sun.transform.localRotation = Quaternion.Lerp(sun.transform.rotation, newRotation, (GlobalTimer.current.timer - startTime) / GlobalTimer.current.hourLength);

            yield return GlobalTimer.current.hourLength; // wait for next frame
        }

        sun.transform.localRotation = newRotation;

        yield return new WaitForSeconds(delayTime);
    }


}

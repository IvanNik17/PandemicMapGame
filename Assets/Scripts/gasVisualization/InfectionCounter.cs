using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionCounter : MonoBehaviour
{
    public float numHealthy = 0f;
    public float numInfected = 0f;
    public float numHealed = 0f;

    float heightNormalizerMax = 5f;

    public GameObject barHealthy_prefab;
    public GameObject barInfected_prefab;
    public GameObject barHealed_prefab;


    GameObject barHealthy_main;
    GameObject barInfected_main;
    GameObject barHealed_main;

    TextMesh textHealthy;
    TextMesh textInfected;
    TextMesh textHealed;

    int oldScale_counter = 2;

    float timewise_boxSize = 10f;
    float timewise_maxInBox = 1000f;


    GameObject healthy_startPos;
    GameObject infected_startPos;
    GameObject healed_startPos;


    List<CombineInstance> allBars_healthy;
    List<CombineInstance> allBars_infected;
    List<CombineInstance> allBars_healed;

    float drawTime = 0.1f;
    float currTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        GameObject barHealthy_mainPos = GameObject.Find("barHealthy_mainPos");
        GameObject barInfected_mainPos = GameObject.Find("barInfected_mainPos");
        GameObject barHealed_mainPos = GameObject.Find("barHealed_mainPos");

        healthy_startPos = GameObject.Find("healthy_timewiseStart");
        infected_startPos = GameObject.Find("infected_timewiseStart");
        healed_startPos = GameObject.Find("healed_timewiseStart");

        allBars_healthy = new List<CombineInstance>();
        allBars_infected = new List<CombineInstance>();
        allBars_healed = new List<CombineInstance>();

        barHealthy_main = Instantiate(barHealthy_prefab, barHealthy_mainPos.transform.position, Quaternion.identity);
        barInfected_main = Instantiate(barInfected_prefab, barInfected_mainPos.transform.position, Quaternion.identity);
        barHealed_main = Instantiate(barHealed_prefab, barHealed_mainPos.transform.position, Quaternion.identity);

        textHealthy = GameObject.Find("textHealthy").GetComponent<TextMesh>();
        textInfected = GameObject.Find("textInfected").GetComponent<TextMesh>();
        textHealed = GameObject.Find("textHealed").GetComponent<TextMesh>();

        //Vector3 barHealed_currScale = barHealed.transform.localScale;
        //barHealed_currScale.y = 0;
        //barHealed.transform.localScale = barHealed_currScale;
        changeScale(barHealed_main, 0, heightNormalizerMax);



        //Vector3 oldPos = timeScale_startPos.transform.position;

        //oldPos.z += ((float)oldScale_counter * barHealthy_main.transform.localScale.z) / (timewise_maxInBox / timewise_boxSize);
        //Debug.Log(oldPos.z);
        //GameObject prevOldBar = Instantiate(barHealthy_prefab, oldPos, Quaternion.identity);
        //Vector3 oldBar_scale = prevOldBar.transform.localScale;
        //oldBar_scale.y = old_numHealthy / heightNormalizerMax;
        //oldBar_scale.z /= (timewise_maxInBox / timewise_boxSize);
        //prevOldBar.transform.localScale = oldBar_scale;
        //oldScale_counter++;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!transform.GetComponent<SimControls>().pauseSim && !transform.GetComponent<SimControls>().endSim)
        {
            currTime += Time.fixedDeltaTime;
            
            if (currTime >= drawTime)
            {

                currTime = 0;

                createTimePlot(barInfected_main, barInfected_prefab, numInfected, oldScale_counter, numInfected, numHealthy, "infected", infected_startPos, allBars_infected);

                createTimePlot(barHealthy_main, barHealthy_prefab, numHealthy, oldScale_counter, numInfected,numHealthy,"healthy", healthy_startPos, allBars_healthy);

                createTimePlot(barHealed_main, barHealed_prefab, numHealed, oldScale_counter, numInfected, numHealthy, "healed", healed_startPos, allBars_healed);

                oldScale_counter++;

                //instantiate old scales:
                //Vector3 oldPos = Vector3.zero;

                //oldPos.z += ((float)oldScale_counter * barHealthy_main.transform.localScale.z) / ((timewise_maxInBox / timewise_boxSize)* drawTime);
                //Debug.Log(oldPos.z);
                //GameObject oldBar = Instantiate(barHealthy_prefab, oldPos, Quaternion.identity);
                //Vector3 oldBar_scale = oldBar.transform.localScale;
                //oldBar_scale.y = numHealthy / heightNormalizerMax;
                //oldBar_scale.z /= ((timewise_maxInBox / timewise_boxSize)* drawTime);
                //oldBar.transform.localScale = oldBar_scale;
                //oldScale_counter++;

                //MeshFilter currBarMeshFilter = oldBar.GetComponentsInChildren<MeshFilter>()[0];

                //Destroy(oldBar);


                //CombineInstance currCombine = new CombineInstance();

                //currCombine.mesh = currBarMeshFilter.sharedMesh;
                //currCombine.transform = currBarMeshFilter.transform.localToWorldMatrix;

                //allBars.Add(currCombine);

                ////currBarMeshFilter.gameObject.SetActive(false);

                //timeScale_startPos.GetComponent<MeshFilter>().mesh = new Mesh();
                //timeScale_startPos.GetComponent<MeshFilter>().mesh.CombineMeshes(allBars.ToArray());


            }





            



            changeScale(barHealthy_main, numHealthy, heightNormalizerMax);
            changeScale(barInfected_main, numInfected, heightNormalizerMax);
            changeScale(barHealed_main, numHealed, heightNormalizerMax);
            //Debug.Log(numHealed);

            

            



        }

        textHealthy.text = numHealthy.ToString();
        textInfected.text = numInfected.ToString();
        textHealed.text = numHealed.ToString();

    }

    void changeScale(GameObject currScaleObj, float changeAmount, float normalizerMax)
    {
        Vector3 currScale = currScaleObj.transform.localScale;
        currScale.y = changeAmount/ normalizerMax;
        currScaleObj.transform.localScale = currScale;
    }

    void createTimePlot(GameObject currBar, GameObject currPrefab, float currNumber, int currCounter, float currNum_infected, float currNum_healthy, string whichBar, GameObject mergerObj, List<CombineInstance> allBars)
    {

        //, string whichBar
        //instantiate old scales:
        Vector3 oldPos = Vector3.zero;

        float scaleYdir = 5f / transform.GetComponent<SpawnObjects>().numberOfActors;

        oldPos.z += ((float)currCounter * currBar.transform.localScale.z) / ((timewise_maxInBox / timewise_boxSize) * drawTime);

        if (whichBar == "healthy")
        {
            oldPos.y += (currNum_infected * scaleYdir);
        }
        else if (whichBar == "healed")
        {
            oldPos.y += ((currNum_infected + currNum_healthy) * scaleYdir);
        }

        //Debug.Log(oldPos.z);
        GameObject oldBar = Instantiate(currPrefab, oldPos, Quaternion.identity);
        Vector3 oldBar_scale = oldBar.transform.localScale;
        oldBar_scale.y = (currNumber * scaleYdir);
        oldBar_scale.z /= ((timewise_maxInBox / timewise_boxSize) * drawTime);
        oldBar.transform.localScale = oldBar_scale;
        

        MeshFilter currBarMeshFilter = oldBar.GetComponentsInChildren<MeshFilter>()[0];

        Destroy(oldBar);


        CombineInstance currCombine = new CombineInstance();

        currCombine.mesh = currBarMeshFilter.sharedMesh;
        currCombine.transform = currBarMeshFilter.transform.localToWorldMatrix;

        allBars.Add(currCombine);


        mergerObj.GetComponent<MeshFilter>().mesh = new Mesh();
        mergerObj.GetComponent<MeshFilter>().mesh.CombineMeshes(allBars.ToArray());

    }

}

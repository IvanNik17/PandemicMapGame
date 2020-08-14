using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetupResources : MonoBehaviour
{
    // Start is called before the first frame update

    public static SetupResources current;

    public Material[] possibleStatesMat;

    [SerializeField] public string[] regionNumbers; // first number  of address - DK-81 - northJutland, DK-82 - central, DK-83 - southern, DK-84 - central, DK-85 - zealand

    [SerializeField] public string[] starterNumber; // houses - 0, hospitals - 1, school - 2

    



    public int houseMaxCapacity = 4;
    public int schoolMaxCapacity = 100;
    public int hospitalMaxCapacity = 50;


    public GameObject housePrefab_susceptible;
    //public GameObject housePrefab_infected;
    //public GameObject housePrefab_recovered;

    public GameObject hospitalPrefab;
    public GameObject schoolPrefab;

    public GameObject childPrefab;
    public GameObject adultPrefab;



    public List<GameObject> allAdults;
    public List<GameObject> allChildren;

    public List<GameObject> allHouseholds;
    public List<GameObject> allHospitals;
    public List<GameObject> allSchools;


    public GameObject hospital;
    public GameObject school;

    public GameObject[] spawnAreas;
    public GameObject[] landMasses;

    public GameObject[] hospitals;
    public GameObject[] schools;

    public GameObject raycasterObj;
    public int numberOfHouses = 100;
    public int radBetweenPeople = 10;
    public GameObject housePosPrefab;

    void Awake()
    {

        

        current = this;


        SEIR_implementation.current.population = numberOfHouses * landMasses.Length;

        allAdults = new List<GameObject>();
        allChildren = new List<GameObject>();


        allHouseholds = new List<GameObject>();
        allHospitals = new List<GameObject>();
        allSchools = new List<GameObject>();

        GameObject houseHolder = GameObject.Find("HousePositionMarkers");

        GameObject personHolder = GameObject.Find("PersonHolder");

        string currRegion = "";


        //Spawning positions

        GameObject prevLandMass = landMasses[0];
        for (int i = 0; i < spawnAreas.Length; i++)
        {
            currRegion = regionNumbers[i];

            prevLandMass.GetComponent<MeshCollider>().enabled = false;
            landMasses[i].GetComponent<MeshCollider>().enabled = true;

            Vector3 centerSpawn = spawnAreas[i].transform.position;
            Vector3 sizeSpawn = spawnAreas[i].transform.localScale;


            //Spawn schools

            string currSchoolAddress = currRegion + "_" + starterNumber[2] + "_" + "1";


            schools[i].GetComponent<School>().buildingAddress = currSchoolAddress;
            schools[i].name = "school_" + currSchoolAddress;

            schools[i].GetComponent<School>().maxCapacity = schoolMaxCapacity;
            schools[i].GetComponent<School>().buildingType = 2;
            allSchools.Add(schools[i]);


            //Spawn hospital

            string currHospitalAddress = currRegion + "_" + starterNumber[1] + "_" + "1";
            hospitals[i].GetComponent<Hospital>().buildingAddress = currHospitalAddress;
            hospitals[i].name = "hospital_" + currHospitalAddress;
            hospitals[i].GetComponent<Hospital>().buildingType = 1;
            hospitals[i].GetComponent<Hospital>().maxCapacity = hospitalMaxCapacity;

            allHospitals.Add(hospitals[i]);




            int houseCounter = 0;

            //spawn house positions
            while (houseCounter < numberOfHouses)
            {
                Vector3 spawnPos_caster = centerSpawn + new Vector3(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), 100, Random.Range(-sizeSpawn.z / 2, sizeSpawn.z / 2));

                raycasterObj.transform.position = spawnPos_caster;

                RaycastHit hit;
                int layerMask = 1 << 8;
                if (Physics.Raycast(raycasterObj.transform.position, -raycasterObj.transform.up, out hit, Mathf.Infinity, layerMask))
                {
                    int layerMaskHouses = 1 << 9;



                    Collider[] hitColliders = Physics.OverlapSphere(hit.point, radBetweenPeople, layerMaskHouses);
                    // This is because capital region is smaller
                    if (i == 3)
                    {
                        hitColliders = Physics.OverlapSphere(hit.point, radBetweenPeople/2, layerMaskHouses);
                    }
                    

                    

                    if (hitColliders.Length == 0)
                    {
                        Vector3 housePos = hit.point;

                        //GameObject currHousePosObj = Instantiate(housePosPrefab, housePos, Quaternion.identity);

                        //currHousePosObj.transform.parent = houseHolder.transform;
                        //currHousePosObj.tag = "housePos";


                        //spawn household
                        GameObject houseCurr = Instantiate(housePrefab_susceptible, housePos, housePrefab_susceptible.transform.rotation);

                        Vector3 houseRot = houseCurr.transform.rotation.eulerAngles;
                        houseRot.y += Random.Range(-80, 80);

                        houseCurr.transform.rotation = Quaternion.Euler(houseRot);


                        houseCurr.transform.parent = houseHolder.transform;
                        houseCurr.GetComponent<House>().maxCapacity = houseMaxCapacity;

                        string currFullAddress = currRegion + "_" + starterNumber[0] + "_" + houseCounter.ToString();
                        houseCurr.GetComponent<House>().buildingAddress = currFullAddress;

                        houseCurr.GetComponent<House>().houseState = 0;
                        houseCurr.name = currFullAddress;

                        houseCurr.GetComponent<House>().residentChildren = new List<GameObject>();
                        houseCurr.GetComponent<House>().residentAdults = new List<GameObject>();
                        houseCurr.GetComponent<House>().buildingType = 0;
                        allHouseholds.Add(houseCurr);





                        //spawn child
                        GameObject childCurr = Instantiate(childPrefab, housePos, Quaternion.identity);
                        childCurr.transform.parent = personHolder.transform;
                        allChildren.Add(childCurr);
                        childCurr.GetComponent<ChildS>().homeObject = houseCurr;

                        //Needs to be changed to the closest hospital
                        childCurr.GetComponent<ChildS>().hospitalObject = hospitals[i];

                        childCurr.GetComponent<ChildS>().currentState = 0;

                        childCurr.GetComponent<ChildS>().changeMaterial(childCurr.GetComponent<ChildS>().currentState);
                        childCurr.GetComponent<ChildS>().currentPlace = 0;
                        childCurr.GetComponent<ChildS>().houseAddress = currFullAddress;
                        childCurr.GetComponent<ChildS>().schoolAddress = currSchoolAddress;
                        childCurr.GetComponent<ChildS>().schoolObject = schools[i];
                        childCurr.GetComponent<ChildS>().age = 10;
                        childCurr.GetComponent<ChildS>().grade = Random.Range(1, 12);

                        houseCurr.GetComponent<House>().currCapacity += 1;
                        houseCurr.GetComponent<House>().residentChildren.Add(childCurr);

                        schools[i].GetComponent<School>().enrolledStudents.Add(childCurr);




                        houseCounter++;
                    }
                }
            }


            prevLandMass = landMasses[i];
        }



        

        ////Spawn schools

        //string currSchoolAddress = currRegion + "_" + starterNumber[2] + "_" + "1";


        //school.GetComponent<School>().buildingAddress = currSchoolAddress;
        //school.name = "school_" + currSchoolAddress;

        //school.GetComponent<School>().maxCapacity = schoolMaxCapacity;

        //allSchools.Add(school);


        ////Spawn hospital

        //string currHospitalAddress = currRegion + "_" + starterNumber[1] + "_" + "1";
        //hospital.GetComponent<Hospital>().buildingAddress = currHospitalAddress;
        //hospital.name = "hospital_" + currHospitalAddress;

        //hospital.GetComponent<Hospital>().maxCapacity = hospitalMaxCapacity;

        //allHospitals.Add(hospital);


        // Spawn houses

        

        //GameObject[] allHousePos = GameObject.FindGameObjectsWithTag("housePos");


        //int counter = 1;
        //foreach (GameObject currHousePos in allHousePos)
        //{

        //    //spawn household
        //    GameObject houseCurr = Instantiate(housePrefab_susceptible, currHousePos.transform.position, housePrefab_susceptible.transform.rotation);

        //    Vector3 houseRot = houseCurr.transform.rotation.eulerAngles;
        //    houseRot.y += Random.Range(-80, 80);

        //    houseCurr.transform.rotation = Quaternion.Euler(houseRot);


        //    houseCurr.transform.parent = houseHolder.transform;
        //    houseCurr.GetComponent<House>().maxCapacity = houseMaxCapacity;

        //    string currFullAddress = currRegion + "_" + starterNumber[0] + "_" + counter.ToString();
        //    houseCurr.GetComponent<House>().buildingAddress = currFullAddress;

        //    houseCurr.GetComponent<House>().houseState = 0;
        //    houseCurr.name = currFullAddress;

        //    houseCurr.GetComponent<House>().residentChildren = new List<GameObject>();
        //    houseCurr.GetComponent<House>().residentAdults = new List<GameObject>();
        //    allHouseholds.Add(houseCurr);


            


        //    //spawn child
        //    GameObject childCurr = Instantiate(childPrefab, currHousePos.transform.position, Quaternion.identity);
        //    childCurr.transform.parent = personHolder.transform;
        //    allChildren.Add(childCurr);
        //    childCurr.GetComponent<ChildS>().homeObject = houseCurr;

        //    //Needs to be changed to the closest hospital
        //    childCurr.GetComponent<ChildS>().hospitalObject = hospital;

        //    childCurr.GetComponent<ChildS>().currentState = 0;

        //    childCurr.GetComponent<ChildS>().changeMaterial(childCurr.GetComponent<ChildS>().currentState);
        //    childCurr.GetComponent<ChildS>().currentPlace = 0;
        //    childCurr.GetComponent<ChildS>().houseAddress = currFullAddress;
        //    childCurr.GetComponent<ChildS>().schoolAddress = currSchoolAddress;
        //    childCurr.GetComponent<ChildS>().schoolObject = school;
        //    childCurr.GetComponent<ChildS>().age = 10;
        //    childCurr.GetComponent<ChildS>().grade = Random.Range(1, 12);

        //    houseCurr.GetComponent<House>().currCapacity += 1;
        //    houseCurr.GetComponent<House>().residentChildren.Add(childCurr);

        //    school.GetComponent<School>().enrolledStudents.Add(childCurr);




        //    //initialPos.Add(childCurr.transform.position);

        //    //allPeople_states.Add(0);

        //    //spawn adult

        //    //GameObject adultCurr = Instantiate(adultPrefab, currHousePos.transform.position, Quaternion.identity);
        //    //adultCurr.transform.parent = personHolder.transform;
        //    //allAdults.Add(adultCurr);
        //    //adultCurr.GetComponent<AdultS>().homeObject = houseCurr;

        //    //adultCurr.GetComponent<AdultS>().currentState = 0;
        //    //adultCurr.GetComponent<AdultS>().changeMaterial(adultCurr.GetComponent<AdultS>().currentState);
        //    //adultCurr.GetComponent<AdultS>().currentPlace = 0;
        //    //adultCurr.GetComponent<AdultS>().houseAddress = currFullAddress;
        //    //adultCurr.GetComponent<AdultS>().workAddress = currSchoolAddress; // currently adults go to school we need work building
        //    //adultCurr.GetComponent<AdultS>().workObject = school;
        //    //adultCurr.GetComponent<AdultS>().age = 35;

        //    //houseCurr.GetComponent<House>().currCapacity += 1;
        //    //houseCurr.GetComponent<House>().residentAdults.Add(adultCurr);




        //    counter++;
        //}

        //foreach (GameObject currHousePos in allHousePos)
        //{
        //    Destroy(currHousePos);
        //}
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject houseHolder = GameObject.Find("HousePositionMarkers");


        ////Spawning positions

        //for (int i = 0; i < spawnAreas.Length; i++)
        //{
        //    Vector3 centerSpawn = spawnAreas[i].transform.position;
        //    Vector3 sizeSpawn = spawnAreas[i].transform.lossyScale;

            

        //    int houseCounter = 0;

        //    Vector3 spawnPos_caster = centerSpawn + new Vector3(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), 100, Random.Range(-sizeSpawn.z / 2, sizeSpawn.z / 2));

        //    raycasterObj.transform.position = spawnPos_caster;

        //    Debug.DrawRay(raycasterObj.transform.position, -raycasterObj.transform.up * 1000);

        //    RaycastHit hit;
        //    int layerMask = 1 << 8;

        //    if (Physics.Raycast(raycasterObj.transform.position, -raycasterObj.transform.up, out hit, Mathf.Infinity, layerMask))
        //    {
        //        Debug.Log("HERE" + hit.point.ToString());
        //    }

        //    //while (houseCounter < numberOfHouses)
        //    //{
        //    //    Vector3 spawnPos_caster = centerSpawn + new Vector3(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), 100, Random.Range(-sizeSpawn.z / 2, sizeSpawn.z / 2));

        //        //    raycasterObj.transform.position = spawnPos_caster;

        //        //    RaycastHit hit;
        //        //    int layerMask = 1 << 8;
        //        //    if (Physics.Raycast(raycasterObj.transform.position, -raycasterObj.transform.up, out hit, Mathf.Infinity, layerMask))
        //        //    {
        //        //        int layerMaskHouses = 1 << 9;
        //        //        Collider[] hitColliders = Physics.OverlapSphere(hit.point, radBetweenPeople, layerMaskHouses);

        //        //        if (hitColliders.Length == 0)
        //        //        {
        //        //            Vector3 housePos = hit.point;

        //        //            GameObject currHousePosObj = Instantiate(housePosPrefab, housePos, Quaternion.identity);

        //        //            currHousePosObj.transform.parent = houseHolder.transform;
        //        //            currHousePosObj.tag = "housePos";

        //        //            houseCounter++;
        //        //        }
        //        //    }
        //        //}
        //}




    }
}

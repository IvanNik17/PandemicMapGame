using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFamily : MonoBehaviour
{
    public int houseMaxCapacity = 4;
    public int schoolMaxCapacity = 100;
    public int hospitalMaxCapacity = 50;


    public GameObject housePrefab_susceptible;
    public GameObject housePrefab_infected;
    public GameObject housePrefab_recovered;

    public GameObject hospitalPrefab;
    public GameObject schoolPrefab;

    public GameObject childPrefab;
    public GameObject adultPrefab;



    public List<GameObject> allAdults;
    public List<GameObject> allChildren;

    public List<GameObject> allHouseholds;
    public List<GameObject> allHospitals;
    public List<GameObject> allSchools;

    //public List<int> allPeople_states; // 0 - susceptible, 1 - infected unknown, 2 - infected known, 3 - recovered


    //public List<Vector3> initialPos;

    //public GameObject personPrefab_susceptible;
    //public GameObject personPrefab_infectedUnknown;
    //public GameObject personPrefab_infectedKnown;
    //public GameObject personPrefab_recovered;

    //public GameObject housePrefab;


    //public List<Vector3> schoolPos_perPerson;
    //public List<Vector3> hospitalPos_perPerson;

    public GameObject hospital;
    public GameObject school;

    // Start is called before the first frame update
    void Awake()
    {

        allAdults = new List<GameObject>();
        allChildren = new List<GameObject>();


        allHouseholds = new List<GameObject>();
        allHospitals = new List<GameObject>();
        allSchools = new List<GameObject>();


        //initialPos = new List<Vector3>();

        //schoolPos_perPerson = new List<Vector3>();
        //hospitalPos_perPerson = new List<Vector3>();

        //allPeople_states = new List<int>();

        string currRegion = SetupResources.current.regionNumbers[0];

        //Spawn schools

        string currSchoolAddress = currRegion + "_" + SetupResources.current.starterNumber[2] + "_" + "1";

        
        school.GetComponent<School>().buildingAddress = currSchoolAddress;
        school.name = "school_" + currSchoolAddress;

        school.GetComponent<School>().maxCapacity = schoolMaxCapacity;

        allSchools.Add(school);


        //Spawn hospital

        string currHospitalAddress = currRegion + "_" + SetupResources.current.starterNumber[1] + "_" + "1";
        hospital.GetComponent<Hospital>().buildingAddress = currHospitalAddress;
        hospital.name = "hospital_" + currHospitalAddress;

        hospital.GetComponent<Hospital>().maxCapacity = hospitalMaxCapacity;

        allHospitals.Add(hospital);


        // Spawn houses

        GameObject houseHolder = GameObject.Find("HousePositionMarkers");

        GameObject[] allHousePos = GameObject.FindGameObjectsWithTag("housePos");


        int counter = 1;
        foreach (GameObject currHousePos in allHousePos)
        {
            
            //spawn household
            GameObject houseCurr = Instantiate(housePrefab_susceptible, currHousePos.transform.position, housePrefab_susceptible.transform.rotation );

            Vector3 houseRot = houseCurr.transform.rotation.eulerAngles;
            houseRot.y += Random.Range(-80, 80);

            houseCurr.transform.rotation = Quaternion.Euler(houseRot);


            houseCurr.transform.parent = houseHolder.transform;
            houseCurr.GetComponent<House>().maxCapacity = houseMaxCapacity;

            string currFullAddress = currRegion + "_" + SetupResources.current.starterNumber[0] + "_" + counter.ToString();
            houseCurr.GetComponent<House>().buildingAddress = currFullAddress;

            houseCurr.GetComponent<House>().houseState = 0;
            houseCurr.name = currFullAddress;

            houseCurr.GetComponent<House>().residentChildren = new List<GameObject>();
            houseCurr.GetComponent<House>().residentAdults = new List<GameObject>();
            allHouseholds.Add(houseCurr);




            //spawn child
            GameObject childCurr = Instantiate(childPrefab, currHousePos.transform.position, Quaternion.identity);
            childCurr.transform.parent = houseCurr.transform;
            allChildren.Add(childCurr);
            childCurr.GetComponent<ChildS>().homeObject = houseCurr;

            childCurr.GetComponent<ChildS>().currentState = 0;
            
            childCurr.GetComponent<ChildS>().changeMaterial(childCurr.GetComponent<ChildS>().currentState);
            childCurr.GetComponent<ChildS>().currentPlace = 0;
            childCurr.GetComponent<ChildS>().houseAddress = currFullAddress;
            childCurr.GetComponent<ChildS>().schoolAddress = currSchoolAddress;
            childCurr.GetComponent<ChildS>().schoolObject = school;
            childCurr.GetComponent<ChildS>().age = 10;

            houseCurr.GetComponent<House>().currCapacity += 1;
            houseCurr.GetComponent<House>().residentChildren.Add(childCurr);

            school.GetComponent<School>().enrolledStudents.Add(childCurr);

            


            //initialPos.Add(childCurr.transform.position);

            //allPeople_states.Add(0);

            //spawn adult

            GameObject adultCurr = Instantiate(adultPrefab, currHousePos.transform.position, Quaternion.identity);
            adultCurr.transform.parent = houseCurr.transform;
            allAdults.Add(adultCurr);
            adultCurr.GetComponent<AdultS>().homeObject = houseCurr;

            adultCurr.GetComponent<AdultS>().currentState = 0;
            adultCurr.GetComponent<AdultS>().changeMaterial(adultCurr.GetComponent<AdultS>().currentState);
            adultCurr.GetComponent<AdultS>().currentPlace = 0;
            adultCurr.GetComponent<AdultS>().houseAddress = currFullAddress;
            adultCurr.GetComponent<AdultS>().workAddress = currSchoolAddress; // currently adults go to school we need work building
            adultCurr.GetComponent<AdultS>().workObject = school;
            adultCurr.GetComponent<AdultS>().age = 35;

            houseCurr.GetComponent<House>().currCapacity += 1;
            houseCurr.GetComponent<House>().residentAdults.Add(adultCurr);
            

            // currently just add the same position over and over
            //schoolPos_perPerson.Add(school.transform.position);
            //hospitalPos_perPerson.Add(hospital.transform.position);

            counter++;
        }

        foreach (GameObject currHousePos in allHousePos)
        {
            Destroy(currHousePos);
        }




        //foreach (Transform childHouse in houseHolder.transform)
        //{
        //    GameObject currChild = Instantiate(childObj, childHouse.position, Quaternion.identity);

        //    allHouseholds.Add(childHouse.gameObject);
        //    allChildren.Add(currChild);

        //    initialPos.Add(currChild.transform.position);


        //    // currently just add the same position over and over
        //    schoolPos_perPerson.Add(school.transform.position);
        //    hospitalPos_perPerson.Add(hospital.transform.position);

        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

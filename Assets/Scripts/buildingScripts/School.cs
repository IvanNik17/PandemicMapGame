using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : Building
{
    // Start is called before the first frame update

    public List<GameObject> enrolledStudents;
    void Start()
    {

        enrolledStudents = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

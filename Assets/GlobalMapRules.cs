using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapRules : MonoBehaviour
{

    public static GlobalMapRules current;

    public int uptoWhichClassIsOpen =12;

    public List<bool> whichClassesAreOpen;


    void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalEvents.current.onChangeSchoolClass += changeClassRule;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void changeClassRule(int schoolClasses)
    //{
    //    uptoWhichClassIsOpen = schoolClasses;
    //}

    void changeClassRule(List<int> schoolClasses, bool isOpen)
    {
        

        for (int i = 0; i < schoolClasses.Count; i++)
        {
            whichClassesAreOpen[schoolClasses[i] - 1] = isOpen;
        }
    }

}

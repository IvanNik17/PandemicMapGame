using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = 1 << 9;
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.parent.GetComponent<Hospital>() != null)
                {
                    Debug.Log("YEEY HOSPITAL");
                }
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }


    }
}

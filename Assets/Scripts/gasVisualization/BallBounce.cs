using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    //Rigidbody _rb;
    //void Start()
    //{
    //    _rb = GetComponent<Rigidbody>();
    //    // Increase max angular velocity or we won't see much spin.
    //    _rb.maxAngularVelocity = 1000;
    //    StartCoroutine(ChangeRotation());
    //}
    //private IEnumerator ChangeRotation()
    //{
    //    while (true)
    //    {
    //        _rb.AddTorque(new Vector3(10 * UnityEngine.Random.Range(0, 3f), UnityEngine.Random.Range(0, 3f), UnityEngine.Random.Range(0, 3f)), ForceMode.VelocityChange);
    //        yield return new WaitForSeconds(1);
    //    }
    //}

    Rigidbody _rb;
    Vector3 blindMovement_RandomDirection;

    public float movementSpeed = 200f;
    public float maxVelocity = 5f;
    public float minVelocity = 2f;

    public float targetSickTime = 10.0f;

    float sqrMaxVel;
    float sqrMinVel;

    float blindMovement_RandomSpeed;

    public int currState = -1;

    public Material[] possibleStatesMat;

    //bool startInfectTimer = false;

    float currTimer = 0f;

    GameObject managerObj;

    public bool isMoving = true;

    Vector3 currVelocity;

    void Start()
    {
        sqrMaxVel = maxVelocity * maxVelocity;
        sqrMinVel = minVelocity * minVelocity;
        _rb = GetComponent<Rigidbody>();
        blindMovement_RandomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

        managerObj = GameObject.Find("Manager");
        //blindMovement_RandomSpeed = Random.Range(50f, 150f);

        //Debug.Log(blindMovement_RandomSpeed);
        //Debug.Log(blindMovement_RandomDirection);
        //(blindMovement_RandomDirection / (float)blindMovement_RandomSpeed)
    }

    void FixedUpdate()
    {
        //_rb.AddForce(blindMovement_RandomDirection * movementSpeed);

        if (!managerObj.transform.GetComponent<SimControls>().pauseSim && !managerObj.transform.GetComponent<SimControls>().endSim)
        {

            if (isMoving)
            {

                if (_rb.isKinematic == true)
                {
                    _rb.isKinematic = false;

                    if (_rb.velocity == Vector3.zero)
                    {
                        _rb.velocity= new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)) * movementSpeed;
                    }
                    _rb.velocity = currVelocity;
                }
                 


                if (_rb.velocity.sqrMagnitude > sqrMaxVel)
                {
                    _rb.velocity = _rb.velocity.normalized * maxVelocity;
                }
                else if (_rb.velocity.sqrMagnitude != 0 && _rb.velocity.sqrMagnitude < sqrMinVel)
                {
                    _rb.velocity = _rb.velocity.normalized * minVelocity;
                }
                else
                {
                    _rb.velocity = blindMovement_RandomDirection * movementSpeed;
                }
            }
            else
            {
                _rb.isKinematic = true;
                //_rb.velocity = Vector3.zero;
            }

            currVelocity = _rb.velocity;



            if (currState == 2)
            {
                currTimer += Time.fixedDeltaTime;

                if (currTimer >= targetSickTime)
                {

                    currState = 3;
                    currTimer = 0f;

                    whichColor(3);

                    managerObj.transform.GetComponent<InfectionCounter>().numHealed += 1f;
                    managerObj.transform.GetComponent<InfectionCounter>().numInfected -= 1f;
                }
            }


        }
        else
        {
            _rb.isKinematic = true;
        }


        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.x);


        //Debug.Log(_rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag != "floor")
        {
            //Debug.Log(blindMovement_RandomDirection);
            Vector3 normal = collision.contacts[0].normal;
            blindMovement_RandomDirection = Vector3.Reflect(blindMovement_RandomDirection + RandomVector(-0.1f,0.1f), normal);

            if (collision.transform.tag == "actor")
            {
                int collisionState = collision.transform.GetComponent<BallBounce>().currState;

                if (collisionState == 2 && currState == 1)
                {
                    if (Random.value <= managerObj.transform.GetComponent<SimControls>().R_0_percent)
                    {
                        currState = 2;
                        whichColor(2);

                        managerObj.transform.GetComponent<InfectionCounter>().numHealthy -= 1f;
                        managerObj.transform.GetComponent<InfectionCounter>().numInfected += 1f;
                    }

                }
            }
        }
    }

    private void whichColor(int state)
    {
        if (state == 1)
        {
            transform.GetComponent<Renderer>().material = possibleStatesMat[0];
        }
        else if (state == 2)
        {
            transform.GetComponent<Renderer>().material = possibleStatesMat[1];
        }
        else if(state == 3)
        {
            transform.GetComponent<Renderer>().material = possibleStatesMat[2];
        }
    }

    private Vector3 RandomVector(float min, float max)
    {
        float x = Random.Range(min, max);
        float z = Random.Range(min, max);
        return new Vector3(x, 0, z);
    }


}

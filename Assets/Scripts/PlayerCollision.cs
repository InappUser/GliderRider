using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    private Run run;

    private bool startTargetRemainTimer = false; //make a struct for bool, timer, timer?
    private float targetRemainTimer=0;
    private float targetRemainDuration = .1f;

    private bool startReducePhysSim = false;
    private float reducePhysSimTimer = 0;

    private string lastTargetCollided;

    private bool land = false;
    private float landTimer = 0;

    void Start()
    {
        run = GameObject.FindObjectOfType<Run>();
    }
    void OnCollisionEnter(Collision col)
    {
        land = true;
        if (col.transform.tag == "Target")
        {

            if (run.GetIsRunning()) //so that colliding with other parts of the target afterwards don't reset the score
            {
                lastTargetCollided = col.transform.name;
                startTargetRemainTimer = true;
            }
            //start timer to make sure player stayed on target
            //if they did, send info to run (start next run)
        }
        startReducePhysSim = true;
    }
    void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Target")
        {
            //startTargetRemainTimer = false;
            //targetRemainTimer = 0; //reset timer when they leave
        }
    }
    void OnTriggerEnter(Collider col)
    {
        print("hit trigger");
        GetComponent<PlaneControls>().land();
        GetComponent<Rigidbody>().isKinematic = true;

        if (col.transform.tag == "Target")
        {

            if (run.GetIsRunning()) //so that colliding with other parts of the target afterwards don't reset the score
            {
                lastTargetCollided = col.transform.name;
                startTargetRemainTimer = true;
            }
            //start timer to make sure player stayed on target
            //if they did, send info to run (start next run)
        }

        if (col.name == "Fuel")
        {
            //add points to run
            run.AddFuelCannister();
            //dissapear fuel
            col.gameObject.SetActive(false);
            //lerp speed 
          //  GetComponent<PlaneControls>().BoostSpeed = true; // Commented out during refactoring to add landing target
        }
    }
    void FixedUpdate()
    {
        if (land) {
            landTimer += Time.deltaTime;
            if (landTimer < .1f)
            {
                GetComponent<PlaneControls>().land();
            }
            else {
                landTimer = 0;
                land = false;
            }
        }
    }

    void Update() {
        if (startTargetRemainTimer)
        {
            targetRemainTimer += Time.deltaTime;
            if (targetRemainTimer > targetRemainDuration)
            {
                print("WIN");
                startTargetRemainTimer = false;
                StartCoroutine(run.EndRun(lastTargetCollided)); // a cproutine so that a delay can be easily implemented
            }
        }

        if (startReducePhysSim)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.drag = reducePhysSimTimer;
            rb.angularDrag= reducePhysSimTimer;
            //
            reducePhysSimTimer += Time.deltaTime;
            if (reducePhysSimTimer > .5f) {
                rb.isKinematic = true; //truely resetting the physics
               // rb.isKinematic = false;
                //print("resetting physics");
                rb.drag = 0;
                rb.angularDrag = 0;
                startReducePhysSim = false;
            }
            //Rigidbody rb = GetComponent<Rigidbody>();
            //rb.velocity = Vector3.Lerp(rb.velocity, -rb.velocity, 10 * Time.deltaTime);
            //if (rb.velocity.magnitude <= 0)
            //{
            //    rb.velocity = Vector3.zero;
            //    print("end");
            //    startReducePhysSim = false;
            //}
        }
    }
}

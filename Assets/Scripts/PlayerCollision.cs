using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    private Run run;

    private bool startTargetRemainTimer = false;
    private float targetRemainTimer=0;
    private float targetRemainDuration = .1f;
    void Start()
    {
        run = GameObject.FindObjectOfType<Run>();
    }
    void OnCollisionEnter(Collision col)
    {

        if (col.transform.tag == "Target")
        {
            print("colliding");
            startTargetRemainTimer = true;
            //start timer to make sure player stayed on target
            //if they did, send info to run (start next run)

        }
        else {
            //if any other collider, crash depending on velocity
        }
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
        if (col.name == "Fuel")
        {
            //add points to run
            run.AddFuelCannister();
            //dissapear fuel
            col.gameObject.SetActive(false);
            //lerp speed - maybe use 
            GetComponent<PlaneControls>().BoostSpeed = true;
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
                run.ResetRun();
            }
        }
    }
}

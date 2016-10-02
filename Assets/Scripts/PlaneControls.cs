using UnityEngine;
using System.Collections;

public class PlaneControls : MonoBehaviour
{

    public float waitBeforeGlide = 3f;
    public float cameraSpring = 0.96f;
    public float baseSpeed = 6f;
    public float cameraDistance = 15.0f;
    public float cameraHeight = 5.0f;
    public float momentum = 30.0f;

 
    private float currentSpeed; //used so that speed can be altered with boost
    private bool boostSpeed = false;
    private bool beenBoosted = false;
    private float speedBoostDuration = 3;
    private float speedBoostTimer = 0;
    private float boostedSpeed = 50;

    //float speedFrom, speedTo;

    float lerpTime = 1f;
    float currentLerpTime;

    public bool BoostSpeed{set{ boostSpeed = value;}}

    // Use this for initialization
    void Start()
    {
        Reset();
    }
    public void Reset()
    {
        Debug.Log("plane controls script added successfully");
        Cursor.visible = false;

        currentSpeed = baseSpeed;
        //print("speed: " + baseSpeed);
        speedBoostTimer = 0;
        beenBoosted = false;
        boostSpeed = false;
    }

    // want to do in fixedUpdate?
    void Update()
    {
        if (boostSpeed)
        {
          SpeedBoost();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        SimulateGilding();
       
    }
    void SimulateGilding()
    {
        

        Vector3 camChaser = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
        Camera.main.transform.position = Camera.main.transform.position * cameraSpring + camChaser * (1.0f - cameraSpring);
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);



        float yaw = Input.GetAxis("Horizontal") / 2;
        float tilt = Input.GetAxis("Vertical") / 2;
        float roll = Input.GetAxis("Roll") / 2;

        // speed
        transform.position += transform.forward * Time.deltaTime * currentSpeed;
        currentSpeed -= transform.forward.y * Time.deltaTime * momentum;
        //print("current:" + currentSpeed);
        //if (currentSpeed < .5f)
        //{
        //    currentSpeed = .5f;
        //}

        // plane rotation
        transform.Rotate(tilt, yaw, roll);



        // collision with terrain
        float currentHeight = Terrain.activeTerrain.SampleHeight(transform.position);
        if (currentHeight > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }
    }

    void SpeedBoost()
    {
        //boost - interpolate between norm speed and boost
        if (beenBoosted == false)
        {
            if (currentSpeed < boostedSpeed)
            {
                currentSpeed = BoostLerpInterp(baseSpeed, boostedSpeed+.1f); //make it aim for a little higher, otherwise it sometimes never reaches boostSpeed
            }
            else
            {
                currentLerpTime = 0;
                beenBoosted = true;
            }
        }
        //start timer
        speedBoostTimer += Time.deltaTime;
        //unboost 
        if (speedBoostTimer > speedBoostDuration) { //slowing down once timer has hit duration
            print("cur:" + currentSpeed + " speed:" + baseSpeed + " beenb "+beenBoosted +""+currentLerpTime);
            if (currentSpeed > baseSpeed)
            {
                currentSpeed = BoostLerpInterp(boostedSpeed, baseSpeed - .1f);
            }
            else
            {
                speedBoostTimer = 0;
                currentLerpTime = 0;
                beenBoosted = false;
                boostSpeed = false;
            }
        }
        
        //if (beenBoosted == false && currentSpeed == speed) { // if before the waitForSeconds and before current speed begins lerping
        //    speedFrom = speed;
        //    speedTo = boostedSpeed;
        //    print("setting init");
        //}

        ////change speed
        //if (currentSpeed != speedTo)
        //{
        //    print("interpolating");
        //    currentSpeed = BoostLerpInterp(speedFrom, speedTo);
        //} else {
        //    currentLerpTime = 0; //reset lerp timer for next use if the current speed has reached the end of the lerp
        //    if (beenBoosted && speed == currentSpeed) { // condition for ending the function
        //        print("end functioN");
        //        beenBoosted = false; //resetting for next function use
        //        boostSpeed = false;
        //        //yield return null;
        //    }
        //}

        ////wait for speed duration
        ////yield return new WaitForSeconds(speedBoostDuration);
        ////hit trigger to decrease speed
        //if (beenBoosted == false)
        //{
        //    speedFrom = boostedSpeed; //swap which speed is to be lerped from and to
        //    speedTo = speed;
        //}
        //beenBoosted = true; //change the state of the function
        //print("hit");

    }


    float BoostLerpInterp( float from, float to)
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float t = currentLerpTime / lerpTime; //providing percentage
        t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        t = t * t;
        return  Mathf.Lerp(from, to, t);
        
    }
}
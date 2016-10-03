using UnityEngine;
using System.Collections;

public class PlaneControls: MonoBehaviour
{

    public float cameraSpring = 0.9f;
    public float baseSpeed = 20f;
    public float airResistance = 1f;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;
    public float cameraForesight = 10f;
    public float momentum = 10f;
    public float zoomOut = 0;

    private float yaw;
    private float tilt;
    private float roll;
    private bool landed;
    private Vector3 camChaser;

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


       
    }
    void FixedUpdate()
    {
        SimulateGilding();
    }
    void SimulateGilding()
    {

        // collision with terrain
        float currentHeight = Terrain.activeTerrain.SampleHeight(transform.position);
        if (currentHeight > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            landed = true;
            land();
        }

        // camera tracking (if low to ground angle higher)
        if (currentHeight > transform.position.y - 20)
        {
            zoomOut = 10 - (transform.position.y - currentHeight);
            camChaser = transform.position - transform.forward * cameraDistance + Vector3.up * (cameraHeight + zoomOut);
            Camera.main.transform.position = Camera.main.transform.position * cameraSpring + camChaser * (1.0f - cameraSpring);
            Camera.main.transform.LookAt(transform.position + transform.forward * cameraForesight);
        }
        else
        {
            camChaser = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
            Camera.main.transform.position = Camera.main.transform.position * cameraSpring + camChaser * (1.0f - cameraSpring);
            Camera.main.transform.LookAt(transform.position + transform.forward * cameraForesight);
        }

        // turning controls
        if (landed == false)
        {
            yaw = Input.GetAxis("Horizontal") / 2;
            tilt = Input.GetAxis("Vertical") / 2;
            roll = Input.GetAxis("Roll") / 2;
        }
        else
        {
            yaw = 0;
            tilt = 0;
            roll = 0;
        }

        transform.Rotate(tilt, yaw, roll);


        // currentSpeed
        transform.position += transform.forward * Time.deltaTime * currentSpeed;
        currentSpeed -= transform.forward.y * Time.deltaTime * momentum;

        // don't allow backward movement
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        // gradually cause desent of plane 
        airResistance += 0.0001f;
        currentSpeed -= 0.8f * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * -airResistance;

        
    }

    public static void Land()
    { }
        public void land()
    {

        if (currentSpeed >= 10 || currentSpeed <= 30)
        {
            // land
            currentSpeed -= 25 * Time.deltaTime;
            airResistance = 0;
            cameraDistance = 30.0f;
            cameraHeight = 30.0f;
            momentum = 0.0f;
        }
        else
        {
            // crash
            Debug.Log("CRASH LANDING");
            currentSpeed = 0f;
            airResistance = 0f;
            cameraDistance = 30.0f;
            cameraHeight = 30.0f;
            momentum = 0.0f;
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
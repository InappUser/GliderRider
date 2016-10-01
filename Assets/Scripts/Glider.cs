using UnityEngine;
using System.Collections;

public class Glider : MonoBehaviour {
    public GameObject gliderCamera;


    Vector3 initCameraDistance;
    Vector3 gliderStart;
    float movementSpeedRaw = 5f;
    float move = 5f;
    float startAccelerationSpeed = 1;
    float currentAccelerationSpeed = 1;
    float accelerationTimeInterpolation = 0.0f;
    float accelerationTIModifier = 0.3f;

    bool slowed = false;
    float vertRotSpeed = 20f;
    float horiRotSpeed = 40f;

    float flightDurEnegy = 1000f;


    float rollToYawMultiplier = .2f; 

    float recentreSpeed = .2f;
    Rigidbody myRigidbody;



	// Use this for initialization
	void Start () {
        myRigidbody = transform.GetComponent<Rigidbody>();
        myRigidbody.velocity = transform.forward * 10;
        //CentredRotation = Quaternion.identity
        initCameraDistance = this.transform.position - gliderCamera.transform.position;
        gliderStart = transform.forward;
    }
	// Update is called once per frame
	void FixedUpdate () {

        FlightControl();



    }

    void FlightControl()
    {
        // CameraMovement();
        //print(transform.rotation);
        if (Input.GetKey(KeyCode.Space))
        {
            Recentre();
        }
        Quaternion rotToAdd = Quaternion.identity; //resetting physics frame's rotation

        float pitch;
        float roll;
        float yaw;

        pitch = Input.GetAxisRaw("Vertical") * (Time.deltaTime * vertRotSpeed);
        yaw = 0;//(Input.GetAxisRaw("Horizontal") * (Time.deltaTime * horiRotSpeed));// * rollToYawMultiplier;
        //useGUILayout transform right + vector3 up - 1.414214
        roll = -(Input.GetAxisRaw("Horizontal") * (Time.deltaTime * horiRotSpeed)); //updating the roll and pitch rotaiton (y and x rotaiton, respectively)

        rotToAdd.eulerAngles = Vector3.Lerp(rotToAdd.eulerAngles, new Vector3(pitch * vertRotSpeed, yaw * horiRotSpeed, roll * horiRotSpeed), 10 * Time.deltaTime);
        // print("raw euler"+rotToAdd.eulerAngles);
        myRigidbody.rotation *= rotToAdd;

        transform.Rotate(rotToAdd.eulerAngles);
        //turn vertical velocity into horizontal
        //myRigidbody.velocity -= Vector3.up * Time.deltaTime; //gravity
        myRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration); //providing gravity to the rigid body
        //ensuring that only negative values are applied to the y of vertVel
        Vector3 vertVel = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y < 0 ? myRigidbody.velocity.y : 0, myRigidbody.velocity.z);//myRigidbody.velocity - Vector3.Exclude(transform.up, myRigidbody.velocity);

        float upVel = Vector3.Distance(transform.forward, Vector3.up);
        float upVelClamped = Mathf.Clamp(((upVel - 1.4f) * 70), -5, 5);
        print("up " + upVelClamped + "vert vel" + vertVel + " vert vel magnitude: " + vertVel.magnitude);
        print("rigid vel mag:" + myRigidbody.velocity.magnitude + " vertVelMag: " + vertVel.magnitude + " multiplied: " + new Vector3(vertVel.x, vertVel.y -= upVelClamped, vertVel.z));

        vertVel = new Vector3(vertVel.x, vertVel.y -= upVelClamped, vertVel.z);// * Time.deltaTime;

        // print("vertvel" + vertVel);
        //print("rigidbodyvel: " + myRigidbody.velocity + " distance from up: " + Vector3.Distance(transform.forward, Vector3.up)/2);
        myRigidbody.velocity -= vertVel * Time.deltaTime;

        myRigidbody.velocity += (vertVel.magnitude) * transform.forward * Time.deltaTime;
        // myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y * upVel, myRigidbody.velocity.z);
        //print("vel" + myRigidbody.velocity);
        // myRigidbody.velocity *= upVel;
        // myRigidbody.velocity += new Vector3(0,-(Vector3.Distance(transform.forward, Vector3.up) -1),0);
        //myRigidbody.velocity = posToAdd * (Time.deltaTime * movementSpeed);
        //  transform.forward = myRigidbody.velocity.normalized;
        //print("movement * gravity: " + (Physics.gravity * 1000) / movementSpeed);

    }

    void Recentre()
    {
        Quaternion zeroPitchRoll = Quaternion.Euler(0, transform.rotation.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, recentreSpeed);

    }

    void CameraMovement() {
        gliderCamera.transform.position = this.transform.position - initCameraDistance; //- gliderCamera.transform.position; 
        //get difference between cam pos and glider pos
        //make current cam pos glider pos - difference
    }
}

using UnityEngine;
using System.Collections;

public class Glider : MonoBehaviour {
    public GameObject gliderCamera;


    Vector3 initCameraDistance;
    Vector3 gliderStart;
    float movementSpeedRaw = 5000f;
    float startAccelerationSpeed = 1;
    float currentAccelerationSpeed = 1;
    float accelerationTimeInterpolation = 0.0f;
    float accelerationTIModifier = 0.3f;

    bool slowed = false;
    float vertRotSpeed = 20f;
    float horiRotSpeed = 40f;


    float rollToYawMultiplier = .2f; 

    float recentreSpeed = .2f;
    Rigidbody myRigidbody;



	// Use this for initialization
	void Start () {
        myRigidbody = transform.GetComponent<Rigidbody>();
        //CentredRotation = Quaternion.identity
        initCameraDistance = this.transform.position - gliderCamera.transform.position;
        gliderStart = transform.forward;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

       
       // CameraMovement();
        //print(transform.rotation);
        if (Input.GetKey(KeyCode.Space)) {
            Recentre();
            //print("keydown");
        }
        Quaternion rotToAdd = Quaternion.identity; //resetting physics frame's rotation

        float pitch;
        float roll;
        float yaw;

        pitch = Input.GetAxisRaw("Vertical") * (Time.deltaTime * vertRotSpeed);
        yaw = 0;//(Input.GetAxisRaw("Horizontal") * (Time.deltaTime * horiRotSpeed));// * rollToYawMultiplier;
        roll = -(Input.GetAxisRaw("Horizontal") * (Time.deltaTime * horiRotSpeed)); //updating the roll and pitch rotaiton (y and x rotaiton, respectively)

        rotToAdd.eulerAngles = Vector3.Lerp(rotToAdd.eulerAngles, new Vector3(pitch * 20, yaw * 20, roll * 20), 10 * Time.deltaTime);
        myRigidbody.rotation *= rotToAdd;
        //print("rigidbody rotation: " + myRigidbody.rotation.eulerAngles + " quaternion: "+ myRigidbody.rotation);
        //if (myRigidbody.rotation.eulerAngles.x > 270 && myRigidbody.rotation.eulerAngles.x > 330)
        print(Vector3.Distance(-transform.up, gliderStart));
        if (Vector3.Distance(-transform.up, gliderStart) < 1.2f)
        {

            currentAccelerationSpeed = Mathf.Lerp(currentAccelerationSpeed, startAccelerationSpeed - 0.9f, accelerationTimeInterpolation);
            accelerationTimeInterpolation += accelerationTIModifier * Time.deltaTime;
            // print("cur speed" + currentAccelerationSpeed);
            ; if (currentAccelerationSpeed < startAccelerationSpeed - .8f)
            {
                slowed = true;
                accelerationTimeInterpolation = 0.1f;
            }
            // print("slowing: "+slowed);
        }
        else if (slowed)
        {
            currentAccelerationSpeed = Mathf.Lerp(currentAccelerationSpeed, startAccelerationSpeed - 0.2f, accelerationTimeInterpolation);
            accelerationTimeInterpolation += accelerationTIModifier * Time.deltaTime;

            if (currentAccelerationSpeed > 1) slowed = false;
        }
        //  print("cur speed" + currentAccelerationSpeed);
        startAccelerationSpeed = Mathf.Clamp(startAccelerationSpeed, 0.1f, 1.9f);



        float movementSpeed = movementSpeedRaw * currentAccelerationSpeed;
        //print("glider start: " + gliderStart);
        Vector3 posToAdd = gliderStart;// + new Vector3(0,-.1f,0);
        posToAdd = myRigidbody.rotation * posToAdd;
        myRigidbody.velocity = posToAdd * (Time.deltaTime * movementSpeed);
        //  transform.forward = myRigidbody.velocity.normalized;
        print("movement * gravity: " + (Physics.gravity * 1000) / movementSpeed);
        myRigidbody.AddForce((Physics.gravity * 1000) / movementSpeed, ForceMode.Acceleration); //providing gravity to the rigid body

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

using UnityEngine;
using System.Collections;

public class Glider : MonoBehaviour {
    public GameObject gliderCamera;


    Vector3 initCameraDistance;
    float movementSpeed = 1000f;
    float startAccelerationSpeed = 1;
    float currentAccelerationSpeed = 1;
    float vertRotSpeed = 20f;
    float horiRotSpeed = 40f;


    float rollToYawMultiplier = .2f; 

    float recentreSpeed = .2f;
    //Quaternion CentredRotation;
    Rigidbody myRigidbody;



	// Use this for initialization
	void Start () {
        myRigidbody = transform.GetComponent<Rigidbody>();
        //CentredRotation = Quaternion.identity
        initCameraDistance = this.transform.position - gliderCamera.transform.position;
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
        print("pitch " + pitch);


        //print(
        //        "pitch " + pitch
        //        + ", yaw " +yaw 
        //        + ", roll " + roll
        //    );
        print("rigidbody rot: " + myRigidbody.rotation.eulerAngles);
        rotToAdd.eulerAngles = new  Vector3(pitch, yaw, roll);
        myRigidbody.rotation *= rotToAdd;

        //if (myRigidbody.rotation.eulerAngles.x < 290 && myRigidbody.rotation.eulerAngles.x > 100)
        //{
        //    startAccelerationSpeed -= 0.1f;
        //}
        //else {
        //    startAccelerationSpeed += 0.1f;
        //}

        //startAccelerationSpeed = Mathf.Clamp(startAccelerationSpeed, 0.1f, 1.9f);

        //movementSpeed *= startAccelerationSpeed;


        Vector3 posToAdd = Vector3.forward;
        posToAdd = myRigidbody.rotation * posToAdd;
        myRigidbody.velocity = posToAdd * (Time.deltaTime * movementSpeed);

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

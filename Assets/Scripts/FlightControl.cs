using UnityEngine;
using System.Collections;

public class FlightControl : MonoBehaviour {
    float movementSpeed = 1000f;
    float rotSpeed = 20f;
    float vertMovement;
    float horiMovement;
    Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Quaternion rotToAdd = Quaternion.identity;

        horiMovement = Input.GetAxisRaw("Roll") * (Time.deltaTime * rotSpeed);
        vertMovement = Input.GetAxisRaw("Pitch") * (Time.deltaTime * rotSpeed);

        rotToAdd.eulerAngles =new  Vector3(vertMovement, horiMovement, 0);
        rigidBody.rotation *= rotToAdd;


        Vector3 posToAdd = Vector3.forward;
        posToAdd = rigidBody.rotation * posToAdd;
        rigidBody.velocity = posToAdd * (Time.deltaTime * movementSpeed);

    }
}

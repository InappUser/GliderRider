using UnityEngine;
using System.Collections;

public class ClassCubeThrow : MonoBehaviour
{
    public GameObject classCubeEmitter;
    public GameObject classCube;
    public float velocity;
    public double delayToFreeze;
    private GameObject cubeHandler;
    private Rigidbody rigidBody;

    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //The Bullet instantiation happens here.
           // GameObject cubeHandler;
            cubeHandler = Instantiate(classCube, classCubeEmitter.transform.position, classCubeEmitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            cubeHandler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
           // Rigidbody rigidBody;
            rigidBody = cubeHandler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            rigidBody.AddForce(transform.up * velocity);

            }

    }

}






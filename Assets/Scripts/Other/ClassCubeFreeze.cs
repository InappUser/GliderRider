using UnityEngine;
using System.Collections;

public class ClassCubeFreeze: MonoBehaviour {

    public double delayToFreeze;
    public GameObject classCube;
    private Rigidbody body;

    // Use this for initialization
    void Start () {
        body = classCube.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	  delayToFreeze -= Time.deltaTime;

        if (delayToFreeze <= 0)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            classCube.transform.rotation = Quaternion.identity;
        }
	}
}

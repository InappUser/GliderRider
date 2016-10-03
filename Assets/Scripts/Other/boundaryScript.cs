using UnityEngine;
using System.Collections;

public class boundaryScript : MonoBehaviour {

    public static GameObject plane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter() {
        Debug.Log("player out of bounds");
    }
    void OnTriggerExit()
    {
        Debug.Log("player back in bounds");
    }
}

using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;
    Transform posCheck;
    public float sensX = 4.0f, sensY = 1.0f, sensScroll = 1.5f;
    public float xAxisStarRot = 30.0f, distance = 10.0f;

    private Camera cam;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    private float currentX = 0;
    private float currentY = 0;


    private Vector3 lastEuler;
    private float eulerDelta;

    private void Start()
    {
        currentY = xAxisStarRot;
        //		Cursor.lockState = CursorLockMode.Locked;
        //		Cursor.visible = false;
        lastEuler = lookAt.eulerAngles;
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        //		print ("delta "+eulerDelta);
        //		print ("mousex " + Input.GetAxis ("Mouse X"));
        currentX += /*eulerDelta  +*/ Input.GetAxis("Mouse X") * sensX;
        currentY += -Input.GetAxis("Mouse Y") * sensY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        GetEulerDelta();
    }
    private void LateUpdate()
    {//doing in the late update because the normal update will be used for player movement - this is dependant on that
        distance -= Input.GetAxis("Mouse ScrollWheel") * sensScroll;
        Vector3 dir = new Vector3(0, 0, -distance);
        //Quaternion lkAt = Quaternion.Euler (0,lookAt.rotation.y,0);//lookAt.rotation;

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //posCheck.position = lookAt.position + rotation * dir;
        //Vector3 posCheckDir = (camTransform.position - posCheck.position).normalized;
        //if(Physics.Raycast(camTransform.position,posCheckDir
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }
    void GetEulerDelta()
    {
        eulerDelta = lookAt.eulerAngles.y - lastEuler.y;
        lastEuler = lookAt.eulerAngles;
    }
}

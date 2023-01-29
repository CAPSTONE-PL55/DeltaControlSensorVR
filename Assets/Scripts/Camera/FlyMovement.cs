using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    //private Vector3 CameraPosition;

    [Header("Camera Settings")]
    [Tooltip("Activate rotation by pressing left mouse button.")]
    public float sensitivity = 2f; // adjust this to change the sensitivity of the rotation
    public float moveSpeed = 20f; // adjust this to change the speed of movement
    public float upDownSpeed = 10f; // adjust this to change the speed of up and down movement
    [Tooltip("Activate sprint by holding shift.")]
    public float sprintSpeed = 4f; // adjust this to change the speed of fast movement
    private float upDownRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //CameraPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the left mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
            upDownRotation -= mouseY;
            upDownRotation = Mathf.Clamp(upDownRotation, -90, 90);
            transform.localEulerAngles = new Vector3(upDownRotation,transform.localEulerAngles.y, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        float moveZ = (Input.GetKey(KeyCode.W)) ? moveSpeed / 10 : (Input.GetKey(KeyCode.S)) ? - moveSpeed / 10 : 0;
        float moveX = (Input.GetKey(KeyCode.D)) ? moveSpeed / 10 : (Input.GetKey(KeyCode.A)) ? - moveSpeed / 10 : 0;
        float moveY = (Input.GetKey(KeyCode.Q)) ? upDownSpeed / 10 : (Input.GetKey(KeyCode.E)) ? - upDownSpeed / 10 : 0;
        
        // Check if sprint is activated
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.position += (transform.forward * moveZ + transform.right * moveX + transform.up * moveY) * Time.deltaTime * sprintSpeed;
        }
        else 
        {
            transform.position += (transform.forward * moveZ + transform.right * moveX + transform.up * moveY) * Time.deltaTime;
        }
    }
}

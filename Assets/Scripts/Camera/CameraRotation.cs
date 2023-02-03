using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("Camera horizontal sensitivity.")]
    public float camSensX = 20f;
    [Tooltip("Camera vertical sensitivity.")]
    public float camSensY = 20f;

    //public Transform orientation;
    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursor.visible) {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * camSensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * camSensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.R)){
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}

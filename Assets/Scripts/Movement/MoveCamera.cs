using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition1;
    public Transform cameraPosition2;

    private bool cam1;

    private void Start()
    {
        cam1 = true;
    }

    private void Update()
    {
        if (cam1) {
            transform.position = cameraPosition1.position;
        } else {
            transform.position = cameraPosition2.position;
        }
        
        if (Input.GetKeyDown(KeyCode.C)) cam1 = !cam1;
    }
}

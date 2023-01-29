using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scipt allows toggelling different objects into view
   1) Need to go to Edit -> Project Settings -> Input Mangager
   2) Open axis, increament size by 4
   3) Assign names for new inputs to match ones used in this file
   4) Attach to any object that is constantly rendered (not a camera)
   5) Attach cameras to cam variables
*/
public class CameraSelect : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    //public GameObject cam3;
    //public GameObject cam4;

    private void setCameras(bool first, bool second, bool third, bool fourth) {
        cam1.SetActive(first);
        cam2.SetActive(second);
        //cam3.SetActive(third);
        //cam4.SetActive(fourth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key")) {
            setCameras(true, false, false, false);
        }
        if (Input.GetButtonDown("2Key")) {
            setCameras(true, true, false, false);
        }
        /*if (Input.GetButtonDown("3Key")) {
            setCameras(false, false, true, false);
        }
        if (Input.GetButtonDown("4Key")) {
            setCameras(false, false, false, true);
        }*/
    }
}

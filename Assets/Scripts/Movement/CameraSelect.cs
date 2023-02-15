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

    //private GameObject cam1Holder;
    private GameObject cam1Avatar;
    //private GameObject cam2Holder;
    private GameObject cam2Avatar;

    void Start()
    {
        //cam1Holder = cam1.transform.GetChild(0).gameObject;
        cam1Avatar = cam1.transform.GetChild(0).gameObject;
        //cam2Holder = cam2.transform.GetChild(0).gameObject;
        cam2Avatar = cam2.transform.GetChild(0).gameObject;

        setCameras(true, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (!cam1.activeSelf) {
                Vector3 cam2Pos = cam2Avatar.transform.position;
                //Quaternion cam2Rotation = cam2Avatar.transform.GetChild(0).gameObject.transform.rotation;
                setCameras(true, false);
                //cam1Holder.transform.position = cam2Pos;
                cam1Avatar.transform.position = cam2Pos;
                //cam1Holder.transform.GetChild(0).gameObject.transform.rotation = cam2Rotation;
                //cam1Avatar.transform.GetChild(0).gameObject.transform.rotation = cam2Rotation;
            } else {
                Vector3 cam1Pos = cam1Avatar.transform.position;
                //Quaternion cam1Rotation = cam1Avatar.transform.GetChild(0).gameObject.transform.rotation;
                setCameras(false, true);
                //cam2Holder.transform.position = cam1Pos;
                cam2Avatar.transform.position = cam1Pos;
                //cam2Holder.transform.GetChild(0).gameObject.transform.rotation = cam1Rotation;
                //cam2Avatar.transform.GetChild(0).gameObject.transform.rotation = cam1Rotation;
            }
            
        }
        /*if (Input.GetButtonDown("2Key")) {
            setCameras(false, true, false, false);
        }
        if (Input.GetButtonDown("3Key")) {
            setCameras(false, false, true, false);
        }
        if (Input.GetButtonDown("4Key")) {
            setCameras(false, false, false, true);
        }*/
    }

    private void setCameras(bool first, bool second) {
        cam1.SetActive(first);
        cam2.SetActive(second);
        //cam3.SetActive(third);
        //cam4.SetActive(fourth);
    }
}

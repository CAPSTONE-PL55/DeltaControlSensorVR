using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightad : MonoBehaviour
{
    public Light myLight;

    public GameObject mqttReceiver;

    [SerializeField]
    private double lux = 200.0;


    void Start()
    {
        myLight = GetComponent<Light>();
        InvokeRepeating("getreadings", 0.0f, 1.0f);
    }
    
    void getreadings()
    {
        lux = mqttReceiver.GetComponent<mqttReceiver1>().readings.light;
    }
    // Update is called once per frame
    void Update()
    { 
        if(lux < 50.0)
        {
            myLight.intensity = 0.0f;
            myLight.range = 3.0f;
            myLight.color = new Color(1.0f,1.0f,0.6f,1.0f);
        }
        else if(lux < 200.0)
        {
            myLight.intensity = 5.0f;
            myLight.range = 5.0f;
            myLight.color = new Color(1.0f,1.0f,0.7f,1.0f);
        }
        else if(lux < 500.0)
        {
            myLight.intensity = 15.0f;
            myLight.range = 7.0f;
            myLight.color = new Color(1.0f,1.0f,0.8f,1.0f);
        }
        else if(lux < 2000.0)
        {   
            myLight.intensity = 30.0f;
            myLight.range = 10.0f;
            myLight.color = new Color(1.0f,1.0f,0.9f,1.0f);
        }
        else
        {
            myLight.intensity = 50.0f;
            myLight.range = 12.0f;
            myLight.color = new Color(1.0f,1.0f,1.0f,1.0f);
        }

    }
}
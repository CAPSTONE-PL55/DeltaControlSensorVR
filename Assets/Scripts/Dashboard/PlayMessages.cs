using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using M2MqttUnity;

// This script must be attached to a 3D object that contains the messages within
public class PlayMessages : MonoBehaviour
{
    // each index represents one of the dropdowns. The value at each index indicates which sensor is selected
    private int[] dropdownValues = new int[] { 0, 0, 0 };
    public mqttReceiver1 mqttReceiver1;
    public mqttReceiver1 mqttReceiver2;

    public Sprite playSprite;
    public Sprite retrySprite;


    // Start is called before the first frame update
    void Start()
    {
        // logic to find the associated receiver object based on which sensor is playing a sound

        GameObject[] receiver1GameObject = GameObject.FindGameObjectsWithTag("Receiver1");
        GameObject[] receiver2GameObject = GameObject.FindGameObjectsWithTag("Receiver2");

        if (receiver1GameObject.Length!=0)
        {
            Debug.Log("receiver 1 is found");
            mqttReceiver1 = receiver1GameObject[0].gameObject.GetComponent<mqttReceiver1>(); //receiver number depends on what sensor is selected
        }

        if (receiver2GameObject.Length!=0)
        {
            Debug.Log("receiver 2 is found");
            mqttReceiver2 = receiver2GameObject[0].gameObject.GetComponent<mqttReceiver1>(); //receiver number depends on what sensor is selected
        }

        // Iterate through all messages and add event listeners for each button and dropdown
        for (int i = 0; i < transform.childCount; i++)
        {
            int index = i;
            Transform child = transform.GetChild(i);
            Text textField = child.GetComponent<Text>();

            if (textField != null) {
                Transform btnObject = child.Find("PlayBtn");
                Image imageComponent = btnObject.GetComponent<Image>();

                Dropdown sensorSelect = textField.GetComponentInChildren<Dropdown>();
                sensorSelect.onValueChanged.AddListener((int value) =>
                {
                    dropdownValues[index] = value;
                    Debug.Log("Dropdown value changed: " + value);
                    if (imageComponent.sprite != playSprite) {
                        imageComponent.sprite = playSprite; 
                    }
                });

                Button playBtn = btnObject.GetComponent<Button>();
                playBtn.onClick.AddListener(() =>
                {
                    Debug.Log("Button clicked: " + textField.text);
                    Debug.Log("value of index: " + index);

                    // replace hardcoded sound file with the output of private helper function to map textField.text to sound file
                    string soundFile;
                    Debug.Log("the textField.text value is: "+ textField.text);

                    if(textField.text.Trim().Equals("FIRE ALARM"))
                    {
                        soundFile = "test fire alarm";
                    }
                    else if(textField.text.Trim().Equals("TIME IS UP"))
                    {
                        soundFile = "test time is up";
                    }
                    else if(textField.text.Trim().Equals("5 MINUTE WARNING"))
                    {
                        soundFile = "test 5 minute warning";
                    }
                    else
                    {
                        soundFile = "garbage value";
                    }

                    if(dropdownValues[index] == 0)
                    {
                        mqttReceiver1.Publish("commands/object/soundfile", soundFile);
                    }
                    else if(dropdownValues[index] == 1)
                    {
                        mqttReceiver2.Publish("commands/object/soundfile", soundFile);
                    }
                    else
                    {
                        mqttReceiver1.Publish("commands/object/soundfile", soundFile);
                        mqttReceiver2.Publish("commands/object/soundfile", soundFile);
                    }

                    // mqttReceiver.Publish("commands/object/soundfile", "(1) Power On.wav");
                    imageComponent.sprite = retrySprite;
                });
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

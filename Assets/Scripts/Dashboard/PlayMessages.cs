using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using M2MqttUnity;

// This script must be attached to a 3D object that contains the messages within
public class PlayMessages : MonoBehaviour
{
    // each index represents one of the dropdowns. The value at each index indicates which sensor is selected
    public mqttReceiver1 mqttReceiver1;
    public mqttReceiver1 mqttReceiver2;

    public Sprite playSprite;
    public Sprite retrySprite;

    private int[] dropdownValues = new int[] { 0, 0, 0 };
    private string voiceCueTopicList = "commands/object/soundfile";


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

                    string soundFile = DetermineSoundFile(textField.text.Trim());
                    Debug.Log("the textField.text value is: "+ textField.text);

                    // publish the soundFile to the appropriate O3 device depending on dropdown selection
                    if(dropdownValues[index] == 0)
                    {
                        mqttReceiver1.Publish("1/"+ voiceCueTopicList, soundFile);
                    }
                    else if(dropdownValues[index] == 1)
                    {
                        mqttReceiver2.Publish("2/"+ voiceCueTopicList, soundFile);
                    }
                    else
                    {
                        mqttReceiver1.Publish("1/"+voiceCueTopicList, soundFile);
                        mqttReceiver2.Publish("2/"+voiceCueTopicList, soundFile);
                    }

                    imageComponent.sprite = retrySprite;
                });
            }            
        }
    }

    // determines which soundfile to publish based on the textField text read in from dropdown selection
    private string DetermineSoundFile(string text)
    {
        // replace the placeholder outputFile strings below with actual O3 Edge soundfiles
        // i.e "(1) Power On.wav"
        string outputFile = "";

        if (text.Equals("\"NOTIFY\""))
        {
            outputFile = "(25) AmbientAlert.wav";
        }
        else if (text.Equals("\"ALARM\""))
        {
            outputFile = "(14) Security Alarm.wav";
        }
        else if (text.Equals("\"WARNING\""))
        {
            outputFile = "(11) Critical Alarm.wav";
        }
        else
        {
            outputFile = "null";
        }

        return outputFile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

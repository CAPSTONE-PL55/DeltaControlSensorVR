using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManageToggles : MonoBehaviour
{
    public GameObject togglesObj;

    public GameObject rain;
    public GameObject notes;

    private bool toggleAll = true;
    private Toggle temperature;
    private Toggle humidity;
    private Toggle luminance;
    private Toggle sound;
    private Toggle occupancy;
    private Toggle all;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = togglesObj.transform;

        // Loop through all the direct children of the parent
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            Toggle toggle = childTransform.GetComponent<Toggle>();

            Transform labelObj = childTransform.Find("Label");
            Text labelText = labelObj.GetComponent<Text>();

            if (labelText.text == "All") {
                toggle.onValueChanged.AddListener(delegate {ToggleAll();});
                all = toggle;
            } else {
                toggle.onValueChanged.AddListener(delegate {SetAll();});
                switch (labelText.text)
                {
                    case "Temperature":
                        temperature = toggle;
                        break;
                    case "Humidity":
                        humidity = toggle;
                        break;
                    case "Luminance":
                        luminance = toggle;
                        break;
                    case "Sound":
                        sound = toggle;
                        break;   
                    default:
                        occupancy = toggle;
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAll() 
    {
        if (temperature.isOn && humidity.isOn && luminance.isOn && sound.isOn && occupancy.isOn) {
            if (!all.isOn) {
                all.isOn = true;
            }
            
        } else {
            if (all.isOn) {
                toggleAll = false;
                all.isOn = false;
            }
        }

        if (humidity.isOn) {
            rain.SetActive(true);
        } else {
            rain.SetActive(false);
        }

        if (sound.isOn) {
            notes.SetActive(true);
        } else {
            notes.SetActive(false);
        }
    }

    void ToggleAll()
    {
        if (all.isOn) {
            temperature.isOn = true;
            humidity.isOn = true;
            luminance.isOn = true;
            sound.isOn = true;
            occupancy.isOn = true;
        } else {
            if (toggleAll) {
                temperature.isOn = false;
                humidity.isOn = false;
                luminance.isOn = false;
                sound.isOn = false;
                occupancy.isOn = false;
            } else {
                toggleAll = true;
            }
        }
    }
}

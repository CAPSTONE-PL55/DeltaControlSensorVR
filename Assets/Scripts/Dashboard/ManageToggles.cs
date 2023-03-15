using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManageToggles : MonoBehaviour
{
    public Toggle temperature;
    public Toggle humidity;
    public Toggle luminance;
    public Toggle sound;
    public Toggle occupancy;
    public Toggle all;

    public GameObject rain;

    private bool toggleAll = true;

    // Start is called before the first frame update
    void Start()
    {
        all.onValueChanged.AddListener(delegate {ToggleAll();});
        temperature.onValueChanged.AddListener(delegate {SetAll();});
        humidity.onValueChanged.AddListener(delegate {SetAll();});
        luminance.onValueChanged.AddListener(delegate {SetAll();});
        sound.onValueChanged.AddListener(delegate {SetAll();});
        occupancy.onValueChanged.AddListener(delegate {SetAll();});

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

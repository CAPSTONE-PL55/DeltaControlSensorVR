using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humidity : MonoBehaviour
{

    public GameObject rain;
    public GameObject rainRipple;
    public GameObject drip;
    public GameObject dripRipple;
    public GameObject stormClouds;

    private int humidity = 45;
    private ParticleSystem rainParticleSystem;
    private ParticleSystem dripParticleSystem;

    private List<ParticleSystem.Burst> lowDrip;
    private List<ParticleSystem.Burst> highDrip;

    // Start is called before the first frame update
    void Start()
    {
        rainParticleSystem =  rain.GetComponent<ParticleSystem>();
        dripParticleSystem =  drip.GetComponent<ParticleSystem>();

        SetBurstLists();
        //SetParticlesActive(true, false, false, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        int tempHumid = humidity;
        SetHumidityValue();

        if (tempHumid != humidity) {
            CheckHumidity();
        }
        
    }

    // Set the burst lists for drip frequency on very dry and dry conditions
    void SetBurstLists() 
    {
        // Note: new ParticleSystem.Burst(Time, Count, Cycles, Interval)
        lowDrip = new List<ParticleSystem.Burst>();
        lowDrip.Add(new ParticleSystem.Burst(0f, 1, 1, 1f));

        highDrip = new List<ParticleSystem.Burst>();
        highDrip.Add(new ParticleSystem.Burst(0f, 2, 1, 1f));
    }

    // Based on current humidity, set current display level
    void CheckHumidity()
    {
        if (humidity < 25) { // Very dry
            SetParticlesActive(false, false, true, false, false);
        } else if (humidity < 40) { // Dry
            SetParticlesActive(false, false, true, true, false);
        } else if (humidity < 60) { // Ideal
            SetParticlesActive(true, false, false, false, false);
        } else if (humidity < 70) { // Wet
            SetParticlesActive(true, true, false, false, false);
        } else { // Very wet
            SetParticlesActive(true, true, false, false, true);
        }
    }

    // for testing
    void SetHumidityValue()
    {
        if (Input.GetAxis("1Key") != 0) {
            humidity = 20;
        } else if (Input.GetAxis("2Key") != 0) {
            humidity = 30;
        } else if (Input.GetAxis("3Key") != 0) {
            humidity = 50;
        } else if (Input.GetAxis("4Key") != 0) {
            humidity = 65;
        } else if (Input.GetAxis("5Key") != 0) {
            humidity = 75;
        }
    }

    void SetParticlesActive(bool r, bool rR, bool d, bool dR, bool sC)
    {
        rain.SetActive(r);
        rainRipple.SetActive(rR);
        drip.SetActive(d);
        dripRipple.SetActive(dR);
        stormClouds.SetActive(sC);

        var emissionModule = rainParticleSystem.emission;

        if (r && rR && sC) {
            emissionModule.rateOverTime = 60f;
        } else if (r && rR) {
            emissionModule.rateOverTime = 40f;
        } else {
            emissionModule.rateOverTime = 20f;
        }

        ParticleSystem.MainModule main = dripParticleSystem.main;
        if (d && dR) {
            dripParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            dripParticleSystem.emission.SetBursts(highDrip.ToArray());
            main.duration = 0.25f;
            dripParticleSystem.Play();
        } else if (d) {
            dripParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            dripParticleSystem.emission.SetBursts(lowDrip.ToArray());
            main.duration = 0.5f;
            dripParticleSystem.Play();
        }

    }
}

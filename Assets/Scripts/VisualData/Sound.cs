using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private double sound;
    public GameObject mqttReceiver;

    private ParticleSystem musicNotes;

    // Start is called before the first frame update
    void Start()
    {
        sound = 41;

        Transform sensor = transform.Find("MusicNotes");
        musicNotes = sensor.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        GetReadings();

        var emissionModule = musicNotes.emission;
        var mainModule = musicNotes.main;

        // Set the emission rate
        if (sound <= 10) {
            emissionModule.rateOverTime = 0f;
        } else if (sound <= 20) {
            emissionModule.rateOverTime = 1f;
        } else if (sound <= 30) {
            emissionModule.rateOverTime = 3f;
        } else if (sound <= 40) {
            emissionModule.rateOverTime = 6f;
        } else if (sound <= 50) { // Size increases
            emissionModule.rateOverTime = 3f;
        } else if (sound <= 60) {
            emissionModule.rateOverTime = 6f;
        } else if (sound <= 70) {
            emissionModule.rateOverTime = 8f;
        } else if (sound <= 80) {
            emissionModule.rateOverTime = 11f;
        } else if (sound <= 90) { // Size increases
            emissionModule.rateOverTime = 6f;
        } else if (sound <= 100) {
            emissionModule.rateOverTime = 9f;
        } else {
            emissionModule.rateOverTime = 12f;
        }

        // Set the size
        if (sound <= 40) {
            mainModule.startSize = 0.2f;
        } else if (sound <= 80) {
            mainModule.startSize = 0.5f;
        } else {
            mainModule.startSize = 1.0f;
        }
    }

    void GetReadings()
    {
        sound = mqttReceiver.GetComponent<mqttReceiver1>().readings.sound;
    }
}

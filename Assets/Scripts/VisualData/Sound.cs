using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private int sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = 41;
    }

    // Update is called once per frame
    void Update()
    {
        SetSoundValue();

        // Iterate through all sensors access each partical emitter
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Transform sensor = child.Find("MusicNotes");
            ParticleSystem musicNotes = sensor.GetComponent<ParticleSystem>();

            SetVisualization(musicNotes, sound);
        }
    }

    void SetVisualization(ParticleSystem musicNotes, int soundValue)
    {
        var emissionModule = musicNotes.emission;
        var mainModule = musicNotes.main;

        // Set the emission rate
        if (soundValue <= 10) {
            emissionModule.rateOverTime = 0f;
        } else if (soundValue <= 20) {
            emissionModule.rateOverTime = 1f;
        } else if (soundValue <= 30) {
            emissionModule.rateOverTime = 3f;
        } else if (soundValue <= 40) {
            emissionModule.rateOverTime = 6f;
        } else if (soundValue <= 50) { // Size increases
            emissionModule.rateOverTime = 3f;
        } else if (soundValue <= 60) {
            emissionModule.rateOverTime = 6f;
        } else if (soundValue <= 70) {
            emissionModule.rateOverTime = 8f;
        } else if (soundValue <= 80) {
            emissionModule.rateOverTime = 11f;
        } else if (soundValue <= 90) { // Size increases
            emissionModule.rateOverTime = 6f;
        } else if (soundValue <= 100) {
            emissionModule.rateOverTime = 9f;
        } else {
            emissionModule.rateOverTime = 12f;
        }

        // Set the size
        if (soundValue <= 40) {
            mainModule.startSize = 0.2f;
        } else if (soundValue <= 80) {
            mainModule.startSize = 0.5f;
        } else {
            mainModule.startSize = 1.0f;
        }
    }

    // Temp, sets sound based on keys, remove this later
    void SetSoundValue()
    {
        if (Input.GetKeyDown(KeyCode.P) && sound < 100) {
            sound += 10;
            Debug.Log("Sound: " + sound);
        } else if (Input.GetKeyDown(KeyCode.L) && sound > 0) {
            sound -= 10;
            Debug.Log("Sound: " + sound);
        } 
    }
}

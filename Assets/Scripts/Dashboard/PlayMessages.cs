using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script must be attached to a 3D object that contains the messages within
public class PlayMessages : MonoBehaviour
{
    public Sprite playSprite;
    public Sprite retrySprite;

    // Start is called before the first frame update
    void Start()
    {
        // Iterate through all messages and add event listeners for each button and dropdown
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Text textField = child.GetComponent<Text>();

            if (textField != null) {
                Transform btnObject = child.Find("PlayBtn");
                Image imageComponent = btnObject.GetComponent<Image>();

                Dropdown sensorSelect = textField.GetComponentInChildren<Dropdown>();
                sensorSelect.onValueChanged.AddListener((int value) =>
                {
                    Debug.Log("Dropdown value changed: " + value);
                    if (imageComponent.sprite != playSprite) {
                        imageComponent.sprite = playSprite; 
                    }
                });

                Button playBtn = btnObject.GetComponent<Button>();
                playBtn.onClick.AddListener(() =>
                {
                    Debug.Log("Button clicked: " + textField.text);
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

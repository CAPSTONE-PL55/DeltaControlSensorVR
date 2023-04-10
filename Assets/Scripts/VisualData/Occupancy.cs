using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupancy : MonoBehaviour
{
    public float fadeTime = 1.0f;
    public double occupancy = 0;
    public int occupancyLimit = 2;

    private MeshRenderer meshRenderer;
    private Material material;
    private Material[] materials;
    public GameObject mqttReceiver;

    // Initiallize array of materials for avatars and set all alphas to 0
    private void Start()
    {
        //stickmenAvatars = new GameObject[occupancyLimit];
        materials = new Material[occupancyLimit];

        for (int i = 0; i < occupancyLimit; i++) 
        {
            GameObject stickmanAvatar = transform.Find("Occupancy").Find(string.Concat("Stickman", i)).gameObject;
            meshRenderer = stickmanAvatar.GetComponent<MeshRenderer>();

            Material material = meshRenderer.material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
            materials[i] = material;
        }
        
    }

    // Based on the current occupancy level, update the number of visable occupants and set oppcupant colour
    private void Update()
    {
        GetReadings();

        for (int i = 0; i < occupancyLimit; i++)
        {
            Material material = materials[i];

            // Change visiblity based on current occupancy
            if ((occupancy > i) && material.color.a < 1)
            {
               ChangeAlpha(material, 1.0f);
            } 
            else if ((occupancy <= i) && material.color.a > 0)
            {
                ChangeAlpha(material, 0.0f);
            }

            // Change colour of all avatars under certain occupancy levels (checks if material already has colour to apply first)
            float percentOccupied = (float) occupancy / (float) occupancyLimit;
            if (percentOccupied <= 0.25f && material.color.r != 19/255f)
            {
                material.color = new Color(19/255f, 149/255f, 26/255f, material.color.a);
            }
            else if (percentOccupied <= 0.75f && material.color.r != 235/255f)
            {
                material.color = new Color(235/255f, 230/255f, 15/255f, material.color.a);
            }
            else if (percentOccupied < 1.0f && material.color.r != 180/255f)
            {
                material.color = new Color(180/255f, 100/255f, 14/255f, material.color.a);
            }
            else if (material.color.r != 220/255f)
            {
                material.color = new Color(220/255f, 40/255f, 10/255f, material.color.a);
            }
        }
    }

    void GetReadings()
    {
        occupancy = mqttReceiver.GetComponent<mqttReceiver1>().readings.occupancy;
    }

    private void ChangeAlpha(Material material, float targetAlpha)
    {
        float alpha = Mathf.Lerp(material.color.a, targetAlpha, Time.deltaTime / fadeTime);
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    }
}

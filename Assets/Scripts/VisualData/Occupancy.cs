using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupancy : MonoBehaviour
{
    public float fadeTime = 1.0f;
    public int occupancy = 0;
    public int occupancyLimit = 2;

    private MeshRenderer meshRenderer;
    private Material material;
    private bool changeState = false;
    private Material[] materials;
    private GameObject[] stickmenAvatars;

    private void Start()
    {
        stickmenAvatars = new GameObject[occupancyLimit];
        materials = new Material[occupancyLimit];

        for (int i = 1; i < occupancyLimit; i++) 
        {
            GameObject stickmanAvatar = transform.Find("Occupancy").Find(string.Concat("Stickman", i)).gameObject;
            stickmanAvatar.SetActive(false);
            meshRenderer = stickmanAvatar.GetComponent<MeshRenderer>();
            materials[i] = meshRenderer.material;
            materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, 0);
            
            stickmenAvatars[i] = stickmanAvatar;
        }
        
    }

    private void Update()
    {
        
        // Every time
        //if (stickmanAvatar.activeSelf && material.color.a < 1)
        //{
        //    float alpha = Mathf.Lerp(material.color.a, 1, Time.deltaTime / fadeTime);
        //    material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
        //}

        
    }
}
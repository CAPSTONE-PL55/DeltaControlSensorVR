using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupancy : MonoBehaviour
{
    public GameObject objectToFade;
    public float fadeTime = 1.0f;

    private MeshRenderer meshRenderer;
    private Material material;

    private void Start()
    {
        meshRenderer = objectToFade.GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            objectToFade.SetActive(true);
        }

        if (objectToFade.activeSelf && material.color.a < 2)
        {
            float alpha = Mathf.Lerp(material.color.a, 2, Time.deltaTime / fadeTime);
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResizer : MonoBehaviour
{
    public Button resizeButton;
    public RectTransform panelRectTransform;
    public GameObject panelContent;
    public Text cameraMode;

    private bool isResized = false;
    private bool isWalkMode = false;

    // Start is called before the first frame update
    void Start()
    {
        resizeButton.onClick.AddListener(ResizePanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            ResizePanel();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if (isWalkMode) {
                cameraMode.text = "Current Camera Mode: Fly";
                isWalkMode = false;
            } else {
                cameraMode.text = "Current Camera Mode: Walk";
                isWalkMode = true;
            }
        }
        
    }

    void ResizePanel()
    {
        if (isResized)
        {
            resizeButton.GetComponentInChildren<Text>().text = "Hide Dashboard";
            panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, 340f);
            panelRectTransform.anchoredPosition = new Vector2(panelRectTransform.anchoredPosition.x, -170f);

            panelContent.SetActive(true);
            isResized = false;
        }
        else
        {
            resizeButton.GetComponentInChildren<Text>().text = "Show Dashboard";
            panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, 66f);
            panelRectTransform.anchoredPosition = new Vector2(panelRectTransform.anchoredPosition.x, -33f);

            panelContent.SetActive(false);
            isResized = true;
        }
    }
}

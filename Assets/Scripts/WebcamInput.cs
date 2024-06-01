using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamInput : MonoBehaviour
{
    WebCamTexture captureCard;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Init(string deviceName)
    {
        captureCard = new WebCamTexture(deviceName);
    }

    public void Enable()
    {
        captureCard.Play();
        image.material.mainTexture = captureCard;
    }
    public void Disable()
    {
        captureCard.Stop();
    }
}

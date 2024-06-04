using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamInput : MonoBehaviour
{
    WebCamTexture captureCard;

    Image image;
    RawImage rawImage;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null )
            rawImage = GetComponent<RawImage>();
    }

    public void Init(string deviceName)
    {
        captureCard = new WebCamTexture(deviceName);
    }

    void OnEnable()
    {
        if (captureCard == null)
            return;

        captureCard.Play();

        if (image != null )
            image.material.mainTexture = captureCard;
        else if (rawImage != null)
            rawImage.material.mainTexture = captureCard;
    }
    void OnDisable()
    {

        if (captureCard == null)
            return;

        captureCard.Stop();
    }
}

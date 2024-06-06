using Spout;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SourceHandler : MonoBehaviour
{
    int sourceID;
    public GameObject sourceObject;

    void Start()
    {
        sourceID = InputManager.RegisterSource(this);
    }

    public void OnSourceChange(int sourceID)
    {
        sourceObject.SetActive(sourceID == this.sourceID);
    }
}

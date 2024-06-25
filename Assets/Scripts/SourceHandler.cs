using Spout;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SourceHandler : MonoBehaviour
{
    public string sourceID;
    public KeyCode key;

    public GameObject sourceObject;

    void Start()
    {
        InputManager.RegisterSource(this);
    }

    public void OnSourceChange(string sourceID)
    {
        sourceObject.SetActive(sourceID == this.sourceID);
    }
}

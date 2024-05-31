using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static Action OnRefresh;

    public KeyCode RefreshKey = KeyCode.F5;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(RefreshKey))
            OnRefresh.Invoke();
    }
}

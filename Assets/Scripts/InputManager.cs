using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static Action OnRefresh;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
            OnRefresh.Invoke();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Alpha1 - Alpha9
            for (int k = 0; k <= SceneManager.sceneCountInBuildSettings && k <=9; k++)
            {
                if (Input.GetKeyDown((KeyCode)(k+49)))
                    TransitionManager.TransitionToScene(k);
            }
        }
    }
}

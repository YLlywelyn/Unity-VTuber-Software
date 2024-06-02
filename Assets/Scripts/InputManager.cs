using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static Action OnRefresh;

    // TODO: Change from list to seperate vars and let sources specify which 'channel' they should be on.  Warn if channel is not null.
    public static Action<int> OnSourceChange;
    static List<SourceHandler> SourceList = new List<SourceHandler>();

    public KeyCode RefreshKey = KeyCode.F5;

    public KeyCode SourceKey = KeyCode.S;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            OnSourceChange.Invoke(0);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(RefreshKey))
            OnRefresh.Invoke();

        if (Input.GetKey(SourceKey))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                OnSourceChange.Invoke(0);

            if (SourceList.Count >= 1)
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    OnSourceChange.Invoke(1);

            if (SourceList.Count >= 2)
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    OnSourceChange.Invoke(2);

            if (SourceList.Count >= 3)
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    OnSourceChange.Invoke(3);
        }
    }

    public static int RegisterSource(SourceHandler source)
    {
        if (SourceList.Contains(source))
            Debug.Log("Re-registering source " + source.name);
        else
        {
            SourceList.Add(source);
            OnSourceChange += source.OnSourceChange;
        }
        return SourceList.IndexOf(source) + 1;
    }
}

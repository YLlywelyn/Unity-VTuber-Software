using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static Action OnRefresh;

    // TODO: Change from list to seperate vars and let sources specify which 'channel' they should be on.  Warn if channel is not null.
    public static Action<string> OnSourceChange;
    static Dictionary<string, SourceHandler> SourceList = new Dictionary<string, SourceHandler>();
    
    public KeyCode RefreshKey = KeyCode.F5;

    public KeyCode SourceModifierKey = KeyCode.S;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        OnSourceChange.Invoke(string.Empty);
    }

    void Update()
    {
        if (Input.GetKeyUp(RefreshKey))
            OnRefresh.Invoke();

        if (Input.GetKey(SourceModifierKey))
        {
            foreach (SourceHandler source in SourceList.Values)
            {
                if (Input.GetKeyDown(source.key))
                    OnSourceChange.Invoke(source.sourceID);
            }
        }
    }

    public static void RegisterSource(SourceHandler source)
    {
        if (SourceList.ContainsKey(source.sourceID))
            Debug.Log("Re-registering source " + source.sourceID);
        else
        {
            SourceList.Add(source.sourceID, source);
            OnSourceChange += source.OnSourceChange;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SingletonObject : MonoBehaviour
{
    static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (objects.ContainsKey(gameObject.name) && objects[gameObject.name] != gameObject)
        {
            Destroy(gameObject);
            return;
        }

        if (objects.ContainsKey(gameObject.name))
            objects[gameObject.name] = gameObject;
        else
            objects.Add(gameObject.name, gameObject);
        DontDestroyOnLoad(gameObject);
    }
}

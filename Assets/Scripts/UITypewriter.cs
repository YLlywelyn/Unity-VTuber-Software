using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITypewriter : MonoBehaviour
{
    TextMeshProUGUI textUI;
    Queue<char> text = new Queue<char>();

    bool canActivate = true;

    public bool startOnStart = false;
    public float secondsPerChar = 0.1f;

    public Action OnTypeWriterFinished;

    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        foreach (char c in textUI.text)
            text.Enqueue(c);
        textUI.text = "";

        if (startOnStart)
            StartTypeWriter();
    }

    public void StartTypeWriter()
    {
        if (canActivate)
        {
            canActivate = false;
            StartCoroutine(TypeWriter());
        }
    }

    IEnumerator TypeWriter()
    {
        while (text.Count > 0)
        {
            textUI.text += text.Dequeue();
            yield return new WaitForSeconds(secondsPerChar);
        }

        OnTypeWriterFinished?.Invoke();
    }
}

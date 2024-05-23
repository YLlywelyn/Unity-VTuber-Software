using System.Collections;
using System.Collections.Generic;
using TMPro;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIChatMessage : MonoBehaviour
{
    [Min(0f)]
    public float destroyAfterSeconds = 30f;
    public TwitchChatMessage message;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;

    public RectTransform rectTransform { get { return (RectTransform)transform; } }

    private void Start()
    {
        nameText.text = message.User.DisplayName;
        messageText.text = message.Message;

        if (destroyAfterSeconds > 0f)
            StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, ((messageText.textInfo.lineCount + 1) * 20) + 30);
        Debug.Log("line count: " + messageText.textInfo.lineCount);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(gameObject);
    }
}
